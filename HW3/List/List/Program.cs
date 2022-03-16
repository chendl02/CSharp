using System;
namespace List
{
    class program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            Random rd = new Random();
            Glist<int> list = new Glist<int>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(rd.Next() % 100);
            }
            int max = list.Head.data;
            int min = list.Head.data;

            list.ForEach(n => Console.WriteLine(n));
            list.ForEach(n => sum += n);
            list.ForEach(n => { max = max > n ? max : n; });
            list.ForEach(n => { min = min < n ? min : n; });

            Console.WriteLine("最大值是：" + max);
            Console.WriteLine("最小值是：" + min);
            Console.WriteLine("总和是：" + sum);


        }
    } 
}