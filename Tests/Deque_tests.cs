using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameThief.GameModel.ServiceClasses;
using NUnit.Framework;

namespace GameThief.Tests
{
    [TestFixture]
    public class Deque_tests
    {
        public void CheckResult<T>(Deque<T> result, List<T> answer)
        {
            var index = 0;
            Assert.AreEqual(result.Count, answer.Count);
            foreach (var res in result)
            {
                Assert.AreEqual(res, answer[index++]);
            }
        }

        [Test]
        public void EmptyDeque()
        {
            var deque = new Deque<int>();
            var answer = new List<int>();
            CheckResult(deque, answer);
        }

        [Test]
        public void PushBack()
        {
            var deque = new Deque<int>();
            deque.PushBack(1);
            deque.PushBack(2);
            deque.PushBack(3);
            var answer = new List<int> {1, 2, 3};
            CheckResult(deque, answer);
        }

        [Test]
        public void PushFront()
        {
            var deque = new Deque<int>();
            deque.PushFront(1);
            deque.PushFront(2);
            deque.PushFront(3);
            var answer = new List<int> { 3, 2, 1 };
            CheckResult(deque, answer);
        }

        [Test]
        public void PopBack()
        {
            var deque = new Deque<int>();
            deque.PushFront(1);
            deque.PushFront(2);
            deque.PushFront(3);
            deque.PopBack();
            deque.PopBack();
            var answer = new List<int> { 3 };
            CheckResult(deque, answer);
        }

        [Test]
        public void PopFront()
        {
            var deque = new Deque<int>();
            deque.PushFront(1);
            deque.PushFront(2);
            deque.PushFront(3);
            deque.PopFront();
            deque.PopFront();
            var answer = new List<int> { 1 };
            CheckResult(deque, answer);
        }

        [Test]
        public void EmptyDequeBeforePushAndPop()
        {
            var deque = new Deque<int>();
            deque.PushFront(1);
            deque.PopFront();
            deque.PushFront(2);
            deque.PopFront();
            var answer = new List<int>();
            CheckResult(deque, answer);
        }
    }
}
