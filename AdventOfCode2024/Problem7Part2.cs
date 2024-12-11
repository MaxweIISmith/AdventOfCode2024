namespace AdventOfCode2024;

public static class Problem7Part2
{
    public static void Solve()
    {
        var reader = File.OpenText("input.txt");

        ulong sum = 0;

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            if (string.IsNullOrEmpty(line))
                continue;

            var parts = line.Split(':');

            var result = ulong.Parse(parts[0]);

            var operands = parts[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(ulong.Parse)
                .ToList();

            var n = (int)Math.Pow(3, operands.Count - 1);

            for (int i = 0; i < n; i++)
            {
                var operators = new List<char>();

                var digits = ConvertDecToBase(i, 3);

                for (int j = 0; j < operands.Count - 1; j++)
                {
                    var signIndex = digits.Count > j ? digits[j] : 0;

                    if (signIndex == 0)
                        operators.Add('+');
                    else if (signIndex == 1)
                        operators.Add('*');
                    else
                        operators.Add('|');
                }

                var operandsCopy = operands.ToList();
                if (result == Compute(operandsCopy, operators))
                {
                    sum += result;
                    break;
                }
            }
        }

        Console.WriteLine(sum);
    }

    private static List<int> ConvertDecToBase(int number, int @base)
    {
        var result = new List<int>();

        do
        {
            result.Add(number % @base);
            number /= @base;
        } while (number > 0);

        return result;
    }

    private static ulong Compute(List<ulong> operands, List<char> operators)
    {
        if (operands.Count == 1)
            return operands[0];

        var op = operators[0];

        if (op == '+')
        {
            operands[1] += operands[0];
        }
        else if (op == '*')
        {
            operands[1] *= operands[0];
        }
        else if (op == '|')
        {
            var deg = (int)Math.Log10(operands[1]);

            operands[1] = operands[0] * (ulong)Math.Pow(10, deg + 1) + operands[1];
        }

        operands.RemoveAt(0);
        operators.RemoveAt(0);

        return Compute(operands, operators);
    }
}