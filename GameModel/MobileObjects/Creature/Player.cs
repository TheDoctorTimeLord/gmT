using GameThief.GameModel.Enums;
using GameThief.GameModel.Managers;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel.MobileObjects.Creature
{
    public class Player : MobileObject
    {
        public Player(InitializationMobileObject init) : base(init)
        {
        }

        protected override Query GetIntentionOfCreature()
        {
            return GameState.GetCurrentQuery();
        }

        public override void ActionTaken(Query query)
        {
            UpdateWorldData();
        }

        public override void ActionRejected(Query query)
        {
        }

        public override void Interative(ICreature creature)
        {
            if (creature is Guard)
            {
                MobileObjectsManager.DeleteCreature(this);
            }
        }
    }
}
