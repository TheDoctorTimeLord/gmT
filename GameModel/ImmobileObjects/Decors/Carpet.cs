using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Carpet : ImmobileObject
    {
        public Carpet() : base(false, true, 0, 1, "carpet.png") { }
    }
}
