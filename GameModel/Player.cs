using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel
{
    public class Player: AnimatedObject, ICreature
    {
        public Point GetPosition()
        {
            return Position;

        }

        public void ChangePosition(Point newPosition)
        {
            Position = newPosition;
        }

        public Query GetIntention()
        {
            throw new NotImplementedException();
        }

        public void ActionTaken(Query query)
        {
            throw new NotImplementedException();
        }

        public void ActionRejected(Query query)
        {
            throw new NotImplementedException();
        }
    }
}
