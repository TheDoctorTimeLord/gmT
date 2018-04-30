using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class ClosedCupboard : ImmobileObject
    {
        public ClosedCupboard() : base(true, true, 0, 20, "closed_cupboard.png") { }
    }
}
