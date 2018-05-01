using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameThief.GameModel.Managers;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel.MapSource
{
    public class NoiseController
    {
        public readonly List<Noise>[,] Noises;

        public NoiseController(int width, int height)
        {
            Noises = new List<Noise>[width, height];

            for (var i = 0; i < width; i++) 
            for (var j = 0; j < height; j++)
            {
                Noises[i,j] = new List<Noise>();
            }
        }

        public void AddNoiseSource(NoiseSource source)
        {
            TemporaryObjectsManager.AddTemporaryObject(source);
            var notVisited = source.GetMaxScope().ToList();
            var noiseCoverage = new Dictionary<Point, int>();
            noiseCoverage[source.Position] =
                source.MaxIntensity - MapManager.Map[source.Position.X, source.Position.Y].ObjectContainer.TotalNoiseSuppression;

            while (true)
            {
                Point toOpen = new Point(-1, -1);
                var loudest = int.MinValue;

                foreach (var p in notVisited)
                {
                    if (noiseCoverage.ContainsKey(p) && noiseCoverage[p] > loudest)
                    {
                        loudest = noiseCoverage[p];
                        toOpen = p;
                    }
                }

                if (toOpen == new Point(-1, -1))
                    break;

                foreach (var p in toOpen.FindNeighbours())
                {
                    if (!InScope(p, source))
                        continue;

                    var currentVolume = noiseCoverage[toOpen] - MapManager.Map[toOpen.X, toOpen.Y].ObjectContainer.TotalNoiseSuppression - 1;
                    
                    if (!noiseCoverage.ContainsKey(p) || noiseCoverage[p] < currentVolume)
                        noiseCoverage[p] = currentVolume;
                }

                notVisited.Remove(toOpen);
            }

            foreach (var p in noiseCoverage)
            {
                Noises[p.Key.X, p.Key.Y].Add(new Noise(source, p.Value));
            }
        }

        public void RemoveSourceNoises(NoiseSource source)
        {
            foreach (var point in source.GetMaxScope())
                Noises[point.X, point.Y] = Noises[point.X, point.Y]
                    .Where(n => n.Source != source)
                    .ToList();
        }

        private bool InScope(Point position, NoiseSource source)
        {
            return MapManager.InBounds(position) &&
                   position.X >= source.Position.X - source.MaxIntensity &&
                   position.X <= source.Position.X + source.MaxIntensity &&
                   position.Y >= source.Position.Y - source.MaxIntensity &&
                   position.Y <= source.Position.Y + source.MaxIntensity;
        }
    }
}
