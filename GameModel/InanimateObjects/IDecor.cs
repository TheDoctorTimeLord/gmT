using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel.InanimateObjects
{
    public interface IDecor : IComparable
    {
        int GetPriority();
        void InteractWith();
    }
}
