using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ruangou_3_2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Random rand=new Random();
            IShape[] shapes = new IShape[10];
            for (int i = 0; i < 10; i++)
            {
                shapes[i] = ShapeFactory.CreateShape(rand);
            }

            double totalArea = 0.0;
            for (int i = 0; i < 10; i++)
            {
                totalArea += shapes[i].CalculateArea();
                Console.WriteLine($"第{i+1}形状为：{shapes[i].JudgeShape(shapes[i])}");
            }

            Console.WriteLine($"Total area: {totalArea:0.00}(结果保留两位小数)");
        }
    }
    interface IShape
    {
        double CalculateArea();
        string JudgeShape(IShape shape);
    }

    abstract class Shape : IShape
    {
        public abstract double CalculateArea();
        public string JudgeShape(IShape shape) {
            if (shape is Rectangle) { 
                return "长方形"; 
            }else if(shape is Square) {
                return "正方形"; 
            }else if (shape is Triangle) {
                return "三角形"; 
            }else {
                return null; 
            }

        }
    }
    class Rectangle : Shape
    {
        private double length;
        private double width;

        public Rectangle(double length, double width)
        {
            this.length = length;
            this.width = width;
        }

        public override double CalculateArea()
        {
            return length * width;
        }
    }

    class Square : Shape
    {
        private double side;

        public Square(double side)
        {
            this.side = side;
        }

        public override double CalculateArea()
        {
            return side * side;
        }
    }

    class Triangle : Shape
    {
        private double length;
        private double height;

        public Triangle(double length, double height)
        {
            this.length = length;
            this.height = height;
        }

        public override double CalculateArea()
        {
            return 0.5 * length * height;
        }
    }
    class ShapeFactory
    {
        public static IShape CreateShape(Random rand)
        {
            int shapeType = rand.Next(3);  // 生成 0~2 的随机数，表示形状类型

            switch (shapeType)
            {
                case 0:
                    return new Rectangle(rand.NextDouble(), rand.NextDouble());
                case 1:
                    return new Square(rand.NextDouble());
                case 2:
                    return new Triangle(rand.NextDouble(), rand.NextDouble());
                default:
                    throw new Exception("Invalid shape type");
            }
        }
    }
}
