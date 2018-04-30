using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class OpenedDoor : ImmobileObject
    {
        public OpenedDoor() : base(false, true, 0, 1, "opened_door.png") { }

        public override bool InteractWith(ICreature creature)
        {
            throw new NotImplementedException();
        }
    }
}
