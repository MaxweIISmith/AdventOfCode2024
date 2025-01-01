using System.Text.RegularExpressions;

namespace AdventOfCode2024
{
    public static class Problem17Part2
    {
        #region Public Methods

        public static void Solve()
        {
            var reader = File.OpenText("input.txt");

            var registerRegex = new Regex("Register \\S: (?<val>\\d+)");

            var a = ulong.Parse(registerRegex.Match(reader.ReadLine()).Groups["val"].Value);
            var b = ulong.Parse(registerRegex.Match(reader.ReadLine()).Groups["val"].Value);
            var c = ulong.Parse(registerRegex.Match(reader.ReadLine()).Groups["val"].Value);

            reader.ReadLine();

            var programRegex = new Regex("Program: (?<prog>(\\d+,)+\\d)");

            var program = programRegex.Match(reader.ReadLine()).Groups["prog"].Value
                .Split(',').Select(ulong.Parse).ToList();

            var trits = Enumerable.Repeat((ulong)0, 16).ToList();
            var aValue = FindRegisterValue(program, trits, 0);

            Console.WriteLine(aValue);
        }

        private static ulong FindRegisterValue(List<ulong> program, List<ulong> trits, int currentIndex)
        {
            for (int i = 0; i <= 7; i++)
            {
                trits[currentIndex] = (ulong)i;

                ulong aValue = 0;

                foreach (var trit in trits)
                {
                    aValue <<= 3;
                    aValue |= trit;
                }

                var output = RunProgram(aValue, 0, 0, program);

                if (output.Count != program.Count)
                    continue;

                int k = 0;
                var length = program.Count;
                while (k < length && output[length - k - 1] == program[length - k - 1])
                {
                    k++;
                }

                if (k == length)
                    return aValue;

                if (k >= currentIndex && currentIndex < trits.Count - 1)
                {
                    var res = FindRegisterValue(program, trits, currentIndex + 1);
                    if (res > 0)
                    {
                        return res;
                    }
                }
            }

            return 0;
        }

        #endregion Public Methods

        #region Private Methods

        private static List<ulong> RunProgram(ulong a, ulong b, ulong c, List<ulong> program)
        {
            var result = new List<ulong>();

            Func<ulong, ulong> combo = v =>
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
                        a = a >> (int)combo(operand);
                        break;

                    case 1:
                        b ^= operand;
                        break;

                    case 2:
                        b = combo(operand) & 0b111;
                        break;

                    case 3:
                        if (a != 0)
                        {
                            index = (int)operand;
                            continue;
                        }
                        break;

                    case 4:
                        b ^= c;
                        break;

                    case 5:
                        result.Add(combo(operand) & 0b111);
                        break;

                    case 6:
                        b = a >> (int)combo(operand);
                        break;

                    case 7:
                        c = a >> (int)combo(operand);
                        break;
                }

                index += 2;
            }

            return result;
        }

        #endregion Private Methods
    }
}