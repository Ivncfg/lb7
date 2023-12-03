using System;

class Calculator<T>
{
    public delegate T OperationDelegate(T a, T b);

    public OperationDelegate Add { get; set; }
    public OperationDelegate Subtract { get; set; }
    public OperationDelegate Multiply { get; set; }
    public OperationDelegate Divide { get; set; }

    public Calculator()
    {
        // Ініціалізація делегатів для арифметичних операцій
        Add = (a, b) => Operator<T>.Add(a, b);
        Subtract = (a, b) => Operator<T>.Subtract(a, b);
        Multiply = (a, b) => Operator<T>.Multiply(a, b);
        Divide = (a, b) => Operator<T>.Divide(a, b);
    }

    public void PerformOperations(T a, T b)
    {
        Console.WriteLine($"Addition: {Add(a, b)}");
        Console.WriteLine($"Subtraction: {Subtract(a, b)}");
        Console.WriteLine($"Multiplication: {Multiply(a, b)}");
        Console.WriteLine($"Division: {Divide(a, b)}");
    }
}

// Клас для реалізації арифметичних операцій
static class Operator<T>
{
    public static T Add(T a, T b) => (dynamic)a + b;
    public static T Subtract(T a, T b) => (dynamic)a - b;
    public static T Multiply(T a, T b) => (dynamic)a * b;
    public static T Divide(T a, T b) => (dynamic)a / b;
}

class Program
{
    static void Main()
    {
        // Приклад використання
        Calculator<int> intCalculator = new Calculator<int>();
        intCalculator.PerformOperations(5, 3);

        Calculator<double> doubleCalculator = new Calculator<double>();
        doubleCalculator.PerformOperations(5.5, 2.2);
    }
}
