using System.Drawing;
using System.Linq;
using GameThief.GameModel.Managers;

namespace GameThief.GameModel.MapSourse
{
    public class NoiseSource : ITemporaryObject
    {
        public NoiseType Type { get; }
        private int duration;
        public int MaxIntensity { get; set; }
        public Point Position { get; set; }

        public NoiseSource(NoiseType type, int duration, int maxIntensity, Point position)
        {
            Type = type;
            this.duration = duration;
            MaxIntensity = maxIntensity;
            Position = position;
        }

        public Point[] GetMaxScope()
        {
            return Enumerable
                .Range(-MaxIntensity, MaxIntensity)
                .SelectMany(num => new[]
                {
                    new Point(Position.X + num, Position.Y),
                    new Point(Position.X, Position.Y + num),
                    new Point(Position.X + num, Position.Y + num),
                    new Point(Position.X + num - 1, Position.Y + num),
                    new Point(Position.X + num, Position.Y + num - 1)
                })
                .Distinct()
                .Where(p => p.X >= 0 && p.Y >= 0 && p.X < MapManager.Map.Cells.GetLength(0) && p.Y < MapManager.Map.Cells.GetLength(1))
                .ToArray();
        }

        public void Update()
        {
            duration--;
        }

        public bool IsActive() => duration > 0;

        public void ActionAfterDeactivation()
        {
            MapManager.NoiseController.RemoveSourceNoises(this);
        }
    }
}
