using System;

namespace Lesson_7
{
    class Graph
    {
        /// <summary>
        /// Матрица весов, с помощью которой задаётся связь между вершинами графа
        /// </summary>
        private int[,] weightMatrix;

        /// <summary>
        /// Возвращает максимальное значение из весовой матрицы графа
        /// </summary>
        public int MaxWeight
        {
            get
            {
                int max = int.MinValue;
                foreach (var item in weightMatrix)
                {
                    if (max < item) { max = item; }
                }
                return max;
            }
        }

        /// <summary>
        /// Конструктор создания графа, в котором указывается количество его вершин
        /// </summary>
        /// <param name="numberOfVertices">Количество вершин графа</param>
        public Graph(int numberOfVertices)
        {
            weightMatrix = new int[numberOfVertices, numberOfVertices];
        }

        /// <summary>
        /// Задаётся ребро (дугу, если есть направление) между вершинами с указанием веса. 
        /// Если вес = 0, то считаем, что связь между вершинами отсутствует.
        /// </summary>
        /// <param name="firstVertice">Первая вершина</param>
        /// <param name="secondVertice">Вторая вершина</param>
        /// <param name="weight">Вес ребра (дуги) между вершинами</param>
        /// <param name="direction">Это однонаправленная связь?</param>
        public void SetEdge(int firstVertice, int secondVertice, int weight, bool direction = false)
        {
            weightMatrix[firstVertice, secondVertice] = weight;
            if (!direction) { weightMatrix[secondVertice, firstVertice] = weight; }
        }

        /// <summary>
        /// Выводит значение весовой матрицы в консоль
        /// </summary>
        public void PrintMatrix()
        {
            Console.Write("   ");
            for (int i = 0; i < weightMatrix.GetLength(0); i++)
            {
                Console.Write($"{i,2} ");
            }
            Console.WriteLine();
            for (int i = 0; i < weightMatrix.GetLength(1); i++)
            {
                Console.Write($"{i,2} ");
                for (int j = 0; j < weightMatrix.GetLength(0); j++)
                {
                    Console.Write($"{(i == j ? "-" : weightMatrix[i, j].ToString()),2} ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Проверка связанности между двумя вершинами в графе. Возвращает true, если связанность есть.
        /// </summary>
        /// <param name="firstVertice">Первая вершина</param>
        /// <param name="secondVertice">Вторая вершина</param>
        /// <returns>Возвращает true, если связанность есть. И false, если из первой вершины нельза достичь вторую.</returns>
        public bool CheckConnection(int firstVertice, int secondVertice)
        {
            if (firstVertice < 0 || firstVertice > weightMatrix.GetLength(0)
                || secondVertice < 0 || secondVertice > weightMatrix.GetLength(0)) { return false; }
            int[] connection = new int[weightMatrix.GetLength(0)];
            connection[firstVertice] = 1;
            MyStack<int> stack = new MyStack<int>(weightMatrix.GetLength(0));
            int vertice = firstVertice;

            while (true)
            {
                for (int i = 0; i < weightMatrix.GetLength(0); i++)
                {
                    if (connection[i] != 1 && weightMatrix[vertice, i] != 0)
                    {
                        if (i == secondVertice) { return true; }
                        stack.Push(i);
                        connection[i] = 1;
                    }
                }
                if (stack.GetCurrentIndex() == -1) { return false; }
                else { vertice = stack.Pop(); }
            }
        }

        /// <summary>
        /// Вычисляет кротчайший по весам маршрут между вершинами
        /// </summary>
        /// <param name="startVertice">Начальная вершина</param>
        /// <param name="endVertice">Конечная вершина</param>
        /// <returns>Возвращает кротчайший по весам маршрут между вершинами ввиде масива с номерами вершин</returns>
        public int[] MinWeightRoute(int startVertice, int endVertice)
        {
            if (!CheckConnection(startVertice, endVertice)) { return Array.CreateInstance(typeof(int), 0) as int[]; }
            int[] minWeightVertices = MinWeightVertices(startVertice);
            int[] minWeightRoute = new int[minWeightVertices.Length];
            int vertice = endVertice;
            minWeightRoute[minWeightRoute.Length - 1] = endVertice;
            int index = minWeightRoute.Length - 2;
            while (true)
            {
                for (int i = 0; i < minWeightVertices.Length; i++)
                {
                    if (weightMatrix[vertice, i] != 0 && minWeightVertices[i] == minWeightVertices[vertice] - weightMatrix[vertice, i])
                    {
                        minWeightRoute[index] = i;
                        if (i == startVertice)
                        {
                            int[] cutArray = new int[minWeightRoute.Length - index];
                            Array.Copy(minWeightRoute, index, cutArray, 0, minWeightRoute.Length - index);
                            return cutArray;
                        }
                        index--;
                        vertice = i;
                    }
                }
            }
        }

        /// <summary>
        /// Рассчитывает минимальные веса в каждой вершине по алгоритму Дейкстры. 
        /// Возвращает массив с минимальными весами относительно начальной вершины, где индекс элемента - это номер вершины.
        /// </summary>
        /// <param name="startVertice">Начальная вершина для расчёта весов по остальным вершинам</param>
        /// <returns>Возвращает массив с минимальными весами относительно начальной вершины, где индекс элемента - это номер вершины.</returns>
        private int[] MinWeightVertices(int startVertice)
        {
            int[] weightVertices = new int[weightMatrix.GetLength(0)];
            int max = MaxWeight * weightMatrix.GetLength(0);
            for (int i = 0; i < weightVertices.Length; i++)
            {
                weightVertices[i] = max + 1;
            }
            weightVertices[startVertice] = 0;
            MyStack<int> stack = new MyStack<int>(weightMatrix.GetLength(0));
            int vertice = startVertice;
            while (true)
            {
                for (int i = 0; i < weightMatrix.GetLength(0); i++)
                {
                    if (weightMatrix[vertice, i] != 0 && weightVertices[i] > weightVertices[vertice] + weightMatrix[vertice, i])
                    {
                        weightVertices[i] = weightVertices[vertice] + weightMatrix[vertice, i];
                        stack.Push(i);
                    }
                }
                if (stack.GetCurrentIndex() == -1) { return weightVertices; }
                else { vertice = stack.Pop(); }
            }
        }
    }
}
