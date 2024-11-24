using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    // Клас, що представляє дріб
    class Fraction
    {
        public long Numerator { get; private set; }
        public long Denominator { get; private set; }

        public Fraction(long numerator, long denominator)
        {
            if (denominator == 0)
            {
                throw new ArgumentException("Denominator cannot be zero.");
            }

            // Автоматично спрощуємо дріб при створенні
            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }

            long gcd = MathUtils.GreatestCommonDivisor(Math.Abs(numerator), Math.Abs(denominator));
            Numerator = numerator / gcd;
            Denominator = denominator / gcd;
        }

        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }

        // Метод для відображення дробу з виділеною цілою частиною
        public string ToMixedString()
        {
            long absNumerator = Math.Abs(Numerator);
            long remainder = absNumerator % Denominator;
            long integerPart = absNumerator / Denominator;

            if (Numerator < 0)
                return $"-({integerPart}+{remainder}/{Denominator})";
            else
                return $"({integerPart}+{remainder}/{Denominator})";
        }

        // Метод для отримання дійсного значення дробу
        public double ToDouble()
        {
            return (double)Numerator / Denominator;
        }

        // Операції над дробами
        public static Fraction operator +(Fraction f1, Fraction f2)
        {
            long numerator = f1.Numerator * f2.Denominator + f2.Numerator * f1.Denominator;
            long denominator = f1.Denominator * f2.Denominator;
            return new Fraction(numerator, denominator);
        }

        public static Fraction operator -(Fraction f1, Fraction f2)
        {
            long numerator = f1.Numerator * f2.Denominator - f2.Numerator * f1.Denominator;
            long denominator = f1.Denominator * f2.Denominator;
            return new Fraction(numerator, denominator);
        }

        public static Fraction operator *(Fraction f1, Fraction f2)
        {
            long numerator = f1.Numerator * f2.Numerator;
            long denominator = f1.Denominator * f2.Denominator;
            return new Fraction(numerator, denominator);
        }

        public static Fraction operator /(Fraction f1, Fraction f2)
        {
            if (f2.Numerator == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }
            long numerator = f1.Numerator * f2.Denominator;
            long denominator = f1.Denominator * f2.Numerator;
            return new Fraction(numerator, denominator);
        }
    }

    // Клас для математичних операцій
    static class MathUtils
    {
        // Алгоритм Евкліда для знаходження НСД
        public static long GreatestCommonDivisor(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }

    // Головний клас програми
    class FractionCalculator
    {
        public void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Виберіть операцію:\n1 - один дріб\n2 - два дроби\n3 - калькулятор дробів\n-> ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ProcessSingleFraction();
                    break;
                case 2:
                    ProcessTwoFractions();
                    break;
                case 3:
                    ProcessFractionExpressions();
                    break;
                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }
        }

        private void ProcessSingleFraction()
        {
            Fraction fraction = InputFraction();
            Console.WriteLine($"Дріб: {fraction}");
            Console.WriteLine($"Дріб з виділеною цілою частиною: {fraction.ToMixedString()}");
            Console.WriteLine($"Дійсне значення дробу: {fraction.ToDouble():F3}");
        }

        private void ProcessTwoFractions()
        {
            Fraction firstFraction = InputFraction();
            Fraction secondFraction = InputFraction();

            Console.WriteLine($"\nСума двох дробів: {firstFraction + secondFraction}");
            Console.WriteLine($"Різниця двох дробів: {firstFraction - secondFraction}");
            Console.WriteLine($"Добуток двох дробів: {firstFraction * secondFraction}");
            Console.WriteLine($"Частка двох дробів: {firstFraction / secondFraction}");
        }

        private void ProcessFractionExpressions()
        {
            Console.Write("Введіть число -> ");
            int n = int.Parse(Console.ReadLine());
            Console.Write("Виберіть функцію:\n1 - CalcExpr1\n2 - CalcExpr2\n-> ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Fraction result1 = CalcExpr1(n);
                    Console.WriteLine($"Результат: {result1}");
                    break;
                case 2:
                    Fraction result2 = CalcExpr2(n);
                    Console.WriteLine($"Результат: {result2}");
                    break;
                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }
        }

        // Введення дробу з консолі
        private Fraction InputFraction()
        {
            Console.Write("Введіть значення чисельника -> ");
            long numerator = long.Parse(Console.ReadLine());
            Console.Write("Введіть значення знаменника -> ");
            long denominator = long.Parse(Console.ReadLine());
            return new Fraction(numerator, denominator);
        }

        // Обчислення виразів
        private Fraction CalcExpr1(int n)
        {
            Fraction result = new Fraction(1, 1);
            for (int i = 1; i <= n; i++)
            {
                Fraction add = new Fraction(1, i * (i + 1));
                result += add;
            }
            return result;
        }

        private Fraction CalcExpr2(int n)
        {
            Fraction result = new Fraction(0, 1);
            Fraction one = new Fraction(1, 1);
            for (int i = 2; i <= n; i++)
            {
                Fraction add = new Fraction(1, i * i);
                Fraction subtracted = one - add;
                result *= subtracted;
            }
            return result;
        }
    }

    // Точка входу в програму
    class Program
    {
        static void Main(string[] args)
        {
            FractionCalculator calculator = new FractionCalculator();
            calculator.Run();
        }
    }
}
