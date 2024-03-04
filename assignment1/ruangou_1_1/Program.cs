using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ruangou_1_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //编写一个实现计算器的控制台程序，接受两个数字和一个运算符作为输入，并打印出计算结果。
            Console.WriteLine("接受两个数字和一个运算符作为输入，并打印出计算结果");
            Console.WriteLine("输入第一个数：");
            int num1 = Int32.Parse(Console.ReadLine());
            Console.WriteLine("输入第二个数：");
            int num2 = Int32.Parse(Console.ReadLine());
            Console.WriteLine("输入运算符");
            string op = Console.ReadLine();
            switch (op)
            {
                case "+":
                    Console.WriteLine("结果为：{0}+{1}={2}", num1, num2, num1 + num2);
                    break;
                case "-":
                    Console.WriteLine("结果为：{0}-{1}={2}", num1, num2, num1 - num2);
                    break;
                case "*":
                    Console.WriteLine("结果为：{0}*{1}={2}", num1, num2, num1 * num2);
                    break;
                case "/":
                    Console.WriteLine("结果为：{0}/{1}={2}", num1, num2, num1 / num2);
                    break;
                default:
                    Console.WriteLine("不支持该运算符");
                    break;
            }

        }
    }
}
