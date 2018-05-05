using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Chair : ImmobileObject
    {
        public Chair() : base(DecorType.Chair, 10, 0, true, false) { }
    }
}
