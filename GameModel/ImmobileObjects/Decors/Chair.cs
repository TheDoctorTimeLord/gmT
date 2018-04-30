using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Chair : ImmobileObject
    {
        public Chair() : base(true, false, 0, 10, "chair.png") { }
    }
}
