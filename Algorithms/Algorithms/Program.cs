using System;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            int task;

            do
            {
                task = GetTask();

                switch (task)
                {
                    case 0:
                        Console.WriteLine("\n\nExit");
                        break;
                    case 1:
                        Task1();
                        break;
                    case 2:
                        Task2();
                        break;
                    case 3:
                        Task3();
                        break;
                    case 4:
                        Task4();
                        break;
                    default:
                        break;
                }
            }
            while (task != 0);

            Console.ReadKey();
        }

        /// <summary>
        /// Выбор задачи
        /// </summary>
        /// <returns></returns>
        static int GetTask()
        {
            Console.WriteLine();
            Console.WriteLine("Select an action:");
            Console.WriteLine("0: Exit");
            Console.WriteLine("1: Mass index");
            Console.WriteLine("2: Max of 4 ints");
            Console.WriteLine("3: Swap 2 ints");
            Console.WriteLine("4: Age");

            string selection = Console.ReadKey().KeyChar.ToString();
            int res;

            try
            {
                res = int.Parse(selection);
            }
            catch (Exception)
            {
                // Неправильный ввод
                return GetTask();
            }

            return res;
        }

        /// <summary>
        /// 1. Ввести вес и рост человека. Рассчитать и вывести индекс массы тела по формуле I=m/(h*h); где m-масса тела в килограммах, h - рост в метрах.
        /// </summary>
        static void Task1()
        {
            float m, h;

            Console.WriteLine("\n");
            Console.WriteLine("Enter body mass (kg): ");

            if (!float.TryParse(Console.ReadLine(), out m))
            {
                Console.WriteLine("Incorrect input!");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Enter body height (m): ");
            do
            {
                if (!float.TryParse(Console.ReadLine(), out h))
                {
                    Console.WriteLine("Incorrect input!");
                    return;
                }

                if (h == 0)
                    Console.WriteLine("Height must be non equal to 0.");
            }
            while (h == 0);

            Console.WriteLine("Mass index = {0}", MassIndex(m, h));
        }

        /// <summary>
        /// 2. Найти максимальное из четырех чисел. Массивы не использовать.
        /// </summary>
        static void Task2()
        {
            int i1, i2, i3, i4;

            Console.WriteLine("\n");
            Console.WriteLine("Enter four inegers.");
            if (!int.TryParse(Console.ReadLine(), out i1))
                return;
            if (!int.TryParse(Console.ReadLine(), out i2))
                return;
            if (!int.TryParse(Console.ReadLine(), out i3))
                return;
            if (!int.TryParse(Console.ReadLine(), out i4))
                return;

            Console.WriteLine("Max integer is {0}", MaxInt(i1, i2, i3, i4));
        }

        /// <summary>
        /// 3. Написать программу обмена значениями двух целочисленных переменных
        /// </summary>
        static void Task3()
        {
            int i1, i2;

            Console.WriteLine("\n");
            Console.WriteLine("Enter two integers.");
            if (!int.TryParse(Console.ReadLine(), out i1))
            {
                Console.WriteLine("Incorrect input!");
                return;
            }
            if (!int.TryParse(Console.ReadLine(), out i2))
            {
                Console.WriteLine("Incorrect input!");
                return;
            }

            Swap(ref i1, ref i2);
            Console.WriteLine("i1 = {0}; i2 = {1}", i1, i2);
        }

        /// <summary>
        /// 6. Ввести возраст человека (от 1 до 150 лет) и вывести его вместе с последующим словом «год», «года» или «лет».
        /// </summary>
        static void Task4()
        {
            int age;

            Console.WriteLine("\n");
            Console.WriteLine("Введите возраст от 1 до 150 лет.");

            do
            {

            } while (!int.TryParse(Console.ReadLine(), out age) || age < 1 || age > 150);

            Console.WriteLine("{0} {1}", age, StringAge(age));
        }

        /// <summary>
        /// Индекс массы тела.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        static float MassIndex(float m, float h)
        {
            return m / (h * h);
        }

        /// <summary>
        /// Максимальное число из 4
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="i2"></param>
        /// <param name="i3"></param>
        /// <param name="i4"></param>
        /// <returns></returns>
        static int MaxInt(int i1, int i2, int i3, int i4)
        {
            int max;

            if (i1 > i2)
                max = i1;
            else
                max = i2;

            if (i3 > max)
                max = i3;

            if (i4 > max)
                max = i4;

            return max;
        }

        static void Swap(ref int i1, ref int i2)
        {
            //// с использованием третьей переменной
            //int i0 = i1;
            //i2 = i1;
            //i1 = i0;

            // без использования третьей переменной
            i1 ^= i2;
            i2 = i1 ^ i2;
            i1 = i1 ^ i2;

        }

        /// <summary>
        /// Возвращает "год", "года" или "лет" в зависимости от переданного значения
        /// </summary>
        /// <returns></returns>
        static string StringAge(int age)
        {
            string res = "";
            int ost = age % 10;

            if (ost == 1 && age % 100 != 11)
                res = "год";
            else if (ost == 0 || (age % 100 > 10 && age % 100 < 20))
                res = "лет";
            else
                res = "года";

            return res;
        }
    }
}
