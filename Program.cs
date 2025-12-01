using System;
using System.Text;

namespace LabWork
{
    // ==========================================
    // Базовий клас: Одновимірний вектор (розмір 4)
    // ==========================================
    public class Vector4
    {
        // Інкапсуляція: поле доступне лише всередині класу
        // Code Convention: приватні поля через underscore та camelCase
        private int[] _elements;
        protected const int Size = 4;

        public Vector4()
        {
            _elements = new int[Size];
        }

        // Віртуальний метод: дозволяє перевизначення у спадкоємцях
        public virtual void SetElements()
        {
            Random rnd = new Random();
            Console.WriteLine("--> Генерація елементів ВЕКТОРА...");
            for (int i = 0; i < Size; i++)
            {
                _elements[i] = rnd.Next(1, 50);
            }
        }

        public virtual void Print()
        {
            Console.WriteLine("Вектор:");
            Console.WriteLine("[ " + string.Join(", ", _elements) + " ]");
        }

        public virtual int GetMaxElement()
        {
            int max = _elements[0];
            foreach (var item in _elements)
            {
                if (item > max) max = item;
            }
            return max;
        }
    }

    // ==========================================
    // Похідний клас: Матриця (4x4)
    // Успадковує Vector4 згідно завдання
    // ==========================================
    public class Matrix4x4 : Vector4
    {
        private int[,] _matrixElements;

        public Matrix4x4()
        {
            _matrixElements = new int[Size, Size];
        }

        // Перевизначення (override) логіки для матриці
        public override void SetElements()
        {
            Random rnd = new Random();
            Console.WriteLine("--> Генерація елементів МАТРИЦІ...");
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    _matrixElements[i, j] = rnd.Next(1, 100);
                }
            }
        }

        public override void Print()
        {
            Console.WriteLine("Матриця (4x4):");
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Console.Write($"{_matrixElements[i, j],4} ");
                }
                Console.WriteLine();
            }
        }

        public override int GetMaxElement()
        {
            int max = _matrixElements[0, 0];
            foreach (var item in _matrixElements)
            {
                if (item > max) max = item;
            }
            return max;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // 1. Змінна базового класу (посилання/вказівник)
            // Поки що вона ні на що не посилається (null)
            Vector4 dataProcessor = null;

            Console.WriteLine("Виберіть режим роботи:");
            Console.WriteLine("1 - Працювати з Вектором");
            Console.WriteLine("Будь-яка інша клавіша - Працювати з Матрицею");
            Console.Write("Ваш вибір: ");

            // Зчитуємо вибір користувача
            char userChoose = Console.ReadKey().KeyChar;
            Console.WriteLine("\n" + new string('-', 30));

            // 2. Динамічне створення об'єкта (на етапі виконання)
            if (userChoose == '1')
            {
                // Створюємо, ініціалізуємо об'єкт Вектора
                Console.WriteLine("Обрано: Вектор");
                dataProcessor = new Vector4();
            }
            else
            {
                // Створюємо, ініціалізуємо об'єкт Матриці
                Console.WriteLine("Обрано: Матриця");
                dataProcessor = new Matrix4x4();
            }

            Console.WriteLine(new string('-', 30));

            // 3. Поліморфний виклик методів
            // Компілятор бачить, що ми викликаємо метод у змінної типу Vector4.
            // Але завдяки 'virtual' і 'override', під час виконання (Runtime)
            // програма перевіряє, який саме об'єкт там лежить (Вектор чи Матриця)
            // і викликає правильний метод.

            dataProcessor.SetElements();    // Викличеться або Vector4.SetElements, або Matrix4x4.SetElements

            Console.WriteLine();
            dataProcessor.Print();          // Викличеться відповідний Print

            int maxVal = dataProcessor.GetMaxElement(); // Викличеться відповідний GetMaxElement

            Console.WriteLine(new string('-', 30));
            Console.WriteLine($"Максимальний елемент: {maxVal}");

            Console.ReadKey();
        }
    }
}