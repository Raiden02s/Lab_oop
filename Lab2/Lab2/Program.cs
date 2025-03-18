using System;
using System.Linq;

class Polynomial
{
    private double[] Coefficients;

    public Polynomial(double[] coefficients) => Coefficients = coefficients;

    public double Evaluate(double x) => Coefficients.Select((c, i) => c * Math.Pow(x, i)).Sum();

    public static Polynomial operator +(Polynomial p1, Polynomial p2) =>
        new Polynomial(p1.Coefficients.Zip(p2.Coefficients, (a, b) => a + b).ToArray());

    public static Polynomial operator -(Polynomial p1, Polynomial p2) =>
        new Polynomial(p1.Coefficients.Zip(p2.Coefficients, (a, b) => a - b).ToArray());

    public static Polynomial operator *(Polynomial p1, Polynomial p2)
    {
        double[] result = new double[p1.Coefficients.Length + p2.Coefficients.Length - 1];
        for (int i = 0; i < p1.Coefficients.Length; i++)
            for (int j = 0; j < p2.Coefficients.Length; j++)
                result[i + j] += p1.Coefficients[i] * p2.Coefficients[j];
        return new Polynomial(result);
    }

    public override string ToString() => string.Join(" + ", Coefficients.Select((c, i) => c != 0 ? $"{c}x^{i}" : "").Where(s => !string.IsNullOrEmpty(s)).Reverse());
}

class Program
{
    static void Main()
    {
        Polynomial p1 = new Polynomial(new double[] { 1, -3, 2 });
        Polynomial p2 = new Polynomial(new double[] { 2, 1 });

        Console.WriteLine($"P1: {p1}\nP2: {p2}\nP1 + P2: {p1 + p2}\nP1 - P2: {p1 - p2}\nP1 * P2: {p1 * p2}\nP1(2): {p1.Evaluate(2)}");
    }
}
