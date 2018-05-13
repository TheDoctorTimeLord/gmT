using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Mirror : ImmobileObject
    {
        public Mirror() : base(DecorType.Mirror, 30, 0, true, false)
        {
        }
    }
}