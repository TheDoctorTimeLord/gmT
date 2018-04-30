using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Painting : ImmobileObject
    {
        public Painting() : base(false, false, 0, 10, "painting.png") { }
    }
}
