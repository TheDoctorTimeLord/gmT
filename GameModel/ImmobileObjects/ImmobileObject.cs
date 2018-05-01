using System;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects
{
    public class ImmobileObject : IDecor
    {
        private readonly bool isSolid;
        private readonly bool isOpaque;
        private readonly int noiseSuppression;
        private readonly int priority;
        private readonly string imageName;

        public ImmobileObject(bool isSolid, bool isOpaque, int noiseSuppression, int priority, string imageName)
        {
            this.isSolid = isSolid;
            this.isOpaque = isOpaque;
            this.noiseSuppression = noiseSuppression;
            this.priority = priority;
            this.imageName = imageName;
        }

        public virtual bool InteractWith(ICreature creature)
        {
            return false;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is ImmobileObject))
                throw new ArgumentException();

            return -priority.CompareTo(((ImmobileObject)obj).GetPriority());
        }

        public string GetImageName()
        {
            return imageName;
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

        public bool IsOpaque()
        {
            return isOpaque;
        }
    }
}
