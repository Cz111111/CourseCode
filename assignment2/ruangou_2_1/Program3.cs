using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ruangou_2_1
{
    internal class Program3
    {
        static void Main(string[] args)
        {
            int[] arr = new int[99];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i + 2;
            }
        
        
            for (int i = 2; i <= 100; i++)
            {
                for (int j = 2 * i; j <= 100; j += i)
                {
                    arr[j - 2] = 0;
                }

            }
        
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != 0)
                {
                    Console.Write(arr[i] + " ");
                }
        
            }
        }
    }
}
