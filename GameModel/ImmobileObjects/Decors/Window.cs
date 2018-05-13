using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class Window : ImmobileObject
    {
        public Window() : base(DecorType.Window, 30, 0, true, false) { }
    }
}
