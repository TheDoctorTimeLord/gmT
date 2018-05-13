using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Carpet : ImmobileObject
    {
        public Carpet() : base(DecorType.Carpet, 1, 0, false, false)
        {
        }
    }
}