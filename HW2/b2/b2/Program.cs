using System;
namespace b2
{
    class b2
    {
        static void Main()
        {
            int[] arr = { 1, 2, 3, 4, 5,6 };
            int Max(int[] arr)
            {
                int temp=arr[0];
                for(int i=1; i<arr.Length; i++)
                {
                    if(arr[i] > temp)temp = arr[i];
                }
                
                return temp;
            }
            int Min(int[] arr)
            {
                int temp = arr[0];
                for (int i = 1; i < arr.Length; i++)
                {
                    if (arr[i] < temp) temp = arr[i];
                }

                return temp;
            }
            double Avg(int[] arr)
            {
                double temp = arr[0];
                for (int i = 1; i < arr.Length; i++)
                {
                    temp += arr[i];
                }

                return temp/arr.Length;
            }

            int Sum(int[] arr)
            {
                int temp = arr[0];
                for (int i = 1; i < arr.Length; i++)
                {
                    temp += arr[i];
                }

                return temp ;
            }
            int max=Max(arr);
            Console.WriteLine("数组中最大的数为:{0}",max);
            int min=Min(arr);
            Console.WriteLine("数组中最小的数为:{0}",min);
            double avg=Avg(arr);
            Console.WriteLine("数组中平均数为:{0}", avg);
            int sum=Sum(arr);
            Console.WriteLine("数组元素的和：{0}", sum);
        }
    }
}