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
            int minHearingVolume, int maxHearingDelta, int fieldOfView, List<Tuple<string, string>> parameters)
        {
            IsDefaultInitialization = false;
            Position = position;
            MaxHealth = maxHealth;
            Health = health;
            Direction = direction;
            MinHearingVolume = minHearingVolume;
            MaxHearingDelta = maxHearingDelta;
            FieldOfView = fieldOfView;
            Parameters = parameters;
        }

        public bool IsDefaultInitialization;

        public Point Position;
        public int MaxHealth;
        public int Health;
        public Direction Direction;
        public int MinHearingVolume;
        public int MaxHearingDelta;
        public int FieldOfView;

        public List<Tuple<string, string>> Parameters;
    }
}
