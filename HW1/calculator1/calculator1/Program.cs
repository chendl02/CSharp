using System;
namespace calculator1
{
    public class Calculator
    {
        static int Main()
        {
            bool loop=true;
            while (loop)
            {
                Console.WriteLine("请输入两个数字和一个运算符");
                Console.WriteLine("请输入第一个数字");
                string num1 = Console.ReadLine();
                Console.WriteLine("请输入运算符");
                string op = Console.ReadLine();
                Console.WriteLine("请输入第二个数字");
                string num2 = Console.ReadLine();
                int n;
                int m;
                bool judge1 = Int32.TryParse(num1, out n);
                bool judge2 = Int32.TryParse(num2, out m);
                if (judge1 && judge2)
                {
                    loop = false;
                    if (op == "+") Console.WriteLine("结果为：{0:D}", m+n);
                    else if (op == "-") Console.WriteLine("结果为：{0:D}", m - n);
                    else if (op == "*") Console.WriteLine("结果为：{0:D}", m * n);
                    else if (op == "/") Console.WriteLine("结果为：{0:D}", m / n);
                    else
                    {
                        Console.WriteLine("请输入'+''-''*''/'中的运算符");
                        loop=true;
                    }
                }
                else
                {
                    Console.WriteLine("请正确输入数字");
                    loop=true;
                }

            }
            return 0;
        }

    }
}
