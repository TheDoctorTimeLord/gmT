using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MapSource;

namespace GameThief.GameModel.ServiceClasses
{
    public static class Dijkstra
    {
        public static void DijkstraTraversal(HashSet<Noise>[,] noises, NoiseSource source,
            Action<HashSet<Noise>, Noise> changer)
        {
            var visited = new HashSet<Point>();
            var potentialTransition =
                new Dictionary<Point, Noise>{{source.Position, new Noise(source, source.MaxIntensity)}};
            while (true)
            {
                if (potentialTransition.Count == 0)
                    return;
                var nextPoint = GetLowestCostPoint(potentialTransition);
                visited.Add(nextPoint.Item1);
                changer(noises[nextPoint.Item1.X, nextPoint.Item1.Y], nextPoint.Item2);

                potentialTransition.Remove(nextPoint.Item1);
                UpdateNeighboringPoint(nextPoint, potentialTransition, visited, noises);
            }
        }

        private static void UpdateNeighboringPoint(Tuple<Point, Noise> currentNoise,
            Dictionary<Point, Noise> potentialTransition, HashSet<Point> visited, HashSet<Noise>[,] noises)
        {
            foreach (var point in GetPossibleTransition(currentNoise.Item1, noises, visited))
            {
                var newIntensity = currentNoise.Item2.Intensity - MapManager.Map[point.X, point.Y]
                                       .ObjectContainer.TotalNoiseSuppression - 1;

                if (newIntensity <= 0 ||
                    potentialTransition.ContainsKey(point) &&
                    potentialTransition[point].Intensity >= newIntensity)
                    continue;

                potentialTransition[point] = new Noise(currentNoise.Item2.Source, newIntensity);
            }
        }

        private static IEnumerable<Point> GetPossibleTransition(Point position, HashSet<Noise>[,] noises, HashSet<Point> visited)
        {
            return offsets
                .Select(offset => position + offset)
                .Where(point => InBound(noises, point) && !visited.Contains(point));
        }

        private static bool InBound(HashSet<Noise>[,] noises, Point point)
        {
            return point.X >= 0 && point.X < noises.GetLength(0) &&
                   point.Y >= 0 && point.Y < noises.GetLength(1);
        }

        private static Tuple<Point, Noise> GetLowestCostPoint(Dictionary<Point, Noise> potentialTransition)
        {
            var currentPoint = new Point(-1, -1);
            var highestIntensity = -1;
            foreach (var point in potentialTransition)
            {
                if (point.Value.Intensity <= highestIntensity) continue;
                currentPoint = point.Key;
                highestIntensity = point.Value.Intensity;
            }
            return Tuple.Create(currentPoint, potentialTransition[currentPoint]);
        }

        private static readonly Size[] offsets = new Size[]
        {
            new Size(-1, -1), 
            new Size(-1, 0), 
            new Size(-1, 1), 
            new Size(0, 1), 
            new Size(0, -1), 
            new Size(1, -1), 
            new Size(1, 0), 
            new Size(1, 1)
        };
    }
}
