//Назаров Р.
using System;
using System.Collections.Generic;

namespace Lesson_3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<BaseMenuItem> Menu = new List<BaseMenuItem>();
            Menu.Add(new BaseMenuItem("Exit", () => { Console.WriteLine("\n\nBye!"); }));
            Menu.Add(new BaseMenuItem("Optimized bubble sort", new Action(Task1)));
            Menu.Add(new BaseMenuItem("Shaker sort", new Action(Task2)));
            Menu.Add(new BaseMenuItem("Binary serch", new Action(Task3)));

            int task;

            do
            {
                ShowMenu(Menu);
                task = GetTask(Menu.Count);
                Menu[task].DoMenuAction();
            }
            while (task != 0);

            Console.ReadKey();
        }


        static void ShowMenu(List<BaseMenuItem> menuItems)
        {
            Console.Clear();

            for (int i = 0; i < menuItems.Count; i++)
            {
                Console.WriteLine($"{i}: {menuItems[i].MenuItemText}");
            }
        }

        /// <summary>
        /// Выбор задачи
        /// </summary>
        /// <returns></returns>
        static int GetTask(int itemsCount)
        {
            string selection = Console.ReadKey().KeyChar.ToString();
            int res = 0;

            if (!int.TryParse(selection, out res))
            {
                Console.WriteLine("Incorrect input!");
                return GetTask(itemsCount);
            }

            if (res < 0 || res > itemsCount)
            {
                Console.WriteLine("Incorrect input!");
                return GetTask(itemsCount);
            }

            return res;
        }

        /// <summary>
        /// 1. Попробовать оптимизировать пузырьковую сортировку. Сравнить количество операций сравнения оптимизированной и не оптимизированной программы.
        /// Написать функции сортировки, которые возвращают количество операций.
        /// </summary>
        static void Task1()
        {
            Console.WriteLine("\n");

            int[] array = GetRandomArray(20);
            int[] array2 = array.Clone() as int[];

            Console.WriteLine("Initial array:");
            foreach (var item in array)
            {
                Console.Write($"{item}\t");
            }
            Console.WriteLine();

            var metrics1 = BubbleSort(ref array);
            var metrics2 = BubbleSortOpt(ref array2);

            Console.WriteLine("Sorted:");
            foreach (var item in array)
            {
                Console.Write($"{item}\t");
            }
            Console.WriteLine($"metrics: compares = {metrics1.Compares}, swaps = {metrics1.Swaps}\n");

            Console.WriteLine("Sorted 2:");
            foreach (var item in array2)
            {
                Console.Write($"{item}\t");
            }
            Console.WriteLine($"metrics: compares = {metrics2.Compares}, swaps = {metrics2.Swaps}");

            Console.ReadKey();
        }

        /// <summary>
        /// 2. *Реализовать шейкерную сортировку.
        /// </summary>
        static void Task2()
        {
            Console.WriteLine("\n");

            int[] array = GetRandomArray(20);
            int[] array2 = array.Clone() as int[];
            int[] array3 = array.Clone() as int[];

            Console.WriteLine("Initial array:");
            foreach (var item in array)
            {
                Console.Write($"{item}\t");
            }
            Console.WriteLine();

            var metrics1 = BubbleSort(ref array);
            var metrics2 = ShakerSort(ref array2);
            var metrics3 = BubbleSortOpt(ref array3);

            Console.WriteLine("Sorted by bubble sort:");
            foreach (var item in array)
            {
                Console.Write($"{item}\t");
            }
            Console.WriteLine($"metrics: compares = {metrics1.Compares}, swaps = {metrics1.Swaps}\n");

            Console.WriteLine("Sorted by shaker sort:");
            foreach (var item in array2)
            {
                Console.Write($"{item}\t");
            }
            Console.WriteLine($"metrics: compares = {metrics2.Compares}, swaps = {metrics2.Swaps}");
            Console.WriteLine($"optimized bubble metrics: compares = {metrics3.Compares}, swaps = {metrics3.Swaps}");

            Console.ReadKey();
        }

        /// <summary>
        /// 3. Реализовать бинарный алгоритм поиска в виде функции, которой передается отсортированный массив.
        /// Функция возвращает индекс найденного элемента или -1, если элемент не найден.
        /// </summary>
        static void Task3()
        {
            int value;

            Console.WriteLine("\n");

            int[] array = GetRandomArray(20);
            BubbleSortOpt(ref array);

            Console.WriteLine("Initial array:");
            foreach (var item in array)
            {
                Console.Write($"{item}\t");
            }

            Console.WriteLine("\nEnter search value: ");
            var str = Console.ReadLine();
            while (!int.TryParse(str, out value))
            {
                Console.WriteLine("\nIncorrect input.");
                str = Console.ReadLine();
            }
            Console.WriteLine();

            value = BinarySearch(array, value);
            if (value > -1)
                Console.WriteLine($"Index of seached value is {value}");
            else
                Console.WriteLine("Value is not found.");

            Console.ReadKey();
        }

        static void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }

        /// <summary>
        /// Получить массив случайных значений
        /// </summary>
        /// <param name="length"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        static int[] GetRandomArray(int length, int maxValue = 100)
        {
            int[] res = new int[length];

            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                res[i] = rnd.Next(maxValue);
            }

            return res;
        }

        /// <summary>
        /// Обычная сортировка методом пузырька.
        /// </summary>
        /// <param name="intArray"></param>
        /// <returns></returns>
        static SortingMetrics BubbleSort(ref int[] intArray, bool assend = true)
        {
            SortingMetrics res = new SortingMetrics() { Swaps = 0, Compares = 0 };

            for (int i = 0; i < intArray.Length; i++)
            {
                for (int j = 0; j < intArray.Length - 1; j++)
                {
                    res.Compares++;

                    if ((assend && intArray[j] > intArray[j + 1]) || (!assend && intArray[j] < intArray[j + 1]))
                    {
                        Swap(ref intArray[j], ref intArray[j + 1]);
                        res.Swaps++;
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// Оптимизированная сортировка методом пузырька.
        /// </summary>
        /// <param name="intArray"></param>
        /// <returns></returns>
        static SortingMetrics BubbleSortOpt(ref int[] intArray, bool assend = true)
        {
            SortingMetrics res = new SortingMetrics() { Swaps = 0, Compares = 0 };

            int tmp = 0;
            for (int i = 0; i < intArray.Length; i++)
            {
                tmp = 0;
                for (int j = 0; j < intArray.Length - 1 - i; j++)
                {
                    res.Compares++;

                    if ((assend && intArray[j] > intArray[j + 1]) || (!assend && intArray[j] < intArray[j + 1]))
                    {
                        tmp++;
                        Swap(ref intArray[j], ref intArray[j + 1]);
                        res.Swaps++;
                    }
                }

                // Если перестановок не было, массив отсортирован.
                if (tmp == 0)
                    break;
            }

            return res;
        }

        static SortingMetrics ShakerSort(ref int[] intArray, bool assend = true)
        {
            SortingMetrics res = new SortingMetrics() { Swaps = 0, Compares = 0 };

            int tmp = 0;
            int indx;
            for (int i = 0; i < intArray.Length; i++)
            {
                tmp = 0;
                for (int j = 0; j < intArray.Length - 1; j++)
                {
                    res.Compares++;

                    if (i % 2 == 0)
                        indx = intArray.Length - 2 - j;
                    else
                        indx = j;

                    if ((assend && intArray[indx] > intArray[indx + 1]) || (!assend && intArray[indx] < intArray[indx + 1]))
                    {
                        tmp++;
                        Swap(ref intArray[indx], ref intArray[indx + 1]);
                        res.Swaps++;
                    }
                }

                // Если перестановок не было или была только одна, массив отсортирован.
                if (tmp == 0)
                    break;
            }

            return res;
        }


        /// <summary>
        /// Бинарный поиск.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static int BinarySearch(int[] array, int value)
        {
            int index = -1;
            int left = 0, right = array.Length - 1;
            int mid = (left + right) / 2;

            while (array[mid] != value && left <= right)
            {
                if (array[mid] < value)
                    left = mid + 1;
                else
                    right = mid - 1;

                mid = (left + right) / 2;
            }

            if (array[mid] == value)
                index = mid;

            return index;
        }
    }
}
