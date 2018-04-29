﻿using System.Collections.Generic;
using System.Linq;
using GameThief.GameModel.AnimatedObjects;

namespace GameThief.GameModel.InanimateObjects
{
    public class ImmobileObject
    {
        public bool IsSolid { get; set; } = false;
        public bool IsTransparent { get; set; } = true;
        public int NoiseInsulation { get; set; } = 0;

        public SortedSet<IDecor> Decors = new SortedSet<IDecor>();

        public void AddDecor(IDecor decor)
        {
            IsSolid = IsSolid || decor.IsSolid();
            IsTransparent = IsTransparent || decor.IsTransparent();
            NoiseInsulation += decor.GetNoiseInsulation();
            Decors.Add(decor);
        }

        public void RemoveDecor(IDecor decor)
        {
            Decors.Remove(decor);
            NoiseInsulation -= decor.GetNoiseInsulation();

            if (decor.IsSolid())
            {
                IsSolid = false;
                foreach (var dec in Decors)
                    IsSolid = IsSolid || dec.IsSolid();
            }

            if (!decor.IsTransparent())
            {
                IsTransparent = true;
                foreach (var dec in Decors)
                    IsTransparent = IsTransparent || dec.IsTransparent();
            }
        }

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
