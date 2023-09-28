using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        int numThreads = Environment.ProcessorCount;

        if (args.Length > 0)
        {
            if (int.TryParse(args[0], out int parsedThreads) && parsedThreads > 0)
            {
                numThreads = parsedThreads;
            }
            else
            {
                Console.WriteLine("Invalid command line parameter. We use the default number of threads.");
            }
        }

        int[] numbers = Enumerable.Range(1, 100000000).ToArray();

        Console.WriteLine($"Start processing {numbers.Length} numbers using {numThreads}flows...");

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        var result = numbers.AsParallel()
                            .WithDegreeOfParallelism(numThreads)
                            .Where(x => x % 2 == 0)
                            .Select(x => x * x)
                            .ToList();

        stopwatch.Stop();

        Console.WriteLine($"Result: {result.Count}numbers, execution time: {stopwatch.ElapsedMilliseconds} ms");
    }
}
