using GameThief.GameModel.ServiceClasses;
using NUnit.Framework;

namespace GameThief.Tests
{
    [TestFixture]
    public class Deque_Tests
    {
        [Test]
        public void TestPeekFrontBack()
        {
            var deque = new Deque<int>();
            deque.PushFront(1);
            deque.PushFront(2);
            Assert.AreEqual(2, deque.PeekFront());
            Assert.AreEqual(1, deque.PeekBack());
            Assert.AreEqual(2, deque.Count);
        }

        [Test]
        public void TestPushPopBack()
        {
            var deque = new Deque<int>();
            deque.PushBack(1);
            deque.PushBack(2);
            deque.PushBack(3);
            Assert.AreEqual(3, deque.PopBack());
            Assert.AreEqual(2, deque.PopBack());
            Assert.AreEqual(1, deque.PopBack());
            Assert.AreEqual(0, deque.Count);
        }

        [Test]
        public void TestPushPopFront()
        {
            var deque = new Deque<int>();
            deque.PushFront(1);
            deque.PushFront(2);
            deque.PushFront(3);
            Assert.AreEqual(3, deque.PopFront());
            Assert.AreEqual(2, deque.PopFront());
            Assert.AreEqual(1, deque.PopFront());
            Assert.AreEqual(0, deque.Count);
        }
    }
}