using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
                GenerateRandomMobileObject(init.Position, init.Direction);
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

        private void GenerateRandomMobileObject(Point position, Direction direction) //TODO ГЕНЕРАТОР В РАЗРАБОТКЕ
        {
            Position = position;
            SightDirection = direction;

            MaxHealth = 10;
            Health = 10;
            MaxHearingDelta = 5;
            MinHearingVolume = 1;
            ViewDistanse = 5;
            ViewWidth = 3;
        }

        protected Point Position;
        public int MaxHealth { get; private set; }
        public int Health { get; private set; }
        public Direction SightDirection;
        public int MinHearingVolume { get; private set; }
        public int MaxHearingDelta { get; private set; }
        public int ViewDistanse { get; private set; }
        public int ViewWidth { get; private set; }
        public Inventory Inventory;

        public List<Point> VisibleCells;
        public HashSet<Noise> AudibleNoises;

        protected bool Hidden;


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

        public bool IsHidden()
        {
           return Hidden;
        }

        public Inventory GetInventory()
        {
            return Inventory;
        }

        public Query GetIntention()
        {
            UpdateWorldData();
            return GetIntentionOfCreature();
        }

        public void UpdateWorldData()
        {
            VisibleCells = MapManager.GetVisibleCells(Position, SightDirection, ViewWidth, ViewDistanse).ToList();
            AudibleNoises = MapManager.GetAudibleNoises(Position, MaxHearingDelta, MinHearingVolume);
        }

        protected virtual Query GetIntentionOfCreature()
        {
            throw new NotImplementedException();
        }

        public virtual void ActionTaken(Query query)
        {
            throw new NotImplementedException();
        }

        public virtual void ActionRejected(Query query)
        {
            throw new NotImplementedException();
        }

        public virtual void Interative(ICreature creature)
        {
            throw new NotImplementedException();
        }
    }
}
