using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel
{
    public class NoiseSource : ITimer
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

        public Point[] GetMaxScope(Map map)
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
                .Where(p => p.X >= 0 && p.Y >= 0 && p.X < map.Noises.GetLength(0) && p.Y < map.Noises.GetLength(1))
                .ToArray();
        }

        public void Update()
        {
            duration--;
        }

        public bool IsActive() => duration > 0;

        public void ActionAfterDeactivation()
        {
            NoiseManager.RemoveNoiseSourse(Position, this);
        }
    }
}
