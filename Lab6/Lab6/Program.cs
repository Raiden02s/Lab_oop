using System;

struct Complex
{
    public double Real { get; }
    public double Imaginary { get; }

    public Complex(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public static Complex Divide(Complex a, Complex b)
    {
        double denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;
        if (denominator == 0) throw new DivideByZeroException("Ділення на нуль");

        double realPart = (a.Real * b.Real + a.Imaginary * b.Imaginary) / denominator;
        double imaginaryPart = (a.Imaginary * b.Real - a.Real * b.Imaginary) / denominator;
        return new Complex(realPart, imaginaryPart);
    }

    public override string ToString() => $"{Real} + {Imaginary}i";
}

class Program
{
    static void Main()
    {
        Complex A = new Complex(4, 3);
        Complex B = new Complex(2, -1);
        Complex result = Complex.Divide(A, B);
        Console.WriteLine($"Результат ділення: {result}");
    }
}
