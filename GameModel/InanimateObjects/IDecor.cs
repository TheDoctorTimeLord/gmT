using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.AnimatedObjects;

namespace GameThief.GameModel.InanimateObjects
{
    public interface IDecor : IComparable
    {
        int GetPriority();
        int GetNoiseInsulation();
        bool IsSolid();
        bool IsTransparent();
        bool InteractWith(ICreature creature);
    }
}
