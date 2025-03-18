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
        Pair c1 = new Complex(2, 3);
        Pair c2 = new Complex(1, -4);
        Console.WriteLine(c1.Add(c2));

        Pair r1 = new Rational(1, 2);
        Pair r2 = new Rational(1, 3);
        Console.WriteLine(r1.Add(r2));
    }
}