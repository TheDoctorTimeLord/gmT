using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class ClosedDoor : ImmobileObject
    {
        public ClosedDoor() : base(true, true, 20, 1, "closed_door.png") { }

        public override bool InteractWith(ICreature creature)
        {
            throw new NotImplementedException();
        }
    }
}
