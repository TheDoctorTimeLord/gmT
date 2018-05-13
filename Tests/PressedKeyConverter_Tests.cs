using System.Collections.Generic;
using System.Windows.Forms;
using GameThief.GameModel;
using GameThief.GameModel.Enums;
using GameThief.GUI;
using NUnit.Framework;

namespace GameThief.Tests
{
    [TestFixture]
    public class PressedKeyConverter_Tests
    {
        [Test]
        public void TestChangingMatching()
        {
            PressedKeyConverter.CreateConverter(new Dictionary<Keys, Query>
            {
                {Keys.D, Query.RotateRight},
                {Keys.W, Query.Move},
                {Keys.A, Query.RotateLeft},
                {Keys.E, Query.Interaction}
            });

            Assert.AreEqual(Query.Move, PressedKeyConverter.Convert(Keys.W));
            PressedKeyConverter.ChangeMatching(Keys.Up, Query.Move);
            Assert.AreEqual(Query.Move, PressedKeyConverter.Convert(Keys.Up));
        }

        [Test]
        public void TestConverter()
        {
            PressedKeyConverter.CreateConverter(new Dictionary<Keys, Query>
            {
                {Keys.D, Query.RotateRight},
                {Keys.W, Query.Move},
                {Keys.A, Query.RotateLeft},
                {Keys.E, Query.Interaction}
            });

            Assert.AreEqual(Query.RotateRight, PressedKeyConverter.Convert(Keys.D));
            Assert.AreEqual(Query.Move, PressedKeyConverter.Convert(Keys.W));
            Assert.AreEqual(Query.RotateLeft, PressedKeyConverter.Convert(Keys.A));
            Assert.AreEqual(Query.Interaction, PressedKeyConverter.Convert(Keys.E));
        }
    }
}