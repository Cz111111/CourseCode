using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ruangou_2_1
{
    internal class Program2
    {
        static void Main(string[] args)
        {
            int[] arr = { 1, 2, 3, 4, 5 };
            int max = arr[0];
            int min = arr[0];
            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (max < arr[i])
                {
                    max = arr[i];
                }
                if (min > arr[i])
                {
                    min = arr[i];
                }
                sum += arr[i];
            }
            Console.WriteLine("max:" + max);
            Console.WriteLine("min:" + min);
            Console.WriteLine("sum:" + sum);
            Console.WriteLine("average:" + (sum / arr.Length));
        }
    }
}
