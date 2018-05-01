using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;
using GameThief.GameModel.Managers;

namespace GameThief.GameModel.FieldOfView
{
    class FieldOfViewRectangle
    {
        public int ViewWidth;
        public int ViewDistance;
        public Point Position;
        public Direction ViewDirection;

        public List<Point> GetScope()
        {
            var oppositeDirection = GameState.ConvertDirectionToSize[GameState.RotateFromTo(ViewDirection, true)];

            var pointsToCheck = Enumerable
                .Range(0, ViewWidth)
                .SelectMany(num => new[]
                {
                    new Size(Position + new Size(oppositeDirection.Width * num, oppositeDirection.Height * num)),
                    new Size(Position - new Size(oppositeDirection.Width * num, oppositeDirection.Height * num))
                })
                .Distinct()
                .Select(sz => new Point(sz))
                .ToList();

            return GetActualScope(ViewDistance, GameState.ConvertDirectionToSize[ViewDirection], pointsToCheck).ToList();
        }

        private IEnumerable<Point> GetActualScope(int distance, Size direction, List<Point> pointsToCheck)
        {
            while (true)
            {
                if (distance < 0 || pointsToCheck.Count == 0)
                    yield break;

                var nextPoints = new List<Point>();

                foreach (var point in pointsToCheck)
                {
                    if (!MapManager.InBounds(point) || !MapManager.Map[point.X, point.Y].ObjectContainer.IsOpaque)
                        continue;

                    yield return point;
                    nextPoints.Add(new Point(new Size(point + direction)));
                }

                distance--;
                pointsToCheck = nextPoints;
            }
        }
    }
}
