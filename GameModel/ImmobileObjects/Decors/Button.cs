using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Button : ImmobileObject
    {
        public Button() : base(DecorType.Button, 10, 0, false, false) { }

        public override bool InteractWith(ICreature creature)
        {
            throw new NotImplementedException();
        }
    }
}
