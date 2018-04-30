using System;
using System.Collections.Generic;
using System.Drawing;
using GameThief.GameModel.Enums;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MapSource;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel.MobileObjects
{
    public class MobileObject : ICreature
    {
        public MobileObject(InitializationMobileObject init)
        {
            if (init.IsDefaultInitialization)
                GenerateRandomMobileObject();
            else
            {
                Position = init.Position;
                Health = init.Health;
                MaxHealth = init.MaxHealth;
                SightDirection = init.Direction;
                MinHearingVolume = init.MinHearingVolume;
                MaxHearingDelta = init.MaxHearingDelta;
                ViewDistanse = init.ViewDistanse;
                ViewWidth = init.ViewWidth;
            }
        }

        protected Point Position;
        public int MaxHealth { get; private set; }
        public int Health { get; private set; }
        public Direction SightDirection;
        public int MinHearingVolume { get; private set; }
        public int MaxHearingDelta { get; private set; }
        public int ViewDistanse { get; private set; }
        public int ViewWidth { get; private set; }

        public List<Point> VisibleCells;
        public List<Noise> AudibleNoises;


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
            VisibleCells = MapManager.GetVisibleCells(Position, SightDirection, ViewWidth, ViewDistanse);
            AudibleNoises = MapManager.GetAudibleNoises(Position, MaxHearingDelta, MinHearingVolume);
            return GetIntentionOfCreature();
        }

        protected virtual Query GetIntentionOfCreature()
        {
            throw new NotImplementedException();
        }

        public virtual void ActionTaken()
        {
            throw new NotImplementedException();
        }

        public virtual void ActionRejected()
        {
            throw new NotImplementedException();
        }

        public virtual void Interative(ICreature creature)
        {
            throw new NotImplementedException();
        }
    }
}
