using System;

abstract class Pair
{
    public abstract Pair Add(Pair other);
    public abstract Pair Subtract(Pair other);
    public abstract Pair Multiply(Pair other);
    public abstract Pair Divide(Pair other);
}

class Complex : Pair
{
    public double Real { get; }
    public double Imaginary { get; }

    public Complex(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public override Pair Add(Pair other)
    {
        if (other is Complex c)
            return new Complex(Real + c.Real, Imaginary + c.Imaginary);
        throw new ArgumentException("Invalid type");
    }

    public override Pair Subtract(Pair other)
    {
        if (other is Complex c)
            return new Complex(Real - c.Real, Imaginary - c.Imaginary);
        throw new ArgumentException("Invalid type");
    }

    public override Pair Multiply(Pair other)
    {
        if (other is Complex c)
            return new Complex(Real * c.Real - Imaginary * c.Imaginary, Real * c.Imaginary + Imaginary * c.Real);
        throw new ArgumentException("Invalid type");
    }

    public override Pair Divide(Pair other)
    {
        if (other is Complex c)
        {
            double denominator = c.Real * c.Real + c.Imaginary * c.Imaginary;
            return new Complex((Real * c.Real + Imaginary * c.Imaginary) / denominator,
                               (Imaginary * c.Real - Real * c.Imaginary) / denominator);
        }
        throw new ArgumentException("Invalid type");
    }

    public override string ToString() => $"{Real} + {Imaginary}i";
}

class Rational : Pair
{
    public int Numerator { get; }
    public int Denominator { get; }

    public Rational(int numerator, int denominator)
    {
        if (denominator == 0) throw new DivideByZeroException("Denominator cannot be zero");
        Numerator = numerator;
        Denominator = denominator;
    }

    public override Pair Add(Pair other)
    {
        if (other is Rational r)
            return new Rational(Numerator * r.Denominator + r.Numerator * Denominator, Denominator * r.Denominator);
        throw new ArgumentException("Invalid type");
    }

    public override Pair Subtract(Pair other)
    {
        if (other is Rational r)
            return new Rational(Numerator * r.Denominator - r.Numerator * Denominator, Denominator * r.Denominator);
        throw new ArgumentException("Invalid type");
    }

    public override Pair Multiply(Pair other)
    {
        if (other is Rational r)
            return new Rational(Numerator * r.Numerator, Denominator * r.Denominator);
        throw new ArgumentException("Invalid type");
    }

    public override Pair Divide(Pair other)
    {
        if (other is Rational r)
            return new Rational(Numerator * r.Denominator, Denominator * r.Numerator);
        throw new ArgumentException("Invalid type");
    }

    public override string ToString() => $"{Numerator}/{Denominator}";
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Choose the type of numbers:");
        Console.WriteLine("1. Complex numbers");
        Console.WriteLine("2. Rational numbers");
        string choice = Console.ReadLine();

        Pair num1 = null, num2 = null;
        if (choice == "1")
        {
            // Input complex numbers
            Console.Write("Enter the real part of the first complex number: ");
            double real1 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter the imaginary part of the first complex number: ");
            double imag1 = Convert.ToDouble(Console.ReadLine());
            num1 = new Complex(real1, imag1);

            Console.Write("Enter the real part of the second complex number: ");
            double real2 = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter the imaginary part of the second complex number: ");
            double imag2 = Convert.ToDouble(Console.ReadLine());
            num2 = new Complex(real2, imag2);
        }
        else if (choice == "2")
        {
            // Input rational numbers
            Console.Write("Enter the numerator of the first rational number: ");
            int numerator1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the denominator of the first rational number: ");
            int denominator1 = Convert.ToInt32(Console.ReadLine());
            num1 = new Rational(numerator1, denominator1);

            Console.Write("Enter the numerator of the second rational number: ");
            int numerator2 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the denominator of the second rational number: ");
            int denominator2 = Convert.ToInt32(Console.ReadLine());
            num2 = new Rational(numerator2, denominator2);
        }

        Console.WriteLine("\nChoose the operation:");
        Console.WriteLine("1. Addition");
        Console.WriteLine("2. Subtraction");
        Console.WriteLine("3. Multiplication");
        Console.WriteLine("4. Division");
        string operation = Console.ReadLine();

        Pair result = null;

        switch (operation)
        {
            case "1":
                result = num1.Add(num2);
                break;
            case "2":
                result = num1.Subtract(num2);
                break;
            case "3":
                result = num1.Multiply(num2);
                break;
            case "4":
                result = num1.Divide(num2);
                break;
            default:
                Console.WriteLine("Invalid operation.");
                break;
        }

        if (result != null)
        {
            Console.WriteLine($"Result: {result}");
        }
    }
}

