using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel
{
    public static class Map
    {
        public static List<Noise>[,] Noises;
        public static List<NoiseSource> NoiseSourses;
        public static Cell[,] Cells;

        public static void CreateMap(int width, int height, IEnumerable<string> content)
        {
            Noises = new List<Noise>[width, height];
            NoiseSourses = new List<NoiseSource>();
            Cells = new Cell[width, height];
            FillMap(content);
        }

        public static void MoveCreature(Point oldPosition, Point newPosition)
        {
            var creature = Cells[oldPosition.X, oldPosition.Y].Creature;
            Cells[oldPosition.X, oldPosition.Y].Creature = null;
            Cells[newPosition.X, newPosition.Y].Creature = creature;
        }

        public static void RemoveNoises()
        {
            NoiseSourses
                .Select(ns =>
                {
                    if (!ns.IsActive())
                    {
                        foreach (var point in GetActualNoiseCoverage(ns))
                            Noises[point.X, point.Y].Where(n => n.Source != ns);

                        return null;
                    }

                    return ns;
                })
                .Where(ns => ns != null);
        }

        public static void AddNoises(NoiseSource source)
        {
            GetActualNoiseCoverage(source)
                .ForEach(p => Noises[p.X, p.Y].Add(new Noise(source, source.MaxIntensity - Cells[p.X, p.Y].Object.NoiseInsulation)));
        }

        private static List<Point> GetActualNoiseCoverage(NoiseSource source)
        {
            var cellsQueue = new Queue<Point>();
            var visitedCells = new HashSet<Point>();
            var result = new List<Point>();

            cellsQueue.Enqueue(source.Position);
            visitedCells.Add(source.Position);

            while(cellsQueue.Count != 0)
            {
                var currentCell = cellsQueue.Dequeue();
                result.Add(currentCell);
                EnqueueNeighbours(currentCell, source, cellsQueue, visitedCells);
            }

            return result;
        }

        private static void EnqueueNeighbours(Point cell, NoiseSource source, Queue<Point> cellsQueue, HashSet<Point> visitedCells)
        {
            foreach (var neighbour in cell.FindNeighbours())
            {
                if (!InIntensityField(neighbour, source) ||
                    source.MaxIntensity - Cells[neighbour.X, neighbour.Y].Object.NoiseInsulation >= 0 ||
                    visitedCells.Contains(neighbour))
                    continue;

                cellsQueue.Enqueue(neighbour);
                visitedCells.Add(neighbour);
            }
        }

        public static bool InBounds(Point position)
        {
            return
                position.X >= 0 &&
                position.Y >= 0 &&
                position.X < Noises.GetLength(0) &&
                position.Y < Noises.GetLength(1);
        }

        private static bool InIntensityField(Point cell, NoiseSource source)
        {
            return InBounds(cell) &&
                cell.X >= source.Position.X - source.MaxIntensity &&
                cell.X <= source.Position.X + source.MaxIntensity &&
                cell.Y >= source.Position.Y - source.MaxIntensity &&
                cell.Y <= source.Position.Y + source.MaxIntensity;
        }

        private static void FillMap(IEnumerable<string> content)
        {
            throw new NotImplementedException();
        }
    }

    public static class PointExtensions
    {
        public static IEnumerable<Point> FindNeighbours(this Point point)
        {
            return PossibleDirections
                .Select(size => new Point(point.X + size.Width, point.Y + size.Height));
        }

        private static readonly Size[] PossibleDirections = new []
        {
            new Size(0, -1),
            new Size(0, 1),
            new Size(-1, 0),
            new Size(1, 0)
        };
    }
}
