using System;

namespace Lesson_7
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph(8);

            graph.SetEdge(0, 1, 4);
            graph.SetEdge(0, 2, 8);
            graph.SetEdge(0, 3, 3);
            graph.SetEdge(1, 5, 6);
            graph.SetEdge(1, 4, 8);
            graph.SetEdge(1, 2, 1);
            graph.SetEdge(2, 4, 2);
            graph.SetEdge(2, 3, 8);
            graph.SetEdge(3, 6, 4);
            graph.SetEdge(4, 5, 2);
            graph.SetEdge(4, 7, 5);
            graph.SetEdge(5, 7, 3);
            graph.SetEdge(6, 7, 2);

            graph.PrintMatrix();

            int startVertice = 6;
            int endVertice = 1;
            Console.WriteLine($"\nПроверка связанности {startVertice} и {endVertice} = {graph.CheckConnection(startVertice, endVertice)}");

            Console.WriteLine($"\nВычисляем кротчайший путь из вершины {startVertice} в вершину {endVertice}");
            int[] minWeightRoute = graph.MinWeightRoute(startVertice, endVertice);

            if (minWeightRoute.Length == 0) { Console.WriteLine("Вершины не связаны между собой."); }
            foreach (var item in minWeightRoute)
            {
                Console.Write($"{item} ");
            }

            Console.ReadKey();
        }
    }
}
