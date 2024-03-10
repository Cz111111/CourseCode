using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ruangou_2_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(isZhiShu(11));
            int num = Int32.Parse(Console.ReadLine());
            for (int i = 2; i <= num; i++)
            {
                if (num%i==0 && isZhiShu(i))
                {
                    Console.Write(i+" ");
                    num = num / i;
                    i--;
                }
            }
        }
        static bool isZhiShu(int num) {
            if (num==1)
            {
                return true;
            }
            for (int i = num - 1; i > 1; i--) {
                if (num % i == 0) {
                    return false;
                }
            }
            return true;
        }
    }
}
