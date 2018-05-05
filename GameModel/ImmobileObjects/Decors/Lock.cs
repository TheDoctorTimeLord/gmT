using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Lock : ImmobileObject
    {
        public Lock() : base(DecorType.Lock, 20, 0, false, false) { }

        public override bool InteractWith(ICreature creature)
        {
            return true;
        }
    }
}
