﻿using GameThief.GameModel.Enums;

namespace GameThief.GameModel.ImmobileObjects.Decors
{
    public class ClosedCupboard : ImmobileObject
    {
        public ClosedCupboard() : base(DecorType.ClosedCupboard, 20, 0, true, true)
        {
        }
    }
}