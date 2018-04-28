using System.Collections.Generic;
using System.Linq;
using GameThief.GameModel.AnimatedObjects;

namespace GameThief.GameModel.InanimateObjects
{
    public class ImmobileObject
    {
        public bool IsSolid { get; set; } = false;
        public int NoiseInsulation { get; set; } = 0;

        public SortedSet<IDecor> Decors = new SortedSet<IDecor>();

        public void Interact(ICreature creature)
        {
            var firstItem = Decors.FirstOrDefault();

            if (firstItem != null)
            {
                var toRemove = firstItem.InteractWith(creature);

                if (toRemove)
                    Decors.Remove(firstItem);
            }
        }
    }
}
