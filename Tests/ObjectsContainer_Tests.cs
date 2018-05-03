﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.ImmobileObjects;
using GameThief.GameModel.ImmobileObjects.Decors;
using GameThief.GameModel.ImmobileObjects.Items;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace GameThief.Tests
{
    [TestFixture]
    public class ObjectsContainer_Tests
    {

        [Test]
        public void TestSingleDecor()
        {
            var container = new ObjectsContainer();
            container.AddDecor(new Chair());
            Assert.True(container.ShowDecor() is Chair);
        }

        [Test]
        public void TestDecorsOrded()
        {
            var container = new ObjectsContainer();
            container.AddDecor(new Painting());
            container.AddDecor(new Wall());
            Assert.True(container.ShowDecor() is Painting);
        }

        [Test]
        public void TestTransparency()
        {
            var container = new ObjectsContainer();
            container.AddDecor(new Table());
            container.AddDecor(new Vase());
            Assert.False(container.IsOpaque);
        }

        [Test]
        public void TestOpacity()
        {
            var container = new ObjectsContainer();
            container.AddDecor(new Wall());
            container.AddDecor(new Painting());
            Assert.True(container.IsOpaque);
        }

        [Test]
        public void TestSolid()
        {
            var container = new ObjectsContainer();
            container.AddDecor(new Chair());
            Assert.True(container.IsSolid);
        }

        [Test]
        public void TestNotSolid()
        {
            var container = new ObjectsContainer();
            container.AddDecor(new Carpet());
            Assert.False(container.IsSolid);
        }
    }
}