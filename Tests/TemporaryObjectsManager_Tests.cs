using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel;
using GameThief.GameModel.Enums;
using GameThief.GameModel.Managers;
using GameThief.GameModel.MapSource;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace GameThief.Tests
{
    [TestFixture]
    public class TemporaryObjectsManager_Tests
    {
        [Test]
        public void TestRemovingDeadObject()
        {
            var objects = new HashSet<SampleTemporaryObject>();
            var temp = new SampleTemporaryObject(1, objects);
            objects.Add(temp);
            TemporaryObjectsManager.AddTemporaryObject(temp);
            TemporaryObjectsManager.UpdateTemporaryObjects();
            Assert.IsEmpty(objects);
        }

        [Test]
        public void TestRemovingOnlyDeadObjects()
        {
            var objects = new HashSet<SampleTemporaryObject>();
            var first = new SampleTemporaryObject(1, objects);
            var second = new SampleTemporaryObject(2, objects);
            objects.Add(first);
            objects.Add(second);
            TemporaryObjectsManager.AddTemporaryObject(first);
            TemporaryObjectsManager.AddTemporaryObject(second);
            TemporaryObjectsManager.UpdateTemporaryObjects();
            Assert.IsNotEmpty(objects);
        }
    }

    internal class SampleTemporaryObject : ITemporaryObject
    {
        private int lifeCount;
        private HashSet<SampleTemporaryObject> storage;

        public SampleTemporaryObject(int lifeCount, HashSet<SampleTemporaryObject> storage)
        {
            this.lifeCount = lifeCount;
            this.storage = storage;
        }

        void ITemporaryObject.ActionAfterDeactivation()
        {
            storage.Remove(this);
        }

        bool ITemporaryObject.IsActive()
        {
            return lifeCount > 0;
        }

        void ITemporaryObject.Update()
        {
            lifeCount--;
        }
    }
}
