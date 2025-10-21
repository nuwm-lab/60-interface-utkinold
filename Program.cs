using System;

namespace AbstractClassTriangle
{
    public abstract class Triangle
    {
        protected string triangleType;
        protected string perimetr;
        protected string area;

        public abstract string FindTriangleType { get; set; }
        public abstract string FindPerimetr { get; set; }
        public abstract string FindArea { get; set; }

        public string GetTriangleType()
        {
            return triangleType;
        }

        public string GetPerimetr()
        {
            return perimetr;
        }

        public string GetArea()
        {
            return area;
        }
    }

    public class GenericTriangle : Triangle
    {
        private double sideA;
        private double sideB;
        private double sideC;

        public GenericTriangle(double a, double b, double c)
        {
            sideA = a;
            sideB = b;
            sideC = c;

            FindTriangleType = DetermineTriangleType();
            FindPerimetr = CalculatePerimeter().ToString("F2");
            FindArea = CalculateArea().ToString("F2");
        }

        public override string FindTriangleType
        {
            get => triangleType;
            set => triangleType = value;
        }

        public override string FindPerimetr
        {
            get => perimetr;
            set => perimetr = value;
        }

        public override string FindArea
        {
            get => area;
            set => area = value;
        }

        private string DetermineTriangleType()
        {
            if (sideA == sideB && sideB == sideC)
                return "Рівносторонній";
            else if (sideA == sideB || sideB == sideC || sideA == sideC)
                return "Рівнобедрений";
            else
                return "Різносторонній";
        }

        private double CalculatePerimeter()
        {
            return sideA + sideB + sideC;
        }

        private double CalculateArea()
        {
            double s = CalculatePerimeter() / 2;
            double area = Math.Sqrt(s * (s - sideA) * (s - sideB) * (s - sideC));
            return area;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть довжину сторони A:");
            bool isValidA = double.TryParse(Console.ReadLine(), out double a);

            Console.WriteLine("Введіть довжину сторони B:");
            bool isValidB = double.TryParse(Console.ReadLine(), out double b);

            Console.WriteLine("Введіть довжину сторони C:");
            bool isValidC = double.TryParse(Console.ReadLine(), out double c);

            if (!isValidA || !isValidB || !isValidC || a <= 0 || b <= 0 || c <= 0)
            {
                Console.WriteLine("Помилка: Введіть додатні числа для сторін.");
                return;
            }

            if (a + b <= c || b + c <= a || a + c <= b)
            {
                Console.WriteLine("Такого трикутника не існує.");
                return;
            }

            Triangle triangle = new GenericTriangle(a, b, c);

            Console.WriteLine($"Тип трикутника: {triangle.GetTriangleType()}");
            Console.WriteLine($"Периметр: {triangle.GetPerimetr()}");
            Console.WriteLine($"Площа: {triangle.GetArea()}");
        }
    }
}
