using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
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
    public class Player_test
    {
        [Test]
        public void TestMovePlayer()
        {
            GameSetter.SetSampleMap(3, 3);
            var player = new Player(new InitializationMobileObject(
                new Point(1, 1), Direction.Down));
            MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature> {player});

            var gs = new GameState();
            var move = new List<Keys>
            {
                Keys.W
            };

            foreach (var key in move)
            {
                GameState.KeyPressed = key;
                gs.UpdateState();
            }

            Assert.True(MapManager.Map[1, 2].Creature is Player);
        }

        [Test]
        public void TestMovePlayerMore()
        {
            GameSetter.SetSampleMap(3, 3);
            var player = new Player(new InitializationMobileObject(
                new Point(1, 1), Direction.Down));
            MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature> {player});

            var gs = new GameState();
            var move = new List<Keys>
            {
                Keys.W,
                Keys.A,
                Keys.W,
                Keys.A
            };

            foreach (var key in move)
            {
                GameState.KeyPressed = key;
                gs.UpdateState();
            }

            Assert.True(MapManager.Map[2, 2].Creature is Player);
            Assert.AreEqual(MapManager.Map[2, 2].Creature.Direction, Direction.Up);
        }

        [Test]
        public void TestPlayerCreate()
        {
            GameSetter.SetSampleMap(3, 3);
            var player = new Player(new InitializationMobileObject(
                new Point(1, 1), Direction.Down));
            MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature> {player});
            Assert.True(MapManager.Map[1, 1].Creature is Player);
        }
    }
}