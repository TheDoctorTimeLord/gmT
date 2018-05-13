using System;
using GameThief.GameModel.Enums;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects
{
    public abstract class ImmobileObject : IDecor
    {
        protected ImmobileObject(DecorType type, int priority, int noiseSuppression, bool isSolid, bool isOpaque)
        {
            Type = type;
            Priority = priority;
            NoiseSuppression = noiseSuppression;
            IsSolid = isSolid;
            IsOpaque = isOpaque;
        }

        public DecorType Type { get; }
        public int Priority { get; }
        public int NoiseSuppression { get; }
        public bool IsSolid { get; }
        public bool IsOpaque { get; }

        public virtual bool InteractWith(ICreature creature)
        {
            return false;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is ImmobileObject))
                throw new ArgumentException();

            return -Priority.CompareTo(((ImmobileObject) obj).Priority);
        }
    }
}