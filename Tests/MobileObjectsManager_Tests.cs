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
    public class MobileObjectsManager_Tests
    {
        [Test]
        public void TestCreatingCreature()
        {
            GameSetter.SetSampleMap(2, 2);
            MobileObjectsManager.CreateCreature(
                new Player(new MobileObjectInitialization(new Point(0, 0), Direction.Down)));
            MobileObjectsManager.UpdateAnimates();
            Assert.True(MapManager.Map[0, 0].Creature is Player);
        }

        [Test]
        public void TestInitialization()
        {
            GameSetter.SetSampleMap(2, 2);
            MobileObjectsManager.InitializationMobileOjects(new HashSet<ICreature>
            {
                new Player(new MobileObjectInitialization(new Point(0, 0), Direction.Down))
            });
            Assert.True(MapManager.Map[0, 0].Creature is Player);
        }

        [Test]
        public void TestRemovingCreature()
        {
            GameSetter.SetSampleMap(2, 2);
            var player = new Player(new MobileObjectInitialization(new Point(0, 0), Direction.Down));
            MobileObjectsManager.CreateCreature(player);
            MobileObjectsManager.UpdateAnimates();
            Assert.True(MapManager.Map[0, 0].Creature is Player);
            MobileObjectsManager.DeleteCreature(player);
            Assert.True(MapManager.Map[0, 0].Creature == null);
        }
    }
}