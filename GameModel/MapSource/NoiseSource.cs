using System.Drawing;
using GameThief.GameModel.Enums;
using GameThief.GameModel.Managers;

namespace GameThief.GameModel.MapSource
{
    public class NoiseSource : ITemporaryObject
    {
        private int duration;

        public NoiseSource(NoiseType type, int duration, int maxIntensity, Point position, string message)
        {
            Type = type;
            this.duration = duration;
            MaxIntensity = maxIntensity;
            Position = position;
            Message = message;
        }

        public NoiseType Type { get; }
        public int MaxIntensity { get; set; }
        public Point Position { get; set; }
        public string Message { get; set; }

        public void Update()
        {
            duration--;
        }

        public bool IsActive()
        {
            return duration >= 0;
        }

        public void ActionAfterDeactivation()
        {
            MapManager.NoiseController.RemoveSourceNoises(this);
        }
    }
}