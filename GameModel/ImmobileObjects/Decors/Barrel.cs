using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Barrel : ImmobileObject
    {
        public Barrel() : base(DecorType.Barrel, 10, 0, true, false)
        {
        }
    }
}