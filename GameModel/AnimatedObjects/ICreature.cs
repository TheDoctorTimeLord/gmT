using System.Drawing;

namespace GameThief.GameModel.AnimatedObjects
{
    public interface ICreature
    {
        Point GetPosition();
        void ChangePosition(Point newPosition);
        Direction GetDirection();
        void ChangeDirection(Direction direction);

        Query GetIntention();
        void ActionTaken(Query query);
        void ActionRejected(Query query);

        void Interative(ICreature creature);
    }
}
