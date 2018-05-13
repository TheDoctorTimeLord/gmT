using System;
using System.Collections.Generic;
using System.Drawing;
using GameThief.GameModel.Enums;
using GameThief.GameModel.MobileObjects;

namespace GameThief.GameModel.ServiceClasses
{
    public class MobileObjectInitialization
    {
        public Direction Direction;
        public int Health;
        public Inventory Inventory;

        public bool IsDefaultInitialization;
        public int MaxHealth;
        public int MaxHearingDelta;
        public int MinHearingVolume;

        public List<Tuple<string, string>> Parameters = new List<Tuple<string, string>>();

        public Point Position;
        public int ViewDistanse;
        public int ViewWidth;

        public MobileObjectInitialization(Point position, Direction direction)
        {
            IsDefaultInitialization = true;
            Position = position;
            Direction = direction;
        }

        public MobileObjectInitialization(Point position, int maxHealth, int health, Direction direction,
            int minHearingVolume, int maxHearingDelta, int viewWidth, int viewDistanse, Inventory inventory,
            List<Tuple<string, string>> parameters)
        {
            IsDefaultInitialization = false;
            Position = position;
            MaxHealth = maxHealth;
            Health = health;
            Direction = direction;
            MinHearingVolume = minHearingVolume;
            MaxHearingDelta = maxHearingDelta;
            ViewWidth = viewWidth;
            ViewDistanse = viewDistanse;
            Parameters = parameters;
            Inventory = inventory;
        }
    }
}