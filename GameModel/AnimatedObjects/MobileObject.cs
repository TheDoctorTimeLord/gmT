using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MapSourse;

namespace GameThief.GameModel
{
    public class MobileObject : ICreature
    {
        protected Point Position;
        public int health { get; private set; }
        public Direction SightDirection;
        public int ThresholdAudibility { get; private set; }
        public int DelicacyHearing { get; private set; }
        public int RangeVisibility { get; private set; }

        protected List<Cell> VisibleCells;
        protected List<NoiseSource> AudibleNoises;


        public void MakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
                AnimatesManager.DeleteCreature(this);
        }

        public Point GetPosition() => Position;

        public void ChangePosition(Point newPosition)
        {
            Position = newPosition;
        }

        public Query GetIntention()
        {
            VisibleCells = MapManager.GetVisibleCells(Position, SightDirection, RangeVisibility);
            AudibleNoises = MapManager.GetAudibleNoises(Position, DelicacyHearing, ThresholdAudibility);
            return GetIntentionOfCreature();
        }

        protected Query GetIntentionOfCreature()
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
