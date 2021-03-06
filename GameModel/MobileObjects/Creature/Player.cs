﻿using GameThief.GameModel.Enums;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MapSource;
using GameThief.GameModel.ServiceClasses;

namespace GameThief.GameModel.MobileObjects.Creature
{
    public class Player : MobileObject
    {
        public Player(MobileObjectInitialization init) : base(init) { }

        public override CreatureTypes Type { get; set; } = CreatureTypes.Player;

        protected override Query GetIntentionOfCreature()
        {
            return GameState.GetCurrentQuery();
        }

        public override void ActionTaken(Query query)
        {
            if (query == Query.Move)
                MapManager.AddNoiseSourse(new NoiseSource(NoiseType.StepsOfThief, 1, 2, Position, "S"));
        }

        public override void ActionRejected(Query query) { }

        public override void InteractWith(ICreature creature)
        {
            if (creature is Guard) MobileObjectsManager.DeleteCreature(this);
        }
    }
}