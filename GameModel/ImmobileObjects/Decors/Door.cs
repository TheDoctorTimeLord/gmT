using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Door : ImmobileObject
    {
        private bool IsClosed;

        public Door() : base(true, false, 10, 10, "door.png")
        {
            IsClosed = true;
        }

        public override bool InteractWith(ICreature creature)
        {
            if (IsClosed)
                return false;

            return true;
        }
    }
}
