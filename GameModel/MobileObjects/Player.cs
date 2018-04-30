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

        protected new Query GetIntentionOfCreature()
        {
            return GameState.GetCurrentQuery();
        }

        public new void ActionTaken(Query query)
        {
        }

        public new void ActionRejected(Query query)
        {
        }

        public void Interative(ICreature creature)
        {
        }

    }
}
