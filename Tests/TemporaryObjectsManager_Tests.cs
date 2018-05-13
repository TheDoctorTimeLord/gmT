using System.Collections.Generic;
using GameThief.GameModel;
using GameThief.GameModel.Managers;
using NUnit.Framework;

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
        private readonly HashSet<SampleTemporaryObject> storage;

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