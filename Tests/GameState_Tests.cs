using System.Collections.Generic;
using System.Windows.Forms;
using GameThief.GameModel;
using GameThief.GameModel.ImmobileObjects.Items;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MobileObjects;
using GameThief.GameModel.MobileObjects.Creature;
using GameThief.GameModel.ServiceClasses;
using NUnit.Framework;

namespace GameThief.Tests
{
    [TestFixture]
    public class GameState_Tests
    {
        [Test]
        public void TestPlayerInteracting()
        {
            GameSetter.SetSampleMap(5, 5);
            var gameState = new GameState();
            MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature> {gameState.Player});
            MapManager.Map[1, 2].ObjectContainer.AddDecor(new Jewel());

            Assert.True(gameState.Player.Inventory.Items.Count == 0);

            gameState.UpdateState();
            GameState.KeyPressed = Keys.D;
            gameState.UpdateState();
            GameState.KeyPressed = Keys.E;
            gameState.UpdateState();

            Assert.True(gameState.Player.Inventory.Items.Count == 1);
        }

        [Test]
        public void TestPlayerMoving()
        {
            GameSetter.SetSampleMap(5, 5);
            var gameState = new GameState();
            MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature> { gameState.Player });
            gameState.UpdateState();
            GameState.KeyPressed = Keys.W;
            gameState.UpdateState();

            Assert.True(MapManager.Map[2, 1].Creature is Player);
        }

        [Test]
        public void TestPlayerStayingInMap()
        {
            GameSetter.SetSampleMap(5, 5);
            var gameState = new GameState();
            MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature> { gameState.Player });
            gameState.UpdateState();
            GameState.KeyPressed = Keys.A;
            gameState.UpdateState();
            GameState.KeyPressed = Keys.W;
            gameState.UpdateState();
            GameState.KeyPressed = Keys.W;
            gameState.UpdateState();

            Assert.True(MapManager.Map[1, 0].Creature is Player);
        }
    }
}