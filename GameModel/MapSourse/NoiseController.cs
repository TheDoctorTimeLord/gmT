using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameThief.GameModel.Managers;

namespace GameThief.GameModel.MapSourse
{
    public class NoiseController
    {
        public List<Noise>[,] Noises;
        private HashSet<NoiseSource> noiseSourses = new HashSet<NoiseSource>();

        public void AddNoiseSourse(Map map, NoiseSource source)
        {
            var notVisited = source.GetMaxScope(map).ToList();
        }

        public void RemoveNoises(Map map)
        {
            noiseSourses
                .Select(ns =>
                {
                    if (!ns.IsActive())
                    {
                        foreach (var point in GetActualNoiseCoverage(map, ns))
                            Noises[point.X, point.Y].Where(n => n.Source != ns);

                        return null;
                    }

                    return ns;
                })
                .Where(ns => ns != null);
        }

        public static void AddNoises(Map map, NoiseSource source)
        {
            GetActualNoiseCoverage(map, source)
                .ForEach(p => map.Noises[p.X, p.Y]
                    .Add(new Noise(source, source.MaxIntensity - map.Cells[p.X, p.Y].Object.NoiseInsulation)));
        }

        private static List<Point> GetActualNoiseCoverage(Map map, NoiseSource source)
        {
            var cellsQueue = new Queue<Point>();
            var visitedCells = new HashSet<Point>();
            var result = new List<Point>();

            cellsQueue.Enqueue(source.Position);
            visitedCells.Add(source.Position);

            while (cellsQueue.Count != 0)
            {
                var currentCell = cellsQueue.Dequeue();
                result.Add(currentCell);
                EnqueueNeighbours(map, currentCell, source, cellsQueue, visitedCells);
            }

            return result;
        }

        private static void EnqueueNeighbours(Map map, Point cell, NoiseSource source, Queue<Point> cellsQueue, HashSet<Point> visitedCells)
        {
            foreach (var neighbour in cell.FindNeighbours())
            {
                if (!InIntensityField(map, neighbour, source) ||
                    source.MaxIntensity - map.Cells[neighbour.X, neighbour.Y].Object.NoiseInsulation >= 0 ||
                    visitedCells.Contains(neighbour))
                    continue;

                cellsQueue.Enqueue(neighbour);
                visitedCells.Add(neighbour);
            }
        }

        private static bool InIntensityField(Map map, Point cell, NoiseSource source)
        {
            return MapManager.InBounds(map, cell) &&
                   cell.X >= source.Position.X - source.MaxIntensity &&
                   cell.X <= source.Position.X + source.MaxIntensity &&
                   cell.Y >= source.Position.Y - source.MaxIntensity &&
                   cell.Y <= source.Position.Y + source.MaxIntensity;
        }
    }
}
