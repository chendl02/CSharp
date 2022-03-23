// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
class Program
{
    static void Main(string[] args)
    {
        int[] arr = new int[100];
        Random random = new Random();

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = random.Next(1000) + 1;
        }
        int[] query = arr.Select(x => x).OrderByDescending(x => x).ToArray();//用LINQ对随机数从大到小排序
        int sum = query.Sum();                                               //用LINQ对随机数求和
        double average = query.Average();                                    //用LINQ对随机数求平均值
        Console.WriteLine("产生的100个随机数从大到小为：");
        for (int i = 0; i < 100; i++)
        {
            Console.WriteLine(query[i]);
        }

        Console.WriteLine("100个随机数的和为：{0}", sum);
        Console.WriteLine("100个随机数的平均值为：{0}", average);
    }
}