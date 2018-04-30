using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Vase : ImmobileObject
    {
        public Vase() : base(false, true, 0, 30, "vase.png") { }
    }
}
