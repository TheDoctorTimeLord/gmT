using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Chair : ImmobileObject
    {
        public Chair() : base(DecorType.Chair, 10, 0, true, false)
        {
        }
    }
}