using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Lock : ImmobileObject
    {
        public Lock() : base(false, false, 0, 10, "door_lock.png") { }
    }
}
