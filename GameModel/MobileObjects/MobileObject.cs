using System;
using System.Collections.Generic;
using System.Drawing;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MapSource;

namespace GameThief.GameModel.MobileObjects
{
    public class MobileObject : ICreature
    {
        protected Point Position;
        public int MaxHealth { get; private set; }
        public int Health { get; private set; }
        public Direction SightDirection;
        public int MinHearingVolume { get; private set; }
        public int MaxHearingDelta { get; private set; }
        public int FieldOfView { get; private set; }

        protected List<Cell> VisibleCells;
        protected List<Noise> AudibleNoises;


        public void MakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
                MobileObjectsManager.DeleteCreature(this);
        }

        public void Healing(int healing)
        {
            Health += healing;
            if (Health > MaxHealth)
                Health = MaxHealth;
        }

        public Point GetPosition() => Position;

        public void ChangePosition(Point newPosition)
        {
            Position = newPosition;
        }

        public Direction GetDirection()
        {
            return SightDirection;
        }

        public void ChangeDirection(Direction direction)
        {
            SightDirection = direction;
        }

        public Query GetIntention()
        {
            VisibleCells = MapManager.GetVisibleCells(Position, SightDirection, FieldOfView);
            AudibleNoises = MapManager.GetAudibleNoises(Position, MaxHearingDelta, MinHearingVolume);
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

        public void Interative(ICreature creature)
        {
            throw new NotImplementedException();
        }
    }
}
