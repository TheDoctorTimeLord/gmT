using System;

namespace GameThief.GameModel.MapSourse
{
    public class Noise : IComparable
    {
        public NoiseSource Source { get; }
        public int Intensity { get; }
        
        public Noise(NoiseSource source, int intensity)
        {
            Source = source;
            Intensity = intensity;
        }
        
        public int CompareTo(object obj)
        {
            if (!(obj is Noise))
                throw new ArgumentException();

            return -Intensity.CompareTo(((Noise)obj).Intensity);
        }
    }
}
