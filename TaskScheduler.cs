using System;
using System.Collections.Generic;

class TaskScheduler<TTask, TPriority> where TPriority : IComparable<TPriority>
{
    private Queue<(TTask task, TPriority priority)> taskQueue = new Queue<(TTask, TPriority)>();
    private Func<TTask, TPriority> getPriority;

    public TaskScheduler(Func<TTask, TPriority> priorityFunction)
    {
        getPriority = priorityFunction;
    }

    public void AddTask(TTask task)
    {
        TPriority priority = getPriority(task);
        taskQueue.Enqueue((task, priority));
        Console.WriteLine($"Task added with priority {priority}");
    }

    public void ExecuteNext(TaskExecution<TTask> executionFunction)
    {
        if (taskQueue.Count > 0)
        {
            var nextTask = taskQueue.Dequeue();
            Console.WriteLine($"Executing task with priority {nextTask.priority}");
            executionFunction(nextTask.task);
        }
        else
        {
            Console.WriteLine("No tasks to execute.");
        }
    }

    public void ReturnToPool(TaskExecution<TTask> poolFunction, TTask task)
    {
        Console.WriteLine("Returning task to pool.");
        poolFunction(task);
    }
}

delegate void TaskExecution<TTask>(TTask task);

class Program
{
    static void Main()
    {
        TaskScheduler<string, int> stringTaskScheduler = new TaskScheduler<string, int>(s => s.Length);
        stringTaskScheduler.AddTask("Task 1");
        stringTaskScheduler.AddTask("Task 2");
        stringTaskScheduler.ExecuteNext(task => Console.WriteLine($"Executing: {task}"));

        TaskScheduler<int, DateTime> dateTaskScheduler = new TaskScheduler<int, DateTime>(n => DateTime.Now.AddSeconds(n));
        dateTaskScheduler.AddTask(5);
        dateTaskScheduler.AddTask(3);
        dateTaskScheduler.ExecuteNext(task => Console.WriteLine($"Executing: {task}"));

        TaskScheduler<int, int> intTaskScheduler = new TaskScheduler<int, int>(n => n);
        intTaskScheduler.AddTask(10);
        intTaskScheduler.ReturnToPool(task => Console.WriteLine($"Returning to pool: {task}"), 10);
    }
}
