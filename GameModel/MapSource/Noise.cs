using System;

namespace GameThief.GameModel.MapSource
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

        public override bool Equals(object obj)
        {
            if (obj is Noise noise)
                return noise.Source == Source && noise.Intensity == Intensity;
            return false;
        }

        public override int GetHashCode()
        {
            return Source.GetHashCode();
        }
    }
}
