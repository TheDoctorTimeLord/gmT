using GameThief.GameModel.Enums;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Lock : ImmobileObject
    {
        public Lock() : base(DecorType.Lock, 20, 0, false, false)
        {
        }

        public override bool InteractWith(ICreature creature)
        {
            return true;
        }
    }
}