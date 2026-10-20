using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;

BenchmarkRunner.Run<SplitBenchmarks>();


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

[MemoryDiagnoser]
public class ContainsBenchmarks
{
    private List<int> _list = null!;
    private HashSet<int> _hashSet = null!;

    [Params(100, 1_000, 10_000, 100_000)]
    public int Size;

    [GlobalSetup]
    public void Setup()
    {
        _list = Enumerable.Range(1, Size).ToList();
        _hashSet = Enumerable.Range(1, Size).ToHashSet();
    }

    [Benchmark]
    public bool ListContains()
    {
        return _list.Contains(Size);
    }

    [Benchmark]
    public bool HashSetContains()
    {
        return _hashSet.Contains(Size);
    }
}

[MemoryDiagnoser]
public class StringBenchmarks
{
    private string firstName = "Arthur";
    private string lastName = "Policarpio";

    [Benchmark]
    public string Concat()
    {
        return firstName + " " + lastName;
    }

    [Benchmark]
    public string StringBuilder()
    {
        var sb = new StringBuilder();

        sb.Append(firstName);
        sb.Append(" ");
        sb.Append(lastName);

        return sb.ToString();
    }

    [Benchmark]
    public string PlusLoop()
    {
        string result = "";

        for (int i = 0; i < 100; i++)
        {
            result += i;
        }

        return result;
    }

    [Benchmark]
    public string StringBuilderLoop()
    {
        var sb = new StringBuilder();

        for (int i = 0; i < 100; i++)
        {
            sb.Append(i);
        }

        return sb.ToString();
    }
}

[MemoryDiagnoser]
public class SplitBenchmarks
{
    private readonly string _csv =
        string.Join(",",
            Enumerable.Range(1, 100));

    [Benchmark]
    public string[] Split()
    {
        return _csv.Split(',');
    }

    [Benchmark]
    public int CountCommas()
    {
        int count = 0;

        foreach (var ch in _csv)
        {
            if (ch == ',')
                count++;
        }

        return count;
    }
}

public class User
{


}
