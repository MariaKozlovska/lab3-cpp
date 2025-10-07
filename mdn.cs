using System;

class BisectionConsole
{
    static double f(double x) => x * x - 2 * x - 6;

    static double Bisection(double a, double b, double eps, int maxIter, out int iterations)
    {
        double fa = f(a), fb = f(b);
        iterations = 0;
        if (fa * fb > 0) throw new ArgumentException("f(a) та f(b) повинні мати різні знаки (f(a)*f(b) < 0).");

        double c = a;
        while ((b - a) / 2.0 > eps && iterations < maxIter)
        {
            c = (a + b) / 2.0;
            double fc = f(c);

            if (fc == 0.0)
            {
                iterations++;
                return c;
            }

            if (fa * fc < 0)
            {
                b = c;
                fb = fc;
            }
            else
            {
                a = c;
                fa = fc;
            }
            iterations++;
        }
        return (a + b) / 2.0;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Метод ділення навпіл. Функція: f(x) = x^2 - 2x - 6");
        double a = 0.0, b = 5.0; // інтервал, де є корінь
        double eps = 1e-6;
        int maxIter = 100;
        Console.WriteLine($"Початковий інтервал: a={a}, b={b}, eps={eps}");

        try
        {
            int it;
            double root = Bisection(a, b, eps, maxIter, out it);
            Console.WriteLine($"\nЗнайдений корінь: x ≈ {root}");
            Console.WriteLine($"f(x) = {f(root)}");
            Console.WriteLine($"Кількість ітерацій: {it}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }

        Console.WriteLine("\nНатисніть ENTER для виходу...");
        Console.ReadLine();
    }
}
