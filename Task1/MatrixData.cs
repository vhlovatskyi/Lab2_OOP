using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public partial class MyMatrix
    {
        private double[,] _data;

        // Конструктор копіювання, створює нову матрицю на основі існуючої, копіюючи її дані.
        public MyMatrix(MyMatrix other)
        {
            int rows = other.Height;
            int cols = other.Width;
            _data = new double[rows, cols];
            Array.Copy(other._data, _data, other._data.Length);
        }

        // Конструктор створює матрицю з двовимірного масиву.
        public MyMatrix(double[,] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            _data = (double[,])array.Clone();   // Метод .Clone() повертає поверхневу копію масиву, тобто створює новий масив такого ж розміру та копіює всі елементи з оригінального масиву.
        }

        // Конструктор створює матрицю з "зубчастого" масиву, перевіряючи, чи є масив прямокутним.
        public MyMatrix(double[][] jaggedArray)
        {
            if (jaggedArray == null || jaggedArray.Length == 0)
                throw new ArgumentException("Invalid jagged array.");

            int rows = jaggedArray.Length;
            int cols = jaggedArray[0].Length;   // Припускається, що перший рядок (jaggedArray[0]) не порожній, інакше буде кинутий виняток під час доступу до jaggedArray[0].Length

            // Перевірка, чи масив фактично прямокутний
            if (!jaggedArray.All(row => row.Length == cols))    // Перевіряє, чи всі рядки в масиві мають однакову довжину.
                throw new ArgumentException("Jagged array is not rectangular.");

            _data = new double[rows, cols];
            for (int i = 0; i < rows; i++)  // Копіювання даних з "зубчастого" масиву до _data
            {
                for (int j = 0; j < cols; j++)
                {
                    _data[i, j] = jaggedArray[i][j];
                }
            }
        }

        // Конструктор з масиву рядків
        public MyMatrix(string[] lines)
        {
            if (lines == null || lines.Length == 0)
                throw new ArgumentException("Invalid string array.");

            // Парсинг чисел з рядків
            double[][] parsedArray = lines.Select(line =>
                line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(double.Parse).ToArray()
            ).ToArray();

            // line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries) розбиває рядок на окремі частини, використовуючи пробіли(' ') або табуляції('\t') як роздільники. StringSplitOptions.RemoveEmptyEntries ігнорує порожні елементи.
            // Select(double.Parse) парсить кожен фрагмент як число типу double.
            // .ToArray() перетворює колекцію чисел у масив.
            // ToArray() ззовні перетворює результат у зубчастий масив double[][], де кожен рядок — це масив чисел.

            // Використання конструктора з "зубчастого" масиву
            _data = new MyMatrix(parsedArray)._data;

            // Використовується вже наявний конструктор MyMatrix, який приймає "зубчастий" масив double[][].
            // Створюється новий об'єкт MyMatrix на основі parsedArray.
            // Потім значення внутрішнього двовимірного масиву _data копіюються з об'єкта, створеного з "зубчастого" масиву, у _data поточного об'єкта.
            // Цей підхід дозволяє уникнути дублювання коду і використовує вже перевірений логічний блок для створення матриці з "зубчастого" масиву.
        }

        // Конструктор з рядка
        public MyMatrix(string matrixString)
        {
            if (string.IsNullOrWhiteSpace(matrixString))
                throw new ArgumentException("Invalid matrix string.");

            string[] lines = matrixString.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            // new[] { '\n', '\r' } — масив символів роздільників: новий рядок(\n) і повернення каретки(\r).

            // Використання конструктора з масиву рядків
            _data = new MyMatrix(lines)._data;

            // Створюється новий об'єкт MyMatrix, використовуючи конструктор, який приймає масив рядків (string[] lines).
            // Конструктор з масиву рядків вже відповідає за парсинг чисел і перетворення рядків у матрицю.
            // Внутрішній двовимірний масив _data копіюється з об'єкта, створеного на основі масиву рядків.
        }

        // Властивості Height та Width
        public int Height => _data.GetLength(0);
        public int Width => _data.GetLength(1);

        // Java-style getter-и
        public int getHeight() => Height;
        public int getWidth() => Width;

        // Індексатор
        public double this[int row, int col]
        {
            get
            {
                if (row < 0 || row >= Height || col < 0 || col >= Width)
                    throw new IndexOutOfRangeException("Invalid matrix index.");
                return _data[row, col];
            }
            set
            {
                if (row < 0 || row >= Height || col < 0 || col >= Width)
                    throw new IndexOutOfRangeException("Invalid matrix index.");
                _data[row, col] = value;
            }
        }

        // Java-style getter та setter для окремого елемента
        public double GetElement(int row, int col) => this[row, col];
        public void SetElement(int row, int col, double value) => this[row, col] = value;

        // Перевизначений метод ToString
        public override string ToString()
        {
            return string.Join("\n", Enumerable.Range(0, Height).Select(i =>
                string.Join("\t", Enumerable.Range(0, Width).Select(j => _data[i, j].ToString()))
            ));

            // Enumerable.Range(0, Height) створює послідовність цілих чисел від 0 до Height -1(це індекси рядків матриці).
            // Кожен елемент цієї послідовності відповідає індексу рядка у матриці.

        }

        // Статичний метод для введення матриці з клавіатури
        public static MyMatrix ReadFromConsole()
        {
            Console.Write("Enter the number of rows: ");
            if (!int.TryParse(Console.ReadLine(), out int rows) || rows <= 0)
                throw new ArgumentException("Invalid number of rows.");

            Console.Write("Enter the number of columns: ");
            if (!int.TryParse(Console.ReadLine(), out int cols) || cols <= 0)
                throw new ArgumentException("Invalid number of columns.");

            double[,] data = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine($"Enter row {i + 1} values (separated by spaces):");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    throw new ArgumentException("Invalid row input.");

                string[] values = input.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length != cols)
                    throw new ArgumentException("Number of values does not match the number of columns.");

                for (int j = 0; j < cols; j++)
                {
                    if (!double.TryParse(values[j], out double element))
                        throw new ArgumentException("Invalid number format.");
                    data[i, j] = element;
                }
            }

            return new MyMatrix(data);
        }
    }
}
