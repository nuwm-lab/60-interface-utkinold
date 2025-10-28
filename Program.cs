using System;

namespace AbstractClassTriangle
{
    public abstract class Triangle
    {
        protected string triangleType;
        protected string perimetr;
        protected string area;
        protected string angles;

        public abstract string FindTriangleType { get; set; }
        public abstract string FindPerimetr { get; set; }
        public abstract string FindArea { get; set; }
        public abstract string FindAngles { get; set; }

        public string GetTriangleType() => triangleType;
        public string GetPerimetr() => perimetr;
        public string GetArea() => area;
        public string GetAngles() => angles;
    }

    public class GenericTriangle : Triangle
    {
        private double sideA, sideB, sideC;
        private double angleA, angleB, angleC;

        public GenericTriangle(double a, double b, double c)
        {
            sideA = a;
            sideB = b;
            sideC = c;

            CalculateAngles();

            FindTriangleType = DetermineTriangleType();
            FindPerimetr = CalculatePerimeter().ToString("F2");
            FindArea = CalculateArea().ToString("F2");
            FindAngles = $"{angleA:F2}°, {angleB:F2}°, {angleC:F2}°";
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
        public override string FindAngles
        {
            get => angles;
            set => angles = value;
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

        private double CalculatePerimeter() => sideA + sideB + sideC;

        private double CalculateArea()
        {
            double s = CalculatePerimeter() / 2;
            return Math.Sqrt(s * (s - sideA) * (s - sideB) * (s - sideC));
        }

        private void CalculateAngles()
        {
            angleA = RadianToDegree(Math.Acos((sideB * sideB + sideC * sideC - sideA * sideA) / (2 * sideB * sideC)));
            angleB = RadianToDegree(Math.Acos((sideA * sideA + sideC * sideC - sideB * sideB) / (2 * sideA * sideC)));
            angleC = 180 - angleA - angleB;
        }

        private double RadianToDegree(double rad) => rad * 180.0 / Math.PI;
    }

    public class EquilateralTriangle : Triangle
    {
        private double side;

        public EquilateralTriangle(double side)
        {
            this.side = side;
            FindTriangleType = "Рівносторонній";
            FindPerimetr = (3 * side).ToString("F2");
            FindArea = ((Math.Sqrt(3) / 4) * side * side).ToString("F2");
            FindAngles = "60°, 60°, 60°";
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

        public override string FindAngles
        {
            get => angles;
            set => angles = value;
        }
    }

    public class AngledTriangle : Triangle
{
    private double sideA, sideB, sideC;
    private double angleA, angleB, angleC; 

    public AngledTriangle(double sideA, double angle1Deg, double angle2Deg)
    {
        if (angle1Deg <= 0 || angle2Deg <= 0 || angle1Deg + angle2Deg >= 180)
            throw new ArgumentException("Некоректні кути: сума двох кутів повинна бути менша за 180.");

        this.sideA = sideA;
        angleA = angle1Deg;
        angleB = angle2Deg;
        angleC = 180 - angleA - angleB;

        double radA = DegreeToRadian(angleA);
        double radB = DegreeToRadian(angleB);
        double radC = DegreeToRadian(angleC);

        double k = sideA / Math.Sin(radA);
        sideB = k * Math.Sin(radB);
        sideC = k * Math.Sin(radC);

        FindTriangleType = DetermineTriangleType();
        FindPerimetr = CalculatePerimeter().ToString("F2");
        FindArea = CalculateArea().ToString("F2");
        FindAngles = $"{angleA:F2}°, {angleB:F2}°, {angleC:F2}°";   
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

    public override string FindAngles
    {
        get => angles;
        set => angles = value;
    }

    private string DetermineTriangleType()
    {
        const double eps = 1e-6;
        bool abEqual = Math.Abs(sideA - sideB) < eps;
        bool bcEqual = Math.Abs(sideB - sideC) < eps;
        bool acEqual = Math.Abs(sideA - sideC) < eps;

        if (abEqual && bcEqual)
            return "Рівносторонній";
        else if (abEqual || bcEqual || acEqual)
            return "Рівнобедрений";
        else
            return "Різносторонній";
    }

    private double CalculatePerimeter() => sideA + sideB + sideC;

    private double CalculateArea()
    {
        double s = CalculatePerimeter() / 2;
        return Math.Sqrt(s * (s - sideA) * (s - sideB) * (s - sideC));
    }

    private double DegreeToRadian(double angle) => angle * Math.PI / 180.0;
}


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть сторону A трикутника:");
            bool validA = double.TryParse(Console.ReadLine(), out double sideA);

            Console.WriteLine("Введіть сторону B трикутника:");
            bool validB = double.TryParse(Console.ReadLine(), out double sideB);

            Console.WriteLine("Введіть сторону C трикутника:");
            bool validC = double.TryParse(Console.ReadLine(), out double sideC);

            if (validA && validB && validC && sideA > 0 && sideB > 0 && sideC > 0 &&
                sideA + sideB > sideC && sideB + sideC > sideA && sideA + sideC > sideB)
            {
                Triangle genericTriangle = new GenericTriangle(sideA, sideB, sideC);
                Console.WriteLine($"Тип: {genericTriangle.GetTriangleType()}");
                Console.WriteLine($"Периметр: {genericTriangle.GetPerimetr()}");
                Console.WriteLine($"Площа: {genericTriangle.GetArea()}");
                Console.WriteLine($"Кути: {genericTriangle.GetAngles()}");
            }
            else
            {
                Console.WriteLine("Невірні дані для сторін трикутника.");
            }

            Console.WriteLine();

            Console.WriteLine("Введіть сторону рівностороннього трикутника:");
            if (double.TryParse(Console.ReadLine(), out double sideEq) && sideEq > 0)
            {
                Triangle eqTriangle = new EquilateralTriangle(sideEq);
                Console.WriteLine($"Тип: {eqTriangle.GetTriangleType()}");
                Console.WriteLine($"Периметр: {eqTriangle.GetPerimetr()}");
                Console.WriteLine($"Площа: {eqTriangle.GetArea()}");
                Console.WriteLine($"Кути: {eqTriangle.GetAngles()}");
            }
            else
            {
                Console.WriteLine("Невірне значення сторони.");
            }

            Console.WriteLine();

            Console.WriteLine("Введіть сторону трикутника (для AngledTriangle):");
            bool validSide = double.TryParse(Console.ReadLine(), out double sideAT);

            Console.WriteLine("Введіть кут 1 (у градусах):");
            bool validAngle1 = double.TryParse(Console.ReadLine(), out double angle1);

            Console.WriteLine("Введіть кут 2 (у градусах):");
            bool validAngle2 = double.TryParse(Console.ReadLine(), out double angle2);

            if (validSide && validAngle1 && validAngle2 && sideAT > 0 && angle1 > 0 && angle2 > 0 && angle1 + angle2 < 180)
            {
                try
                {
                    Triangle angledTriangle = new AngledTriangle(sideAT, angle1, angle2);
                    Console.WriteLine($"Тип: {angledTriangle.GetTriangleType()}");
                    Console.WriteLine($"Периметр: {angledTriangle.GetPerimetr()}");
                    Console.WriteLine($"Площа: {angledTriangle.GetArea()}");
                    Console.WriteLine($"Кути: {angledTriangle.GetAngles()}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Помилка: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Невірні вхідні дані для кута або сторони.");
            }
        }
    }
}
