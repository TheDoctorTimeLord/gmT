using System.Drawing;

namespace GameThief.GameModel.AnimatedObjects
{
    public interface ICreature
    {
        Point GetPosition();
        void ChangePosition(Point newPosition);

        Query GetIntention();
        void ActionTaken(Query query);
        void ActionRejected(Query query);
    }
}
