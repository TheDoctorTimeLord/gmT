using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void TestRemovingDeadObjects()
        {
            SampleMapSetter.SetSampleMap(3, 3);
            TemporaryObjectsManager.AddTemporaryObject(new NoiseSource
                (NoiseType.Cat, 1, 3, new Point(0, 0), "Meow"));
            TemporaryObjectsManager.UpdateTemporaryObjects();
            Assert.IsEmpty(MapManager.GetAudibleNoises(new Point(), 10, 0));
        }

        [Test]
        public void TestRemovingOnlyDeadObjects()
        {
            SampleMapSetter.SetSampleMap(3, 3);
            TemporaryObjectsManager.AddTemporaryObject(new NoiseSource
                (NoiseType.Cat, 1, 10, new Point(0, 0), "Meow"));
            TemporaryObjectsManager.AddTemporaryObject(new NoiseSource
                (NoiseType.Cat, 10, 10, new Point(0, 1), "Meow meow"));
            TemporaryObjectsManager.UpdateTemporaryObjects();
            Assert.IsNotEmpty(MapManager.GetAudibleNoises(new Point(0, 1), 10, 0));
        }
    }
}
