using Calc.Patterns;

namespace Calc
{
    public static class Calc
    {
        public static Scanner MultiplyOrDivide(this Scanner scanner)
        {
            return scanner.Pipe(new MultiplyDividePattern(scanner));
        }

        public static Scanner AddOrSubtract(this Scanner scanner)
        {
            return scanner.Pipe(new AddSubtractPattern(scanner));
        }

        public static Scanner Brackets(this Scanner scanner)
        {
            return scanner.Pipe(new BracketsPattern(scanner));
        }
    }
}