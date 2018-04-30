using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ServiceClasses
{
    public class InitializationMobileObject
    {
        public InitializationMobileObject()
        {
            IsDefaultInitialization = true;
        }

        public InitializationMobileObject(Point position, int maxHealth, int health, Direction direction,
            int minHearingVolume, int maxHearingDelta, int viewWidth, int viewDistanse, List<Tuple<string, string>> parameters)
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
        }

        public bool IsDefaultInitialization;

        public Point Position;
        public int MaxHealth;
        public int Health;
        public Direction Direction;
        public int MinHearingVolume;
        public int MaxHearingDelta;
        public int ViewDistanse;
        public int ViewWidth;

        public List<Tuple<string, string>> Parameters;
    }
}
