using System;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects
{
    public interface IDecor : IComparable
    {
        string GetImageName();
        int GetPriority();
        int GetNoiseSuppression();
        bool IsSolid();
        bool IsOpaque();
        bool InteractWith(ICreature creature);
    }
}
