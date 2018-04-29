using System.Collections.Generic;
using System.Linq;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects
{
    public class ObjectsContainer
    {
        public bool IsSolid { get; set; } = false;
        public bool IsTransparent { get; set; } = true;
        public int TotalNoiseSuppression { get; set; } = 0;

        public SortedSet<IDecor> Decors = new SortedSet<IDecor>();

        public void AddDecor(IDecor decor)
        {
            IsSolid = IsSolid || decor.IsSolid();
            IsTransparent = IsTransparent || decor.IsTransparent();
            TotalNoiseSuppression += decor.GetNoiseSuppression();
            Decors.Add(decor);
        }

        public void RemoveDecor(IDecor decor)
        {
            Decors.Remove(decor);
            TotalNoiseSuppression -= decor.GetNoiseSuppression();

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
