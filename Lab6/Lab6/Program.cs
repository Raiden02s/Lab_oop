using System;

public struct ComplexNumber
{
    public double Real;  // Дійсна частина
    public double Imaginary;  // Уявна частина

    // Конструктор для ініціалізації комплексного числа
    public ComplexNumber(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    // Перевизначення ToString для зручного виведення комплексного числа
    public override string ToString()
    {
        return $"{Real} + {Imaginary}i";
    }
}

class Program
{
    static void Main()
    {
        // Введення користувачем двох комплексних чисел
        Console.WriteLine("Enter the first complex number (A):");
        Console.Write("Real part: ");
        double realA = double.Parse(Console.ReadLine());
        Console.Write("Imaginary part: ");
        double imaginaryA = double.Parse(Console.ReadLine());

        Console.WriteLine("\nEnter the second complex number (B):");
        Console.Write("Real part: ");
        double realB = double.Parse(Console.ReadLine());
        Console.Write("Imaginary part: ");
        double imaginaryB = double.Parse(Console.ReadLine());

        // Створення комплексних чисел
        ComplexNumber A = new ComplexNumber(realA, imaginaryA);
        ComplexNumber B = new ComplexNumber(realB, imaginaryB);

        // Ділення комплексних чисел
        ComplexNumber result = DivideComplexNumbers(A, B);

        // Виведення результату
        Console.WriteLine($"\nResult of A / B: {result}");
    }

    // Функція для ділення комплексних чисел
    static ComplexNumber DivideComplexNumbers(ComplexNumber A, ComplexNumber B)
    {
        // Перевірка на нульовий знаменник (B = 0)
        if (B.Real == 0 && B.Imaginary == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero (complex number B is zero).");
        }

        // Використовуємо формулу для ділення комплексних чисел:
        // (A / B) = (A * conjugate(B)) / (B * conjugate(B))
        // Де conjugate(B) - комплексне спряження числа B
        double denominator = B.Real * B.Real + B.Imaginary * B.Imaginary;  // |B|^2
        double realPart = (A.Real * B.Real + A.Imaginary * B.Imaginary) / denominator;
        double imaginaryPart = (A.Imaginary * B.Real - A.Real * B.Imaginary) / denominator;

        return new ComplexNumber(realPart, imaginaryPart);
    }
}
