using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Mirror : ImmobileObject
    {
        public Mirror() : base(DecorType.Mirror, 30, 0, true, false) { }
    }
}
