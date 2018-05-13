using System;
using System.Collections.Generic;

namespace GameThief.GameModel.ServiceClasses
{
    public class Instruction
    {
        private int index;
        private readonly List<string> parameters;

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

        public void ResetInstruction()
        {
            index = 0;
        }
    }
}