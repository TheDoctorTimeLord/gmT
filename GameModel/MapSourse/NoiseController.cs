using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using GameThief.GameModel.Managers;

namespace GameThief.GameModel.MapSourse
{
    public class NoiseController
    {
        public readonly List<Noise>[,] Noises;

        public NoiseController(int width, int height)
        {
            Noises = new List<Noise>[width, height];
        }

        public void AddNoiseSourse(NoiseSource source)
        {
            var notVisited = source.GetMaxScope().ToList();
            var noiseCoverage = new Dictionary<Point, int>();
            noiseCoverage[source.Position] =
                source.MaxIntensity - MapManager.Map[source.Position.X, source.Position.Y].Object.NoiseInsulation;

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

                    var currentVolume = noiseCoverage[toOpen] - MapManager.Map[toOpen.X, toOpen.Y].Object.NoiseInsulation - 1;
                    
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
                Noises[point.X, point.Y].Where(n => n.Source != source);
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
