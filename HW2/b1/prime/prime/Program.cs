using System;
namespace prime
{
    class prime
    {
        static void Main(string[] args)
        {

            bool IsPrime(int i)
            {
                bool b = true;
                if (i == 1 ) b = false;
                else if(i == 2 ) b = true;
                else
                {

                    for (int j = 3; j < i; j++)
                    {
                        if (i % j == 0)
                        {
                            b = false;
                        }
                    }
                }
                return b;

            }
            Console.WriteLine("请输入数字");
            string num_=Console.ReadLine();
            if (num_ == null) return;
            int num=int.Parse(num_);
            Console.WriteLine("{0}的素数因子为：/n",num);
            for(int i = 2; i<= num; i++)
            {
                while (num % i == 0)
                {
                    if (IsPrime(i))
                    {
                        Console.Write("{0} ,",i);
                    }
                    num = num / i;
                }
            }
            
            
        }
        
    }
}