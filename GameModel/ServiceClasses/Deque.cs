using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameThief.GameModel.ServiceClasses
{
    public class Deque<T>
    {
        private Node left = null;
        private Node right = null;

        public int Count { get; private set; } = 0;

        public void PushFront(T value)
        {
            if (left == null)
            {
                var newNode = new Node {Left = null, Right = null, Value = value};
                left = newNode;
                right = newNode;
            }
            else
            {
                var newLeftNode = new Node {Left = null, Right = left, Value = value};
                left.Left = newLeftNode;
                left = newLeftNode;
            }
            Count++;
        }

        public void PushBack(T value)
        {
            if (right == null)
            {
                var newNode = new Node { Left = null, Right = null, Value = value };
                left = newNode;
                right = newNode;
            }
            else
            {
                var newRightNode = new Node { Left = right, Right = null, Value = value };
                right.Right = newRightNode;
                right = newRightNode;
            }
            Count++;
        }

        public T PeekFront()
        {
            if (left == null)
                throw new ArgumentException("Попытка вызова левого элемента в пустом деке");
            return left.Value;
        }

        public T PeekBack()
        {
            if (right == null)
                throw new ArgumentException("Попытка вызова правого элемента в пустом деке");
            return right.Value;
        }

        public T PopFront()
        {
            if (left == null)
                throw new ArgumentException("Попытка удаления левого элемента в пустом деке");
            var result = left.Value;
            left = left.Right;

            if (left == null)
                right = null;
            else
                left.Left = null;
            
            Count--;

            return result;
        }

        public T PopBack()
        {
            if (right == null)
                throw new ArgumentException("Попытка удаления правого элемента в пустом деке");
            var result = right.Value;
            right = right.Left;

            if (right == null)
                left = null;
            else
                right.Right = null;

            Count--;

            return result;
        }

        private class Node
        {
            public T Value;
            public Node Left;
            public Node Right;
        }
    }
}
