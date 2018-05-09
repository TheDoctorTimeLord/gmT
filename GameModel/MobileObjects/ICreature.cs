using System.Drawing;
using GameThief.GameModel.Enums;

namespace GameThief.GameModel.MobileObjects
{
    public interface ICreature
    {
        CreatureTypes Type { get; set; }
        Point Position { get; set; }
        Direction Direction { get; set; }
        bool IsHidden();
        Inventory Inventory { get; }

        Query GetIntention();
        void ActionTaken(Query query);
        void ActionRejected(Query query);

        void Interative(ICreature creature);
    }
}
