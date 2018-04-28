using System.Collections.Generic;

namespace GameThief.GameModel.InanimateObjects
{
    public class ImmobileObject
    {
        public bool IsSolid;
        public int NoiseInsulation;

        public SortedSet<IDecor> Decors;
    }
}
