using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Treasure : ImmobileObject
    {
        public Treasure() : base(false, true, 0, 30, "treasure.png") { }
    }
}
