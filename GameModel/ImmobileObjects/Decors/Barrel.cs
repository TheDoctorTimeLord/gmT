using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Barrel : ImmobileObject
    {
        public Barrel() : base(DecorType.Barrel, 10, 0, true, false) { }
    }
}
