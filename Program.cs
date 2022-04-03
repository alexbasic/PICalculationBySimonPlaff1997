using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PICalculationBySimonPlaff1997
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var calculator = new PICalculationBySimonPlaff();
            var timer = new Stopwatch();
            Console.WriteLine($"Start at {DateTime.Now}");
            timer.Start();
            var (value, count) = calculator.Calc(cancellationTokenSource.Token).Result;
            timer.Stop();
            Console.WriteLine($"Stop at {DateTime.Now}");
            Console.WriteLine($"value is {value} and num iterations is {count}");
            Console.WriteLine($"Elapsed millisaconds is {timer.ElapsedMilliseconds}");
            Console.WriteLine($"Press any key...");
            Console.ReadKey();
        }
    }

    public class PICalculationBySimonPlaff
    {
        public Task<(double, int)> Calc(CancellationToken token)
        {
            return Task.Factory.StartNew(() =>
            {
                var result = 0d;
                var n = 500_000_000;

                for (var k = 0; k <= n; k++)
                {
                    if (token.IsCancellationRequested)
                        return (result, k);
                    result += (1d / Math.Pow(16d, k)) * ((4d / (8d * k + 1)) - (2d / (8d * k + 4)) - (1d / (8d * k + 5)) - (1d / (8d * k + 6)));
                }
                return (result, n);
            });
        }
    }
}
