using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public partial class MyMatrix
    {
        // Оператор додавання
        public static MyMatrix operator +(MyMatrix a, MyMatrix b)
        {
            if (a.Height != b.Height || a.Width != b.Width)
                throw new ArgumentException("Matrices must have the same dimensions for addition.");

            double[,] result = new double[a.Height, a.Width];
            for (int i = 0; i < a.Height; i++)
            {
                for (int j = 0; j < a.Width; j++)
                {
                    result[i, j] = a[i, j] + b[i, j];
                }
            }

            return new MyMatrix(result);
        }

        // Оператор множення
        public static MyMatrix operator *(MyMatrix a, MyMatrix b)
        {
            if (a.Width != b.Height)
                throw new ArgumentException("The number of columns of the first matrix must equal the number of rows of the second matrix.");

            double[,] result = new double[a.Height, b.Width];
            for (int i = 0; i < a.Height; i++)
            {
                for (int j = 0; j < b.Width; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < a.Width; k++)
                    {
                        result[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return new MyMatrix(result);
        }

        // Метод GetTransponedArray
        private double[,] GetTransponedArray()
        {
            double[,] transposed = new double[Width, Height];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    transposed[j, i] = _data[i, j];
                }
            }
            return transposed;
        }

        // Метод GetTransponedCopy
        public MyMatrix GetTransponedCopy()
        {
            return new MyMatrix(GetTransponedArray());
        }

        // Метод TransponeMe
        public void TransponeMe()
        {
            _data = GetTransponedArray();
        }
    }
}
