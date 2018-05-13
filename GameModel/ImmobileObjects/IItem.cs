using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ImmobileObjects
{
    public interface IItem
    {
        int Price { get; }
        DecorType Type { get; }
    }
}