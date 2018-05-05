﻿using System.Drawing;
using GameThief.GameModel.Enums;

namespace GameThief.GameModel.MobileObjects
{
    public interface ICreature
    {//сделать свойства
       // Point Position { get; set; }
        Point GetPosition();
        void ChangePosition(Point newPosition);
        Direction GetDirection();
        void ChangeDirection(Direction direction);
        bool IsHidden();
        Inventory GetInventory();

        Query GetIntention();
        void ActionTaken(Query query);
        void ActionRejected(Query query);

        void Interative(ICreature creature);
    }
}
