using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameThief.GameModel.Enums;
using GameThief.GameModel.Managers;

namespace GameThief.GameModel.MapSource
{
    public class LightSource
    {
        public LightSource(int lightDistance, int lightWidth, Point position)
        {
            LightDistance = lightDistance;
            LightWidth = lightWidth;
            Position = position;
        }

        public int LightDistance { get; set; }
        public int LightWidth { get; set; }
        public Point Position { get; set; }

        public IEnumerable<Point> GetScope()
        {
            return GetLightStripe(Direction.Down)
                .Concat(GetLightStripe(Direction.Up))
                .Concat(GetLightStripe(Direction.Left))
                .Concat(GetLightStripe(Direction.Right))
                .Distinct()
                .Where(p => MapManager.InBounds(p));
        }

        private IEnumerable<Point> GetLightStripe(Direction direction)
        {
            var currentDirection = GameState.ConvertDirectionToSize[direction];
            var oppositeDirection = GameState.ConvertDirectionToSize[GameState.RotateFromTo(direction, true)];

            var singleStripe = Enumerable
                .Range(0, LightDistance)
                .Select(num =>
                    new Size(Position + new Size(currentDirection.Width * num, currentDirection.Height * num)))
                .Select(sz => new Point(sz));

            return Enumerable
                .Range(0, LightWidth)
                .SelectMany(num =>
                    singleStripe.SelectMany(p => new[]
                    {
                        new Size(p + new Size(oppositeDirection.Width * num, oppositeDirection.Height * num)),
                        new Size(p - new Size(oppositeDirection.Width * num, oppositeDirection.Height * num))
                    }))
                .Select(sz => new Point(sz))
                .ToArray();
        }
    }
}