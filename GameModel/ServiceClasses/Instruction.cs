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
        public AIActionType InstructionType;
        public Point Position;
        public int Duration;

        public Instruction(AIActionType type, Point position, int duration)
        {
            InstructionType = type;
            Position = position;
            Duration = duration;
        }
    }
}
