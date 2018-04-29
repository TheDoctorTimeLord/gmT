using System;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects
{
    public interface IDecor : IComparable
    {
        int GetPriority();
        int GetNoiseSuppression();
        bool IsSolid();
        bool IsTransparent();
        bool InteractWith(ICreature creature);
    }
}
