using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Lock : ImmobileObject
    {
        public Lock() : base(DecorType.Lock, 10, 0, false, false) { }
    }
}
