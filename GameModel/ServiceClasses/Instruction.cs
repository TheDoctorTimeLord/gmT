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
        private List<string> parameters;
        private int index = 0;

        public Instruction(List<string> parameters)
        {
            this.parameters = parameters;
        }

        public string GetNextParameter()
        {
            if (index >= parameters.Count)
                throw new Exception("Неверное обращение с инструкицей или неверная инициализация");
            return parameters[index++];
        }
    }
}
