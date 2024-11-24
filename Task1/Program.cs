using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Введення першої матриці
                Console.WriteLine("Enter the first matrix:");
                MyMatrix matrix1 = MyMatrix.ReadFromConsole();
                Console.WriteLine("First matrix:");
                Console.WriteLine(matrix1);

                // Введення другої матриці
                Console.WriteLine("\nEnter the second matrix:");
                MyMatrix matrix2 = MyMatrix.ReadFromConsole();
                Console.WriteLine("Second matrix:");
                Console.WriteLine(matrix2);

                // Додавання матриць
                try
                {
                    MyMatrix sum = matrix1 + matrix2;
                    Console.WriteLine("\nThe sum of the matrices is:");
                    Console.WriteLine(sum);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError adding matrices: {ex.Message}");
                }

                // Множення матриць
                try
                {
                    MyMatrix product = matrix1 * matrix2;
                    Console.WriteLine("\nThe product of the matrices is:");
                    Console.WriteLine(product);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError multiplying matrices: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
