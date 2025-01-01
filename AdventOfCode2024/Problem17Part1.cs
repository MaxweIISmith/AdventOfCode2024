using System.Text.RegularExpressions;

namespace AdventOfCode2024
{
    public static class Problem17Part1
    {
        public static void Solve()
        {
            var reader = File.OpenText("input.txt");

            var registerRegex = new Regex("Register \\S: (?<val>\\d+)");

            var a = Int32.Parse(registerRegex.Match(reader.ReadLine()).Groups["val"].Value);
            var b = Int32.Parse(registerRegex.Match(reader.ReadLine()).Groups["val"].Value);
            var c = Int32.Parse(registerRegex.Match(reader.ReadLine()).Groups["val"].Value);

            reader.ReadLine();

            var programRegex = new Regex("Program: (?<prog>(\\d+,)+\\d)");

            var program = programRegex.Match(reader.ReadLine()).Groups["prog"].Value
                .Split(',').Select(Int32.Parse).ToList();

            var result = new List<int>();

            Func<int, int> combo = v =>
            {
                switch (v)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                        return v;
                    case 4:
                        return a;
                    case 5:
                        return b;
                    case 6:
                        return c;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            };

            int index = 0;
            while (true)
            {


                if (index >= program.Count)
                    break;

                var command = program[index];
                var operand = program[index + 1];

                switch (command)
                {
                    case 0:
                        a >>= combo(operand);
                        break;
                    case 1:
                        b ^= operand;
                        break;
                    case 2:
                        b = combo(operand) & 0x7;
                        break;
                    case 3:
                        if (a != 0)
                        {
                            index = operand;
                            continue;
                        }
                        break;
                    case 4:
                        b ^= c;
                        break;
                    case 5:
                        result.Add(combo(operand) & 0x7);

                        Console.WriteLine($"a = {a}");
                        Console.WriteLine($"b = {b}");
                        Console.WriteLine($"c = {c}");
                        Console.WriteLine(result.Last());
                        break;
                    case 6:
                        b = a >> combo(operand);
                        break;
                    case 7:
                        c = a >> combo(operand);
                        break;
                }

                index += 2;
            }

            Console.WriteLine(String.Join(",", result));
        }
    }
}