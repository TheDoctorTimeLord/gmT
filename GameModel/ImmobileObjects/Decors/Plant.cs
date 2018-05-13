using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Plant : ImmobileObject
    {
        public Plant() : base(DecorType.Plant, 10, 0, true, false)
        {
        }
    }
}