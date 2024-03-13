using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ruangou_3_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IShape rectangle = new Rectangle(5.0, 3.0);
            IShape square = new Square(4.0);
            IShape triangle = new Triangle(6.0, 8.0);

            Console.WriteLine($"Rectangle: Area = {rectangle.CalculateArea()}, IsValid = {rectangle.IsValidShape()}");
            Console.WriteLine($"Square: Area = {square.CalculateArea()}, IsValid = {square.IsValidShape()}");
            Console.WriteLine($"Triangle: Area = {triangle.CalculateArea()}, IsValid = {triangle.IsValidShape()}");
        }
    }
    interface IShape
    {
        double CalculateArea();
        bool IsValidShape();
    }
    abstract class Shape : IShape
    {
        public string Name { get; set; }

        public abstract double CalculateArea();

        public abstract bool IsValidShape();
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

        public override bool IsValidShape()
        {
            return length > 0 && width > 0;
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

        public override bool IsValidShape()
        {
            return side > 0;
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

        public override bool IsValidShape()
        {
            return length > 0 && height > 0;
        }
    }
}
