using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ruangou_2_1
{
    internal class Program4
    {
        static void Main(string[] args)
        {
            int[,] arr = {
                {1,2,3,4 },
                {5,1,2,3 },
                {9,5,1,2 }
            };
            bool flag=true;
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);
            for (int i = 1; i < rows; i++)
            {
                for (int j = 1; j < cols; j++)
                {
                    if (arr[i, j] != arr[i-1,j-1])
                    {
                        flag = false;
                        break;
                    }
                }
            }
            
            Console.WriteLine(flag);
        
        }
    }
}
