﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameThief.GameModel.Enums;
using GameThief.GameModel.ImmobileObjects;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MapSource;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel.MobileObjects
{
    public abstract class MobileObject : ICreature
    {
        public HashSet<Noise> AudibleNoises;

        protected bool Hidden;

        public List<Point> VisibleCells;

        //public static MobileObject GetDefault(MobileObjectInitialization init)
        //{
        //    return GenerateRandomMobileObject(init.Position, init.Direction);
        //}
        protected MobileObject(MobileObjectInitialization init)
        {
            if (init.IsDefaultInitialization)
            {
                GenerateRandomMobileObject(init.Position, init.Direction);
            }
            else
            {
                Position = init.Position;
                Health = init.Health;
                MaxHealth = init.MaxHealth;
                Direction = init.Direction;
                MinHearingVolume = init.MinHearingVolume;
                MaxHearingDelta = init.MaxHearingDelta;
                ViewDistanse = init.ViewDistanse;
                ViewWidth = init.ViewWidth;
                Inventory = init.Inventory;
            }
        }

        public int MaxHealth { get; private set; }
        public int Health { get; private set; }
        public int MinHearingVolume { get; private set; }
        public int MaxHearingDelta { get; private set; }
        public int ViewDistanse { get; private set; }
        public int ViewWidth { get; private set; }

        public virtual CreatureTypes Type { get; set; }
        public Inventory Inventory { get; private set; }

        public Point Position { get; set; }

        public Direction Direction { get; set; }

        public bool IsHidden()
        {
            return Hidden;
        }

        public Query GetIntention()
        {
            UpdateWorldData();
            return GetIntentionOfCreature();
        }

        public abstract void ActionTaken(Query query);

        public abstract void ActionRejected(Query query);

        public abstract void InteractWith(ICreature creature);

        private void GenerateRandomMobileObject(Point position, Direction direction) //TODO ГЕНЕРАТОР В РАЗРАБОТКЕ
        {
            Position = position;
            Direction = direction;

            MaxHealth = 10;
            Health = 10;
            MaxHearingDelta = 5;
            MinHearingVolume = 1;
            ViewDistanse = 5;
            ViewWidth = 3;

            Inventory = new Inventory(new HashSet<IItem>(), 10);
        }


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

        public void UpdateWorldData()
        {
            VisibleCells = MapManager.GetVisibleCells(Position, Direction, ViewWidth, ViewDistanse).ToList();
            AudibleNoises = MapManager.GetAudibleNoises(Position, MaxHearingDelta, MinHearingVolume);
        }

        protected abstract Query GetIntentionOfCreature();
    }
}