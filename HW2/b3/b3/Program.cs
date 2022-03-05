using System;
namespace b3
{
    class b3
    {
        static public void Main()
        {
            int[] arr = new int[101];
            //数组内容为1表示为素数，将2到100全部设置为素数
            for (int i = 2; i <= 100; i++)
            {
                arr[i] = 1;
            }
            //剔除非素数
            for (int i = 2; i <=100; i++)
            {
                if(arr[i] != 0)
                {
                    for(int q = 2; q * i <= 100; q++)
                    {
                        arr[q*i] = 0;
                    }
                }
            }
            Console.Write("2-100的所有素数为：");
            for(int i = 2; i <= 100; i++)
            {
                if(arr[(int)i] != 0) Console.Write(" {0}",i);
            }
        }
    }
}