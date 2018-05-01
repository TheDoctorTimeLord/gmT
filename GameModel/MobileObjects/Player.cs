using System;
using System.Collections.Generic;
using System.Drawing;
using GameThief.GameModel.Enums;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel.MobileObjects
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
            switch (query)
            {
                case Query.RotateLeft:
                    SightDirection = GameState.RotateFromTo(SightDirection, true);
                    break;
                case Query.RotateRight:
                    SightDirection = GameState.RotateFromTo(SightDirection, false);
                    break;
            }
        }

        public override void ActionRejected(Query query)
        {
        }

        public override void Interative(ICreature creature)
        {
        }

    }
}
