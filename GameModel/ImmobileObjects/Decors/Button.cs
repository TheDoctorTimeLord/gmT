using System;
using GameThief.GameModel.Enums;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Button : ImmobileObject
    {
        public Button() : base(DecorType.Button, 10, 0, false, false)
        {
        }

        public override bool InteractWith(ICreature creature)
        {
            throw new NotImplementedException();
        }
    }
}