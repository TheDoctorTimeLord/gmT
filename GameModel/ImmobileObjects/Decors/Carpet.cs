using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Carpet : ImmobileObject
    {
        public Carpet() : base(false, false, 0, 1, "carpet.png") { }
    }
}
