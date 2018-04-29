using System;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects
{
    public class ImmobileObject : IDecor
    {
        private readonly bool isSolid = false;
        private readonly bool isTransparent = true;
        private readonly int noiseSuppression = 0;
        private readonly int priority = 0;

        public ImmobileObject(bool isSolid, bool isTransparent, int noiseSuppression, int priority)
        {
            this.isSolid = isSolid;
            this.isTransparent = isTransparent;
            this.noiseSuppression = noiseSuppression;
            this.priority = priority;
        }

        public bool InteractWith(ICreature creature)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object obj)
        {
            if (!(obj is ImmobileObject))
                throw new ArgumentException();

            return priority.CompareTo(((ImmobileObject)obj).GetPriority());
        }

        public int GetNoiseSuppression()
        {
            return noiseSuppression;
        }

        public int GetPriority()
        {
            return priority;
        }

        public bool IsSolid()
        {
            return isSolid;
        }

        public bool IsTransparent()
        {
            return isTransparent;
        }
    }
}
