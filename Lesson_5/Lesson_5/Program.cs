using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_5
{
    class Program
    {
        /// <summary>
        /// Метод переводит запись арифметического выражения из инфиксной в постфиксную
        /// </summary>
        /// <param name="infix"></param>
        /// <returns></returns>
        static char[] ConvertInfixToPostfix(char[] infix)
        {
            Array.Resize(ref infix, infix.Length + 1);
            infix[infix.Length - 1] = '▉';
            List<char> postfix = new List<char>();
            Stack<char> stack = new Stack<char>(infix.Length / 2);
            stack.Push('▉');
            foreach (char item in infix)
            {
                if (Char.IsLetterOrDigit(item)) postfix.Add(item);
                else
                {
                S: switch (CheckStack(item, stack.Peek()))
                    {
                        case 0:
                            Console.WriteLine("Недопустимый символ в последовательности");
                            return null;
                        case 1:
                            stack.Push(item);
                            break;
                        case 2:
                            postfix.Add(stack.Pop());
                            goto S;
                        case 3:
                            stack.Pop();
                            break;
                        case 4:
                            break;
                        case 5:
                            Console.WriteLine("Недопустимая последовательность");
                            return null;
                        default:
                            break;
                    }
                }
            }
            return postfix.ToArray();
        }

        /// <summary>
        /// Метод проверяет комбинацию обрабатываемого символа и верхнего в стэке
        /// </summary>
        /// <param name="item"></param>
        /// <param name="stack"></param>
        /// <returns></returns>
        private static byte CheckStack(char item, char stack)
        {
            switch (item)
            {
                case '(':
                    return 1;
                case ')':
                    return SwitchForRBracket(stack);
                case '+':
                case '-':
                    return SwitchForAddSub(stack);
                case '*':
                case '/':
                    return SwitchForDivMul(stack);
                case '▉':
                    return SwitchForAnchor(stack);
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Метод выбирает операцию для указателя начала и конца последовательности
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        private static byte SwitchForAnchor(char stack)
        {
            switch (stack)
            {
                case '▉':
                    return 4;
                case '+':
                case '-':
                case '*':
                case '/':
                    return 2;
                case '(':
                    return 5;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Метод выбирает операцию для знаков * и /
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        private static byte SwitchForDivMul(char stack)
        {
            switch (stack)
            {
                case '▉':
                case '(':
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Метод выбирает операцию для знаков + и -
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        private static byte SwitchForAddSub(char stack)
        {
            switch (stack)
            {
                case '▉':
                case '(':
                    return 1;
                case '+':
                case '-':
                case '*':
                case '/':
                    return 2;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Метод выбирает операцию для закрывающей скобки
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        static byte SwitchForRBracket(char stack)
        {
            switch (stack)
            {
                case '▉':
                    return 5;
                case '+':
                case '-':
                case '*':
                case '/':
                    return 2;
                case '(':
                    return 3;
                default:
                    return 0;
            }
        }

        static void Main(string[] args)
        {
            char[] inf = { '(', 'A', '*', '(', 'B', '+', 'C', ')', '/', '8', ')' };
            Console.WriteLine(inf);
            Console.WriteLine(ConvertInfixToPostfix(inf));
            Console.ReadKey();
        }
    }
}
