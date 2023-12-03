using System;
using System.Collections.Generic;

class FunctionCache<TKey, TResult>
{
    private Dictionary<TKey, TResult> cache = new Dictionary<TKey, TResult>();
    private Func<TKey, TResult> userFunction;

    public FunctionCache(Func<TKey, TResult> function)
    {
        userFunction = function;
    }

    public TResult GetResult(TKey key)
    {
        if (cache.ContainsKey(key))
        {
            Console.WriteLine($"Result for key '{key}' found in cache.");
            return cache[key];
        }
        else
        {
            TResult result = userFunction(key);
            cache[key] = result;
            Console.WriteLine($"Result for key '{key}' calculated and cached.");
            return result;
        }
    }
}

class Program
{
    static void Main()
    {
        Func<string, int> stringLengthFunction = s => s.Length;
        FunctionCache<string, int> stringLengthCache = new FunctionCache<string, int>(stringLengthFunction);

        Console.WriteLine(stringLengthCache.GetResult("apple"));  // Calculates and caches result
        Console.WriteLine(stringLengthCache.GetResult("apple"));  // Retrieves result from cache

        Func<int, double> squareRootFunction = n => Math.Sqrt(n);
        FunctionCache<int, double> squareRootCache = new FunctionCache<int, double>(squareRootFunction);

        Console.WriteLine(squareRootCache.GetResult(16));  // Calculates and caches result
        Console.WriteLine(squareRootCache.GetResult(16));  // Retrieves result from cache
    }
}
