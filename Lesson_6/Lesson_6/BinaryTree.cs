using System;

namespace Lesson_6
{
    class BinaryTree
    {
        public Node Root { get; private set; }
        public int CountNodes { get; private set; }
        public int Height { get; private set; }

        public BinaryTree(Node root)
        {
            Root = root;
            CountNodes = 1;
            Height = 1;
        }

        public void AddNode(Node newNode, Node nodeInTree = null, int depth = 2)
        {
            if (nodeInTree == null) { nodeInTree = Root; depth = 2; CountNodes++; }
            if (newNode.Data > nodeInTree.Data)
            {
                if (nodeInTree.Right != null) { AddNode(newNode, nodeInTree.Right, depth + 1); }
                else
                {
                    nodeInTree.Right = newNode;
                    newNode.Parent = nodeInTree;
                    if (depth > Height) { Height = depth; }
                }
            }
            if (newNode.Data <= nodeInTree.Data)
            {
                if (nodeInTree.Left != null) { AddNode(newNode, nodeInTree.Left, depth + 1); }
                else
                {
                    nodeInTree.Left = newNode;
                    newNode.Parent = nodeInTree;
                    if (depth > Height) { Height = depth; }
                }
            }
        }


        public void Print(Node node = null, int depth = 1, int count = 1, int xCursor = 0, int yCursor = 0)
        {
            if (node == null)
            {
                node = Root;
                depth = 1;
                count = 1;
                xCursor = Console.CursorLeft;
                yCursor = Console.CursorTop;
            }
            Console.CursorTop = yCursor + depth - 1;

            if (Height == depth)
            {
                Console.CursorLeft = xCursor + (count - (1 << (depth - 1))) * 4;
            }
            else
            {
                Console.CursorLeft = xCursor + (int)(((double)(1 << (Height - depth - 1)) - 0.5) * 4)
                    + (1 << (Height - depth)) * (count - (1 << (depth - 1))) * 4;
            }
            Console.Write($"[{node.Data,2}]");

            if (node.Left != null) { Print(node.Left, depth + 1, count * 2, xCursor, yCursor); }
            if (node.Right != null) { Print(node.Right, depth + 1, count * 2 + 1, xCursor, yCursor); }
            if (node == Root) { Console.CursorLeft = xCursor; Console.CursorTop = yCursor + Height + 1; }
        }

        public int MaxHeightOfNode(Node node)
        {
            if (node.Left != null && node.Right != null)
            {
                int a = MaxHeightOfNode(node.Left);
                int b = MaxHeightOfNode(node.Right);
                return a > b ? a + 1 : b + 1;
            }
            if (node.Left != null && node.Right == null) { return 1 + MaxHeightOfNode(node.Left); }
            if (node.Right != null && node.Left == null) { return 1 + MaxHeightOfNode(node.Right); }
            return 1;
        }

        public int CountOfNode(Node node)
        {
            if (node.Left != null && node.Right == null) { return 1 + MaxHeightOfNode(node.Left); }
            if (node.Right != null && node.Left == null) { return 1 + MaxHeightOfNode(node.Right); }
            return 1;
        }

        public void RoteteRight(Node node)
        {
            Node left = node.Left;
            if (node.Parent != null)
            {
                if (node.Parent.Left == node) { node.Parent.Left = left; }
                else if (node.Parent.Right == node) { node.Parent.Right = left; }
            }
            else { Root = left; }
            left.Parent = node.Parent;
            node.Parent = left;
            node.Left = left.Right;
            left.Right = node;
        }

        public void RoteteLeft(Node node)
        {
            Node right = node.Right;
            if (node.Parent != null)
            {
                if (node.Parent.Left == node) { node.Parent.Left = right; }
                else if (node.Parent.Right == node) { node.Parent.Right = right; }
            }
            else { Root = right; }
            right.Parent = node.Parent;
            node.Parent = right;
            node.Right = right.Left;
            right.Left = node;
        }

        public void BalanceTree(Node node = null, int count = 0)
        {
            bool flag = false;
            if (node == null) { node = Root; flag = true; }
            while (true)
            {
                int heightLeft = node.Left == null ? 0 : MaxHeightOfNode(node.Left);
                int heightRight = node.Right == null ? 0 : MaxHeightOfNode(node.Right);
                int countLeft = node.Left == null ? 0 : CountOfNode(node.Left);
                int countRight = node.Right == null ? 0 : CountOfNode(node.Right);

                if (heightLeft > heightRight || (countLeft - countRight) > 1)
                { RoteteRight(node); }
                else if (heightLeft < heightRight || (countRight - countLeft) > 1)
                { RoteteLeft(node); }
                else { break; }
            }
            if (node.Left != null) { BalanceTree(node.Left); }
            if (node.Right != null) { BalanceTree(node.Right); }

            if (flag == true)
            {
                int differenceHeight = Math.Abs((Root.Left == null ? 0 : MaxHeightOfNode(Root.Left)) - (Root.Right == null ? 0 : MaxHeightOfNode(Root.Right)));
                int differenceCount = Math.Abs((node.Left == null ? 0 : CountOfNode(node.Left)) - (node.Right == null ? 0 : CountOfNode(node.Right)));
                if (differenceHeight > 1 || differenceCount > 1) { BalanceTree(null); }
                if (differenceHeight <= 1 && differenceCount <= 1 && count < 2) { BalanceTree(null, count + 1); }
                if (differenceHeight <= 1 && differenceCount <= 1 && count >= 2)
                { Height = MaxHeightOfNode(Root); }
            }
            return;
        }

        private int BinarySearch(int data, Node node = null, int index = 1)
        {
            if (node == null) { node = Root; index = 1; }
            if (data == node.Data) { return index; }
            if (data > node.Data)
            {
                if (node.Right != null) { return BinarySearch(data, node.Right, index * 2 + 1); }
                else { return 0; }
            }
            if (data < node.Data)
            {
                if (node.Left != null) { return BinarySearch(data, node.Left, index * 2); }
                else { return 0; }
            }
            return 0;
        }

        public void SearchInterface()
        {
            int data;
            while (true)
            {
                Console.WriteLine("Укажите значение, которое нужно найти:");
                if (int.TryParse(Console.ReadLine(), out data)) { break; }
                else { Console.WriteLine("Указано некорректное значение, попробуйте ещё раз..."); }
            }
            int index = BinarySearch(data);
            int str = (int)Math.Log(index, 2) + 1;
            int pos = index - (1 << (str - 1)) + 1;

            if (index != 0)
            {
                Console.WriteLine($"Элемент находится в {str} строке, на {pos} позиции слева, которые есть в этой строке.");
            }
            else { Console.WriteLine("Такой элемент отсутствует в нашем дереве."); }
        }
    }
}
