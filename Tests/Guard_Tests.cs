using System;
using System.Collections.Generic;
using System.Drawing;
using GameThief.GameModel;
using GameThief.GameModel.Enums;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MobileObjects;
using GameThief.GameModel.MobileObjects.Creature;
using GameThief.GameModel.ServiceClasses;
using NUnit.Framework;

namespace GameThief.Tests
{
    [TestFixture]
    public class Guard_Tests
    {
        [Test]
        public void TestNormalSituation()
        {
            GameSetter.SetSampleMap(11, 11);
            var gameState = new GameState();

            GameInformationManager.CreateTrackByName(new Dictionary<string, List<Instruction>>
            {
                {
                    "guardTrack", new List<Instruction>
                    {
                        new Instruction(new List<string> {"MoveTo", "10", "9"})
                    }
                }
            });

            var guard = new Guard(new MobileObjectInitialization(new Point(10, 10), 10, 10, Direction.Down, 1, 4, 4, 3,
                new Inventory(10), new List<Tuple<string, string>> {Tuple.Create("path", "guardTrack")}));

            MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature> {gameState.Player, guard});
            gameState.UpdateState();
            gameState.UpdateState();
            gameState.UpdateState();

            Assert.True(MapManager.Map[10, 9].Creature is Guard);
        }
    }
}