using System.Text.RegularExpressions;

namespace AdventOfCode2024
{
    public static class Problem13Part2
    {
        public static void Solve()
        {
            var reader = File.OpenText("input.txt");

            long sum = 0;

            var regexA = new Regex("Button A: X\\+(?<a11>\\d+), Y\\+(?<a21>\\d+)");
            var regexB = new Regex("Button B: X\\+(?<a12>\\d+), Y\\+(?<a22>\\d+)");
            var regexPrize = new Regex("Prize: X=(?<b1>\\d+), Y=(?<b2>\\d+)");

            const long offset = 10000000000000;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var match = regexA.Match(line);
                var a11 = Int64.Parse(match.Groups["a11"].Value);
                var a21 = Int64.Parse(match.Groups["a21"].Value);

                line = reader.ReadLine();
                match = regexB.Match(line);
                var a12 = Int64.Parse(match.Groups["a12"].Value);
                var a22 = Int64.Parse(match.Groups["a22"].Value);

                line = reader.ReadLine();
                match = regexPrize.Match(line);
                var b1 = Int64.Parse(match.Groups["b1"].Value) + offset;
                var b2 = Int64.Parse(match.Groups["b2"].Value) + offset;

                reader.ReadLine();

                var det = a11 * a22 - a12 * a21;
                var detA = b1 * a22 - b2 * a12;
                var detB = a11 * b2 - b1 * a21;

                if (detA % det != 0 || detB % det != 0)
                    continue;

                var a = detA / det;
                var b = detB / det;

                sum += a * 3 + b * 1;
            }

            Console.WriteLine(sum);
        }
    }
}