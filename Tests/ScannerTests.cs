using System;
using Calc;
using Calc.Patterns;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ScannerTests
    {
        [SetUp]
        public void ScannerInit()
        {
            Scanner.AddPattern<BracketsPattern>();
            Scanner.AddPattern<MultiplyDividePattern>();
            Scanner.AddPattern<AddSubtractPattern>();
        }

        [TestCase("12.8+3-2*2", (float)11.8)]
        [TestCase("4*3-2*2", (float)8)]
        public void Scanner_TestCompute_Always_Return_ExpectedValue(string input, float result)
        {
            var scanner = new Scanner(input);

            var actual = scanner.Scan().Compute();
            Console.WriteLine(actual);

            Assert.AreEqual(result, actual);
        }

        [TestCase("2^3+4")]
        public void Scanner_InputFormat_Invalid_Test(string input)
        {
            var scanner = new Scanner(input);

            Assert.Throws<ArgumentException>(() => scanner.Scan(), "Неверный формат входной строки");
        }

        [TestCase("12-(3+1)", (float)8)]
        [TestCase("12*(3+1.5)", (float)54)]
        [TestCase("23 * 2 + 45 - 24 / 5", (float)86.2)]
        public void Scanner_TestCompute_WithBrackets_Brackets_Always_Return_ExpectedValue(string input, float result)
        {
            var scanner = new Scanner(input);

            var actual = scanner.Scan().Compute();
            Console.WriteLine(actual);

            Assert.AreEqual(result, actual);
        }
    }
}
