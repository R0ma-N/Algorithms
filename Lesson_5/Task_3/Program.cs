using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
    class Program
    {
        /// <summary>
        /// Метод удаляет в последовательности все символы кроме скобок
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        static char[] ConvertToSeqOfBrackets(string inputData)
        {
            List<char> res = inputData.ToList();
            string brackets = "[](){}";
            for (int i = 0; i < res.Count; i++)
            {
                if (brackets.IndexOf(res[i]) == -1)
                {
                    res.Remove(res[i]);
                    i--;
                }
            }
            char[] arrRes = res.ToArray();
            return arrRes;
        }

        /// <summary>
        /// Метод проверяет является ли последовательность скобок правильной
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        static bool CheckSequenceOfBrackets(char[] arr)
        {
            Stack<char> stack = new Stack<char>(arr.Length / 2);
            foreach (char item in arr)
            {
                switch (item)
                {
                    case '(':
                    case '[':
                    case '{':
                        stack.Push(item);
                        continue;
                    case ')':
                    case ']':
                    case '}':
                        if (CheckPairOfBrackets(stack, item)) continue;
                        else return false;
                    default:
                        return false;
                }
            }
            if (stack.Count == 0) return true;
            else return false;
        }

        /// <summary>
        /// Метод проверяет является ли скобка парной
        /// </summary>
        /// <param name="stack">Стэк с последовательностью левых скобок</param>
        /// <param name="brR">Правая скобка</param>
        /// <returns></returns>
        static bool CheckPairOfBrackets(Stack<char> stack, char brR)
        {
            if (stack.Count == 0) return false;
            char brL = stack.Pop();
            switch ((int)brR)
            {
                case 41:
                    return (brR - 1 == brL) ? true : false;
                case 93:
                case 125:
                    return (brR - 2 == brL) ? true : false;
                default:
                    return false;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine(ConvertToSeqOfBrackets(@"(fk k =[ & } 54 { / ] \_)"));
            char[] v = { '(', '(', '[', '{', '}', ']', '{', '}', ')', '[', '[', ']', '(', ')', ']', ')' };
            Console.WriteLine(CheckSequenceOfBrackets(v));
            Console.ReadKey();
        }

    }
}
