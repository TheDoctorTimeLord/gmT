using System.Collections.Generic;
using System.Linq;

namespace GameThief.GameModel.InanimateObjects
{
    public class ImmobileObject
    {
        public bool IsSolid { get; set; } = false;
        public int NoiseInsulation { get; set; } = 0;

        public SortedSet<IDecor> Decors = new SortedSet<IDecor>();

        public void InteractWith() => Decors.First().InteractWith();
    }
}
