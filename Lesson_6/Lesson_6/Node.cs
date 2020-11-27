
namespace Lesson_6
{
    /// <summary>
    /// Узел бинарного дерева
    /// </summary>
    class Node
    {
        internal int Data { get; private set; }
        internal Node Left { get; set; }
        internal Node Right { get; set; }
        internal Node Parent { get; set; }

        public Node(int data)
        {
            Data = data;
        }
    }
}
