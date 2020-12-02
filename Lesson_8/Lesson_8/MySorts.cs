using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson_8
{
    /// <summary>
    /// Класс для работы с разными методами сортировки и тестирования их производительности
    /// </summary>
    class MySorts
    {
        public delegate void SortDelegate(ref int[] arr, int startIndex = 0, int endIndex = -10, int avarage = 0);

        private List<int[]> arraysForTest; //Хранит список эталнонных массивов, на которых будут тестироваться все сортировки
        private SortDelegate[] sortMethods; //Хранит список методов, которые нужно протестировать
        private int[] time; //Хранит время выполнения в миллисекундах по каждой сортировке при тестировании на масиве с максимальной длинной
        private int[] comparer; //Хранит количество сравнений по каждой сортировке при тестировании на масиве с максимальной длинной
        private int[] swap;  //Хранит количество свопов по каждой сортировке при тестировании на масиве с максимальной длинной


        public static int CountOp { get; private set; }  //Счётчик количества сравнений
        public static int CountSwap { get; private set; }  //Счётчик количества свопов
        public static DateTime StartTime { get; private set; }  //Время запуска метода при тестировании
        public static DateTime FinishTime { get; private set; } //Время окончания работы метода при тестировании
        public int MaxNumberInArray { get; private set; }  //Хранит количество элементов в максимально большом массиве, который используется для тестирования

        /// <summary>
        /// Конструктор класса: создаётся массивы для тестирования, на основании указанного количество элементов
        /// Для тестирования используются 3 варианта массива: обычный со случайными числами, обратно отсортированный и отсортированный
        /// </summary>
        /// <param name="numberItemsInArrays">Количетво элементов в массивах для тестирования</param>
        /// <param name="sortMethods">Список методов, которые нужно протестировать</param>
        public MySorts(int[] numberItemsInArrays, SortDelegate[] sortMethods)
        {
            this.sortMethods = sortMethods;
            MaxNumberInArray = 0;
            arraysForTest = new List<int[]>();
            int[] arr;
            foreach (var item in numberItemsInArrays)
            {
                if (MaxNumberInArray < item) { MaxNumberInArray = item; }
                arr = CreateNewArray(item);
                arraysForTest.Add(arr);
                arr = arr.Clone() as int[];
                Array.Sort(arr, ReversComparer);
                arraysForTest.Add(arr);
                arr = arr.Clone() as int[];
                Array.Sort(arr);
                arraysForTest.Add(arr);
            }
        }

        /// <summary>
        /// Реализует интерфейс Comparison<T> для обратной сортировки массива
        /// </summary>
        /// <param name="a">Первое число</param>
        /// <param name="b">Второе число</param>
        /// <returns>Результат сравнения: 1, -1, или 0</returns>
        int ReversComparer(int a, int b) => (a < b) ? 1 : ((a > b) ? -1 : 0);

        /// <summary>
        /// Создаёт новый массив и заполняет его случайными числами
        /// </summary>
        /// <param name="numberOfItems">Количество элементов в массиве</param>
        /// <returns>Возвращает массив, заполненный случайными числами</returns>
        int[] CreateNewArray(int numberOfItems)
        {
            int[] arr = new int[numberOfItems];
            Random rand = new Random(numberOfItems);
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rand.Next(numberOfItems + 1);
            }
            return arr;
        }

        /// <summary>
        /// Запускает процесс тестирования сортировок, выводит таблицу по работе каждого метода с каждым массивом.
        /// Также заполняет данные в таблицы для проведения сравнительного анализа работы методов между собой.
        /// </summary>
        public void TestSortes()
        {
            time = new int[3 * sortMethods.Length];
            comparer = new int[3 * sortMethods.Length];
            swap = new int[3 * sortMethods.Length];
            int index = 0;
            Console.WriteLine($"{"Метод",10}| {"Кол-во эл.",10}| {"Вид массива",14}| {"Сравнения",10}| " +
                $"{"Перестановки",12}| {"Время",10}| {"Сравнения/N",11}| {"Отсортирован?",13}|");
            for (int i = 0; i < arraysForTest.Count; i += 3)
            {
                foreach (var sortMethod in sortMethods)
                {
                    for (int j = i; j < i + 3; j++)
                    {
                        CountOp = 0;
                        CountSwap = 0;
                        Console.Write($"{sortMethod.Method.ToString().Substring(5, sortMethod.Method.ToString().IndexOf('(') - 5),10}| ");
                        Console.Write($"{arraysForTest[j].Length,10}| ");
                        switch (j % 3)
                        {
                            case 0:
                                Console.Write("      Обычный | ");
                                break;
                            case 1:
                                Console.Write("     Обратный | ");
                                break;
                            case 2:
                                Console.Write("Сортированный | ");
                                break;
                            default:
                                break;
                        }
                        int[] arr = arraysForTest[j].Clone() as int[];
                        StartTime = DateTime.Now;
                        sortMethod(ref arr);
                        FinishTime = DateTime.Now;
                        //Print(arr);
                        if (MaxNumberInArray == arraysForTest[j].Length)
                        {
                            time[index] = (int)(FinishTime - StartTime).TotalMilliseconds;
                            comparer[index] = CountOp;
                            swap[index] = CountSwap;
                            index++;
                        }
                        Console.WriteLine
                            ($"{(CountOp == -1 ? "Прерывание" : CountOp.ToString()),10}| " +
                            $"{CountSwap,12}| " +
                            $"{(int)(FinishTime - StartTime).TotalMilliseconds,10}| {CountOp / arr.Length,11}| " +
                            $"{(CheckSortResult(ref arr) ? "Да" : "Нет"),13}|");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Выводит таблицы сравнительного анализа работы методов между собой по времени выполнения, кол-ву сравнений и свопов 
        /// </summary>
        public void TestSortesСompareTable()
        {
            Console.WriteLine($"\n\nТаблица сравнения методов сортировки на массивах из {MaxNumberInArray} элементов:");
            Console.WriteLine("По времени выполнения (вычитаем время по столбцу из времени по строке)." +
                "\nОтрицательное значение означает, что метод в строке работает быстрее, чем по столбцу:");
            Console.WriteLine();
            OutPutTable(ref time, sortMethods);
            Console.WriteLine("\n\nПо количеству сравнений (вычитаем время по столбцу из времени по строке)." +
                "\nОтрицательное значение означает, что метод в строке делает меньше сравнений, чем по столбцу:");
            Console.WriteLine();
            OutPutTable(ref comparer, sortMethods);
            Console.WriteLine("\n\nПо количеству свопов (вычитаем время по столбцу из времени по строке)." +
                "\nОтрицательное значение означает, что метод в строке делает меньше свопов, чем по столбцу:");
            Console.WriteLine();
            OutPutTable(ref swap, sortMethods);
        }

        /// <summary>
        /// Рассчитывает значения отклонений при сравнении каждого элемента массива с каждым элементом и выводит их в таблицу 
        /// </summary>
        /// <param name="arr">Массив для данными для рассчётов</param>
        /// <param name="sortMethods">Список методов, для вывода их наименования в таблицу</param>
        private void OutPutTable(ref int[] arr, SortDelegate[] sortMethods)
        {
            Console.Write($"{"Методы",10}| {"Тип",9}| ");
            foreach (var sortMethod in sortMethods)
            {
                Console.Write($"{sortMethod.Method.ToString().Substring(5, sortMethod.Method.ToString().IndexOf('(') - 5),10}| ");
            }
            Console.WriteLine();
            int index = 0;
            foreach (var sortMethod in sortMethods)
            {
                for (int i = index; i < index + 3; i++)
                {
                    Console.Write($"{sortMethod.Method.ToString().Substring(5, sortMethod.Method.ToString().IndexOf('(') - 5),10}| ");
                    switch (i % 3)
                    {
                        case 0:
                            Console.Write(" Обычный | ");
                            break;
                        case 1:
                            Console.Write("Обратный | ");
                            break;
                        case 2:
                            Console.Write("Отсортир | ");
                            break;
                        default:
                            break;
                    }
                    for (int j = i % 3; j < arr.Length; j += 3)
                    {
                        Console.Write($"{(arr[index + (i % 3)] - arr[j]),10}| ");
                    }
                    Console.WriteLine();
                }
                index += 3;
            }

        }

        /// <summary>
        /// Проверяет отсортирован ли массив и возвращает true, если он отсортирован, иначе false
        /// </summary>
        /// <param name="arr">Массив для проверки его отсортированности</param>
        /// <returns>Возвращает true, если массив отсортирован, иначе false</returns>
        public bool CheckSortResult(ref int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i - 1] > arr[i]) { return false; }
            }
            return true;
        }

        /// <summary>
        /// Двоичный поиск в отсортированном бинарном дереве, который передаётся ввиде массива, 
        /// где сыновья располагаются по индексам: левый i*2, правый i*2+1, относительно индекса родителя. 
        /// </summary>
        /// <param name="item">Элемент, который нужно найти в бинарном дереве</param>
        /// <param name="arr">отсортированное бинарное дерево в виде массива</param>
        /// <returns>Возвращает индекс элемента или -1, если не нашёл данный элемент</returns>
        public static int BinarySearch(int item, ref int[] arr)
        {
            CountOp = 0;
            int L = 0;
            int R = arr.Length - 1;
            int index;
            while (L != R)
            {
                CountOp++;
                index = L + (R - L) / 2;
                if (arr[index] == item) { return index; }
                if (arr[index] > item) { R = index - 1; }
                else { L = index + 1; }
            }
            return -1;
        }

        /// <summary>
        /// Шейкерная сортировка, или двунаправленная (англ. Cocktail sort) — разновидность пузырьковой сортировки
        /// </summary>
        /// <param name="arr">Массив для сортировки</param>
        /// <param name="noUsed1">Не используется, нужен для приведения метода к общей сигнатуре</param>
        /// <param name="noUsed2">Не используется, нужен для приведения метода к общей сигнатуре</param>
        /// <param name="noUsed3">Не используется, нужен для приведения метода к общей сигнатуре</param>
        public static void Shaker(ref int[] arr, int noUsed1 = 0, int noUsed2 = 0, int noUsed3 = 0)
        {
            int flagSwap = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                CountOp++;
                for (int j = i + 1; j < arr.Length - i; j++)
                {
                    CountOp++;
                    if (arr[j - 1] > arr[j])
                    {
                        CountSwap++; arr[j - 1] ^= arr[j]; arr[j] ^= arr[j - 1]; arr[j - 1] ^= arr[j];
                        flagSwap = 1;
                    }
                }
                if (flagSwap == 0) { break; } else { flagSwap = 0; }
                for (int j = arr.Length - 2 - i; j > i; j--)
                {
                    CountOp++;
                    if (arr[j - 1] > arr[j])
                    {
                        CountSwap++; arr[j - 1] ^= arr[j]; arr[j] ^= arr[j - 1]; arr[j - 1] ^= arr[j];
                        flagSwap = 1;
                    }
                }
                if (flagSwap == 0) { break; } else { flagSwap = 0; }
            }
        }

        /// <summary>
        /// Сортировка простыми обменами, сортиро́вка пузырько́м (англ. bubble sort) — простой алгоритм сортировки. 
        /// Для понимания и реализации этот алгоритм — простейший, но эффективен он лишь для небольших массивов. 
        /// </summary>
        /// <param name="arr">Массив для сортировки</param>
        /// <param name="noUsed1">Не используется, нужен для приведения метода к общей сигнатуре</param>
        /// <param name="noUsed2">Не используется, нужен для приведения метода к общей сигнатуре</param>
        /// <param name="noUsed3">Не используется, нужен для приведения метода к общей сигнатуре</param>
        public static void Buble(ref int[] arr, int noUsed1 = 0, int noUsed2 = 0, int noUsed3 = 0)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                CountOp++;
                for (int j = 1; j < arr.Length; j++)
                {
                    CountOp++;
                    if (arr[j - 1] > arr[j]) { CountSwap++; arr[j - 1] ^= arr[j]; arr[j] ^= arr[j - 1]; arr[j - 1] ^= arr[j]; }
                }
            }
        }

        /// <summary>
        /// Оптимизированный алгоритм пузырьковой сортировки - убраны лишние проходы по массиву
        /// </summary>
        /// <param name="arr">Массив для сортировки</param>
        /// <param name="noUsed1">Не используется, нужен для приведения метода к общей сигнатуре</param>
        /// <param name="noUsed2">Не используется, нужен для приведения метода к общей сигнатуре</param>
        /// <param name="noUsed3">Не используется, нужен для приведения метода к общей сигнатуре</param>
        public static void BublePlus(ref int[] arr, int noUsed1 = 0, int noUsed2 = 0, int noUsed3 = 0)
        {
            int flagSwap = 0;
            int indexJamp = 0;
            int count = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                CountOp++;
                for (int j = 1; j < arr.Length - count; j++)
                {
                    CountOp++;
                    if (arr[j - 1] > arr[j])
                    {
                        CountSwap++;
                        arr[j - 1] ^= arr[j]; arr[j] ^= arr[j - 1]; arr[j - 1] ^= arr[j];
                        if (flagSwap == 0) { indexJamp = j + 1; }
                        flagSwap = 1;
                    }

                }
                count++;
                i = indexJamp - 1;
                if (flagSwap == 0) { break; }
                else flagSwap = 0;
            }
        }

        /// <summary>
        /// Выводит значения в консоль по всем эталонным массивам, которые используются для тестирования сортировок
        /// </summary>
        public void Print()
        {
            foreach (var arr in arraysForTest)
            {
                foreach (var item in arr)
                {
                    Console.Write($"{item} ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Выводит в консоль значения указанного массива
        /// </summary>
        /// <param name="arr">Массив для вывода его значений в консоль</param>
        public void Print(int[] arr)
        {
            foreach (var item in arr)
            {
                Console.Write($"{item} ");
            }
        }

        /// <summary>
        /// Быстрая сортировка, сортировка Хоара (англ. quicksort), часто называемая qsort.
        /// Один из самых быстрых известных универсальных алгоритмов сортировки массивов: в среднем O(n*log n) обменов. 
        /// Из-за наличия ряда недостатков на практике обычно используется с некоторыми доработками.
        /// </summary>
        /// <param name="arr">Массив для сортировки</param>
        /// <param name="startIndex">Начальный индекс для сортировки</param>
        /// <param name="endIndex">Конечный индекс для сортировки</param>
        /// <param name="noUsed">Не используется, нужен для приведения метода к общей сигнатуре</param>
        public static void Quick(ref int[] arr, int startIndex = 0, int endIndex = -10, int noUsed = 0)
        {
            if (endIndex == -10) { endIndex = arr.Length - 1; }
            if (startIndex >= endIndex) { return; }
            int wall = startIndex - 1;
            int pillar = arr[endIndex];
            int temp;
            for (int i = startIndex; i <= endIndex; i++)
            {
                CountOp++;
                if (CountOp >= 45000000) { CountOp = -1; return; } //Делаем прерывание вычислений, чтобы не получить ошибку "Out of Memory"
                if (arr[i] <= pillar)
                {
                    if (i == wall + 1)
                    {
                        wall++;
                    }
                    else
                    {
                        CountSwap++;
                        temp = arr[wall + 1];
                        arr[wall + 1] = arr[i];
                        arr[i] = temp;
                        wall++;
                    }
                }
            }
            if (wall - startIndex > 0) { Quick(ref arr, startIndex, wall - 1); }
            if (endIndex - wall > 0) { Quick(ref arr, wall + 1, endIndex); }
        }

        /// <summary>
        /// Доработанный алгоритм быстрой сортировки, где на каждом шаге считается среднее арифметическое значение по элементам
        /// и далее оно используется для определения опорного значения.
        /// </summary>
        /// <param name="arr">Массив для сортировки</param>
        /// <param name="startIndex">Начальный индекс для сортировки</param>
        /// <param name="endIndex">Конечный индекс для сортировки</param>
        /// <param name="avarage">Среднее значение элементов массива</param>
        public static void QuickAver(ref int[] arr, int startIndex = 0, int endIndex = -10, int avarage = 0)
        {
            if (endIndex == -10) { endIndex = arr.Length - 1; avarage = arr[endIndex]; }
            if (startIndex >= endIndex) { return; }
            int wall = startIndex - 1;
            int pillar = avarage;
            int leftSum = 0;
            int rightSum = 0;
            int temp;
            for (int i = startIndex; i <= endIndex; i++)
            {
                CountOp++;
                if (CountOp >= 50000000) { CountOp = -1; return; } //Делаем прерывание вычислений, чтобы не получить ошибку "Out of Memory"
                if (arr[i] <= pillar)
                {
                    if (i == wall + 1)
                    {
                        wall++;
                        leftSum += arr[i];
                    }
                    else
                    {
                        leftSum += arr[i];
                        CountSwap++;
                        temp = arr[wall + 1];
                        arr[wall + 1] = arr[i];
                        arr[i] = temp;
                        wall++;
                    }
                }
                else { rightSum += arr[i]; }
            }
            if (wall - startIndex > 0)
            {
                QuickAver(ref arr, startIndex, arr[wall] == pillar ? wall - 1 : wall, leftSum / (wall - startIndex + 1));
            }
            if (endIndex - wall > 1)
            {
                QuickAver(ref arr, wall + 1, endIndex, rightSum / (endIndex - wall));
            }
        }

        /// <summary>
        /// Пирамидальная сортировка (англ. Heapsort, «Сортировка кучей») — алгоритм сортировки, работающий в худшем, 
        /// в среднем и в лучшем случае (то есть гарантированно) за Θ(n log n) операций при сортировке n элементов. 
        /// Количество применяемой служебной памяти не зависит от размера массива (то есть, O(1)).
        /// </summary>
        /// <param name="arr">Массив для сортировки</param>
        /// <param name="noUsed1">Не используется, нужен для приведения метода к общей сигнатуре</param>
        /// <param name="noUsed2">Не используется, нужен для приведения метода к общей сигнатуре</param>
        /// <param name="noUsed3">Не используется, нужен для приведения метода к общей сигнатуре</param>
        public static void Heap(ref int[] arr, int noUsed1 = 0, int noUsed2 = 0, int noUsed3 = 0)
        {
            int endHeap = arr.Length - 1;
            int temp;

            while (endHeap > 0)
            {
                for (int i = (endHeap - (2 - (endHeap % 2))) / 2; i >= 0; i--)
                {
                    CountOp++;

                    if (i * 2 + 1 > endHeap) { continue; }
                    if (arr[i] < arr[i * 2 + 1]) { CountSwap++; temp = arr[i]; arr[i] = arr[i * 2 + 1]; arr[i * 2 + 1] = temp; }
                    if (i * 2 + 2 > endHeap) { continue; }
                    if (arr[i] < arr[i * 2 + 2]) { CountSwap++; temp = arr[i]; arr[i] = arr[i * 2 + 2]; arr[i * 2 + 2] = temp; }

                }
                CountSwap++; temp = arr[endHeap]; arr[endHeap] = arr[0]; arr[0] = temp; endHeap--;
            }
        }

        /// <summary>
        /// Доработанная быстрая сортировка с использованием 2-х указателей, которые идут на встречу друг другу с концов массива
        /// и делают свопы, если элемент у левого указателя больше опорного значения, а элемент у правого указателя меньше опорного значения.
        /// </summary>
        /// <param name="arr">Массив для сортировки</param>
        /// <param name="startIndex">Начальный индекс для сортировки</param>
        /// <param name="endIndex">Конечный индекс для сортировки</param>
        /// <param name="noUsed">Не используется, нужен для приведения метода к общей сигнатуре</param>
        public static void Quick2P(ref int[] arr, int startIndex = 0, int endIndex = -10, int noUsed = 0)
        {
            if (endIndex == -10) { endIndex = arr.Length - 1; }
            if (endIndex <= startIndex) { return; }
            int pillar = arr[endIndex];
            int lPointer = startIndex;
            int rPointer = endIndex;
            int direction = 1;
            int temp;
            while (lPointer < rPointer)
            {
                CountOp++;
                if (CountOp == 35000000) { CountOp = -1; return; }  //Делаем прерывание вычислений, чтобы не получить ошибку "Out of Memory"
                if (direction == 1)
                {
                    if (arr[lPointer] > pillar)
                    {
                        direction = -1;
                        continue;
                    }
                    lPointer++;
                }
                else
                {
                    if (arr[rPointer] <= pillar)
                    {
                        CountSwap++;
                        temp = arr[lPointer];
                        arr[lPointer] = arr[rPointer];
                        arr[rPointer] = temp;
                        direction = 1;
                        lPointer++;
                        if (lPointer >= rPointer - 1) { break; }
                        if (arr[rPointer - 1] > pillar) { rPointer--; }
                        continue;
                    }
                    rPointer--;
                }
            }
            Quick2P(ref arr, startIndex, lPointer - 1);
            Quick2P(ref arr, rPointer, endIndex);

        }
    }
}
