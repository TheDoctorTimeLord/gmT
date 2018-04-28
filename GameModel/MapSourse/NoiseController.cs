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

        public void AddNoiseSourse(NoiseSource source)
        {
            var notVisited = source.GetMaxScope(MapManager.Map).ToList();
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
                    if (!MapManager.InBounds(p))
                        continue;
                    var currentVolume = noiseCoverage[toOpen] - MapManager.Map[toOpen.X, toOpen.Y].Object.NoiseInsulation - 1;
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
    }
}
