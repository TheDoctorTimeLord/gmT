using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Button : ImmobileObject
    {
        public Button() : base(false, true, 10, 0, "button.png") { }

        public override bool InteractWith(ICreature creature)
        {
            throw new NotImplementedException();
        }
    }
}
