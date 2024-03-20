using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace ruangou_4_1
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            GenericList<int> list = new GenericList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            list.ForEach(
                x => Console.Write(x+" "),
                (a, b) => a > b ? a : b,
                (a, b) => a < b ? a : b,
                (a, b) => a+b
                );
        }
    }
    class Node<T> { 
        public Node<T> Next { get; set; }
        public T Data { get; set; }
        public Node(T t) {
            Next = null;
            Data = t;
        }
    }
    class GenericList<T> {
        private Node<T> head;
        private Node<T> tail;
        public GenericList()
        {
            tail = head = null;
        }
        public Node<T> Head { 
            get=> head;
        }
        public void Add(T t)
        {
            Node<T> n = new Node<T>(t);
            if (tail==null)
            {
                tail = head = n;
            }
            else
            {
                tail.Next = n;
                tail = n;
            }
        }
        public void ForEach(
            Action<T> action, 
            Func<T, T, T> f_max, 
            Func<T, T, T> f_min, 
            Func<T, T, T> f_sum
            )
        {
            Node<T> current = head;
            T max = head.Data;
            T min = head.Data;
            T sum = head.Data;
            while (current != null)
            {
                action(current.Data);
                max = f_max(max, current.Data);
                min = f_min(min, current.Data);
                sum = f_sum(sum, current.Data);
                current = current.Next;
            }
            Console.WriteLine();
            Console.WriteLine($"最大值: {max}");
            Console.WriteLine($"最小值: {min}");
            Console.WriteLine($"求和: {sum}");
            
        }
    }
    
}
