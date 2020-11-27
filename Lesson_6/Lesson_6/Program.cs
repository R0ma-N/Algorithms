using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_6
{
    class Program
    {
        static void Main(string[] args)
        {
            //2.Переписать программу, реализующее двоичное дерево поиска:
            //a.Добавить в него обход дерева различными способами.
            //b.Реализовать поиск в нём.

            Console.SetBufferSize(2000, 200);

            Random rand = new Random(90);
            Node[] nodeList = new Node[10];
            for (int i = 0; i < nodeList.Length; i++)
            {
                nodeList[i] = new Node(rand.Next(100));
            }

            BinaryTree tree = new BinaryTree(nodeList[0]);
            for (int i = 1; i < nodeList.Length; i++)
            {
                tree.AddNode(nodeList[i]);
            }

            Console.WriteLine($"Кол-во узлов = {tree.CountNodes}, высота = {tree.Height}, корень = {tree.Root.Data}\n");
            tree.Print();

            Console.WriteLine("\n\nБалансируем дерево:");
            tree.BalanceTree();
            Console.WriteLine($"Кол-во узлов = {tree.CountNodes}, высота = {tree.Height}, корень = {tree.Root.Data}\n");
            tree.Print();

            Console.WriteLine("\n\n");
            tree.SearchInterface();


            Console.ReadKey();
        }
    }
}
