using System;
using Calc.Patterns;

namespace Calc
{
    class Program
    {
        static void Main(string[] args)
        {
            Scanner.AddPattern<BracketsPattern>();
            Scanner.AddPattern<MultiplyDividePattern>();
            Scanner.AddPattern<AddSubtractPattern>();

            foreach (var input in args)
            {
                var scanner = new Scanner(input);
                var value = scanner.Scan().Compute();
                Console.WriteLine($"{input.Replace(" ", "")}={value}");
            }

            Console.ReadLine();
        }
    }
}
