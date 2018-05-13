using System.Drawing;
using GameThief.GameModel;
using GameThief.GameModel.Enums;
using GameThief.GameModel.ImmobileObjects;
using GameThief.GameModel.ImmobileObjects.Decors;
using GameThief.GameModel.ImmobileObjects.Items;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MobileObjects.Creature;
using GameThief.GameModel.ServiceClasses;
using NUnit.Framework;

namespace GameThief.Tests
{
    [TestFixture]
    public class ObjectsContainer_Tests
    {
        [Test]
        public void TestDecorsOrded()
        {
            var container = new ObjectsContainer();
            container.AddDecor(new PaintingFlowers());
            container.AddDecor(new Wall());
            Assert.True(container.ShowDecor() is PaintingFlowers);
        }

        //[Test]
        //public void TestInteractWithRemoval()
        //{
        //    GameSetter.SetSampleMap(3, 3);
        //    var container = MapManager.Map[1, 1].ObjectContainer;
        //    container.AddDecor(new Table());
        //    container.AddDecor(new Vase());
        //    Assert.True(container.ShowDecor() is Vase);
        //    var player = new Player(new MobileObjectInitialization(new Point(0, 1), Direction.Right));
        //    MobileObjectsManager.CreateCreature(player);
        //    MobileObjectsManager.UpdateAnimates();
        //    container.Interact(MapManager.Map[0,1].Creature);
        //    Assert.True(container.ShowDecor() is Table);
        //}

        [Test]
        public void TestInteractWithNoRemoval()
        {
            GameSetter.SetSampleMap(3, 3);
            var container = MapManager.Map[1, 1].ObjectContainer;
            container.AddDecor(new Table());
            Assert.True(container.ShowDecor() is Table);
            var player = new Player(new MobileObjectInitialization(new Point(0, 1), Direction.Right));
            MapManager.AddCreatureToMap(player);
            container.Interact(player);
            Assert.True(container.ShowDecor() is Table);
        }

        [Test]
        public void TestNotSolid()
        {
            var container = new ObjectsContainer();
            container.AddDecor(new Carpet());
            Assert.False(container.IsSolid);
        }

        [Test]
        public void TestOpacity()
        {
            var container = new ObjectsContainer();
            container.AddDecor(new Wall());
            container.AddDecor(new PaintingFlowers());
            Assert.True(container.IsOpaque);
        }

        [Test]
        public void TestSingleDecor()
        {
            var container = new ObjectsContainer();
            container.AddDecor(new Chair());
            Assert.True(container.ShowDecor() is Chair);
        }

        [Test]
        public void TestSolid()
        {
            var container = new ObjectsContainer();
            container.AddDecor(new Chair());
            Assert.True(container.IsSolid);
        }

        [Test]
        public void TestTransparency()
        {
            var container = new ObjectsContainer();
            container.AddDecor(new Table());
            container.AddDecor(new Vase());
            Assert.False(container.IsOpaque);
        }
    }
}