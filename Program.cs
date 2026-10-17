using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

BenchmarkRunner.Run<LoopBenchmarks>();


[MemoryDiagnoser]
public class FirstBenchmark
{
    [Benchmark]
    public int AddNumbers()
    {
        return 1 + 1;

    }

    [Benchmark]
    public User CreateUser()
    {
        return new User();
    }

    [Benchmark]
    [ArgumentsSource(nameof(GetName))]
    public string BuildName(string firstName, string lastName)
    {
        return firstName + " " + lastName;
    }

    public IEnumerable<object[]> GetName()
    {
        yield return new object[] { "arthur", "policarpio" };
    }

}

[MemoryDiagnoser]
[DisassemblyDiagnoser]
public class LoopBenchmarks
{
    private int[] numbers;

    [GlobalSetup]
    public void Setup()
    {
        numbers = Enumerable.Range(1, 1000).ToArray();
    }

    [Benchmark]
    public int ForLoop()
    {
        int sum = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            sum += numbers[i];
        }
        return sum;
    }

    [Benchmark]
    public int ForeachLoop()
    {
        int sum = 0;
        foreach (var n in numbers)
        {
            sum += n;

        }
        return sum;
    }

    [Benchmark]
    public int LinqSum()
    {
        return numbers.Sum();

    }

}

public class User
{


}
