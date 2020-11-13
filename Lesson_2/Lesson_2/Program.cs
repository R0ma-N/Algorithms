using System;
using System.Collections.Generic;

namespace Lesson_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<BaseMenuItem> Menu = new List<BaseMenuItem>();
            Menu.Add(new BaseMenuItem("Exit", () => { Console.WriteLine("\n\nBye!"); }));
            Menu.Add(new BaseMenuItem("Decimal to binary", new Action(Task1)));
            Menu.Add(new BaseMenuItem("Power", new Action(Task2)));
            Menu.Add(new BaseMenuItem("Calculator", new Action(Task3)));
            Menu.Add(new BaseMenuItem("Calculator with array", new Action(Task4)));

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
        /// 1. Реализовать функцию перевода из десятичной системы в двоичную, используя рекурсию.
        /// </summary>
        static void Task1()
        {
            int m;

            Console.WriteLine("\n");
            Console.Write("Enter integer: ");

            if (!int.TryParse(Console.ReadLine(), out m))
            {
                Console.WriteLine("Incorrect input!");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Binary representation: {0}", DecimalToBinary(m));
            Console.ReadKey();
        }

        /// <summary>
        /// 2. Реализовать функцию возведения числа a в степень b:
        /// </summary>
        static void Task2()
        {
            int a, b;

            Console.WriteLine("\n");
            Console.Write("Enter <a>: ");
            if (!int.TryParse(Console.ReadLine(), out a))
                return;

            Console.Write("Enter <b>: ");
            if (!int.TryParse(Console.ReadLine(), out b))
                return;

            Console.WriteLine("{0} powered {1} is {2}", a, b, Power(a, b));
            Console.ReadKey();
        }

        /// <summary>
        /// 3. Исполнитель Калькулятор преобразует целое число, записанное на экране. У исполнителя две команды, каждой команде присвоен номер:
        /// </summary>
        static void Task3()
        {
            Console.WriteLine("\n");
            Console.WriteLine("3 to 20 by {0} programs.", Calculator());
            Console.ReadKey();
        }

        /// <summary>
        /// Решение задачи 3 с использованием массива
        /// </summary>
        static void Task4()
        {
            Console.WriteLine("\n");
            Console.WriteLine("3 to 20 by {0} programs.", CalculatorArray());
            Console.ReadKey();
        }

        /// <summary>
        /// Перевод числа из десятичной системы в двоичную.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        static string DecimalToBinary(int m)
        {
            if (m == 0)
                return "";
            else
                return DecimalToBinary(m / 2) + (m % 2).ToString();
        }

        /// <summary>
        /// Возведение числа в степень.
        /// </summary>
        /// <returns></returns>
        static int Power(int a, int b)
        {
            ////Без рекурсии
            //int res = 1;
            //for (int i = 0; i < b; i++)
            //{
            //    res *= a;
            //}
            //return res;

            ////Просто с рекурсией
            //if (b == 0)
            //    return 1;
            //else
            //    return a * Power(a, b-1);

            //С использованием свойства чётности степени
            if (b == 0)
                return 1;
            else if (b == 2)
                return a * a;
            else
            {
                if (b % 2 == 0)
                    return Power(Power(a, b / 2), 2);
                else
                    return a * Power(a, b - 1);
            }

        }

        /// <summary>
        /// Возвращает число программ, которые преобразуют 3 в 20 с использованием двух команд: +1 и *2.
        /// </summary>
        static int Calculator()
        {
            return DoCalcStep(3, 20, 0) + DoCalcStep(3, 20, 1);
        }

        /// <summary>
        /// Операция калькулятора.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <param name="comand">0 - прибавление 1; 1 - умножение на 2</param>
        /// <returns></returns>
        static int DoCalcStep(int start, int finish, int comand)
        {
            switch (comand)
            {
                case 0:
                    //+1
                    start += 1;
                    break;
                case 1:
                    //*2
                    start *= 2;
                    break;
                default:
                    break;
            }

            if (start == finish)
                return 1;
            else if (start > finish)
                return 0;
            else
                return DoCalcStep(start, finish, 0) + DoCalcStep(start, finish, 1);
        }

        /// <summary>
        /// Задача про калькулятор, но с использованием массива.
        /// </summary>
        /// <returns></returns>
        static int CalculatorArray()
        {
            int start = 3, finish = 20;

            List<string> programs = new List<string>();

            string mask = "";
            for (int i = 0; i <= finish - start; i++)
            {
                mask += "0";
            }

            string checkP;
            for (int i = 0; i <= Power(2, finish - start); i++)
            {
                checkP = CheckProgram(i, start, finish, mask);
                if (checkP != "")
                {
                    if (!programs.Contains(checkP))
                        programs.Add(checkP);
                }
            }

            return programs.Count;
        }

        //Проверка программы. Программа представляется в виде двоичного числа размером не больше программы из одних только прибавлений единицы.
        static string CheckProgram(int p, int start, int finish, string mask)
        {
            string outStr = "";
            string strP = DecimalToBinary(p);
            if (strP.Length > finish - start)
                return outStr;


            strP = mask.Substring(0, (finish - start) - strP.Length) + strP;
            int res = start;
            for (int i = 0; i < strP.Length; i++)
            {
                char item = strP[i];
                switch (item)
                {
                    case '0':
                        res += 1;
                        break;
                    case '1':
                        res *= 2;
                        break;
                    default:
                        break;
                }

                if (res == finish)
                {
                    outStr = strP.Substring(0, i + 1);
                    break;
                }
            }

            return outStr;
        }
    }
}
