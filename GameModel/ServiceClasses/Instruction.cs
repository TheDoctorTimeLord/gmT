using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ServiceClasses
{
    public class Instruction
    {
        public ActionAiType InstructionType;
        public Point Position;
        public int Duration;
    }
}
