using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameThief.GameModel;
using GameThief.GameModel.Enums;
using NUnit.Framework;

namespace GameThief.Tests
{
    [TestFixture]
    public class ConverterPressedKey_Tests
    {
        [Test]
        public void TestConverter()
        {
            ConverterPressedKey.CreateConverter(new Dictionary<Keys, Query>
            {
                {Keys.D, Query.RotateRight},
                {Keys.W, Query.Move},
                {Keys.A, Query.RotateLeft},
                {Keys.E, Query.Interaction}
            });

            Assert.AreEqual(Query.RotateRight, ConverterPressedKey.Convert(Keys.D));
            Assert.AreEqual(Query.Move, ConverterPressedKey.Convert(Keys.W));
            Assert.AreEqual(Query.RotateLeft, ConverterPressedKey.Convert(Keys.A));
            Assert.AreEqual(Query.Interaction, ConverterPressedKey.Convert(Keys.E));
        }

        [Test]
        public void TestChangingMatching()
        {
            ConverterPressedKey.CreateConverter(new Dictionary<Keys, Query>
            {
                {Keys.D, Query.RotateRight},
                {Keys.W, Query.Move},
                {Keys.A, Query.RotateLeft},
                {Keys.E, Query.Interaction}
            });

            Assert.AreEqual(Query.Move, ConverterPressedKey.Convert(Keys.W));
            ConverterPressedKey.ChangeMatching(Keys.Up, Query.Move);
            Assert.AreEqual(Query.Move, ConverterPressedKey.Convert(Keys.Up));
        }
    }
}
