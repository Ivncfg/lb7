using System;
using System.Collections.Generic;

class Repository<T>
{
    private List<T> elements = new List<T>();

    public void Add(T element)
    {
        elements.Add(element);
    }

    public List<T> Find(Func<T, bool> criteria)
    {
        return elements.FindAll(criteria);
    }
}

class Program
{
    static void Main()
    {
        Repository<int> intRepository = new Repository<int>();
        intRepository.Add(1);
        intRepository.Add(2);
        intRepository.Add(3);

        List<int> result = intRepository.Find(x => x > 1);
        Console.WriteLine(string.Join(", ", result));

        Repository<string> stringRepository = new Repository<string>();
        stringRepository.Add("apple");
        stringRepository.Add("banana");
        stringRepository.Add("orange");

        List<string> stringResult = stringRepository.Find(s => s.Length > 5);
        Console.WriteLine(string.Join(", ", stringResult));
    }
}
