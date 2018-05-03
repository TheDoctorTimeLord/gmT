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
            TestMapSetter.SetSampleMap(3, 3);
            TemporaryObjectsManager.AddTemporaryObject(new NoiseSource
                (NoiseType.Cat, 1, 1, new Point(), "Meow"));
            TemporaryObjectsManager.UpdateTemporaryObjects();
            Assert.IsEmpty(MapManager.GetAudibleNoises(new Point(), 1, 1));
        }
    }
}
