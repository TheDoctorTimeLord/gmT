using System;
using GameThief.GameModel.Enums;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects
{
    public interface IDecor : IComparable
    {
        DecorType Type { get; }
        int Priority { get; }
        int NoiseSuppression { get; }
        bool IsSolid { get; }
        bool IsOpaque { get; }
        bool InteractWith(ICreature creature);
    }
}
