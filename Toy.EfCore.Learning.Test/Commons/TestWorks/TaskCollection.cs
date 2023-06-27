using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Toy.EfCore.Learning.Test.Commons.TestWorks;

public class TaskCollection
{
    private readonly List<Func<Task>> _tasks = new();

    public void Register(Func<Task> task)
    {
        _tasks.Add(task);
    }

    public async Task ExecuteAllAsync()
    {
        foreach (var task in _tasks)
        {
            await task();
        }
    }

    public void Clear() => _tasks.Clear();
}