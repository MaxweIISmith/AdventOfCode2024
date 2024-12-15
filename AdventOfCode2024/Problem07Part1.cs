namespace AdventOfCode2024;

public static class Problem07Part1
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

            var n = (int)Math.Pow(2, operands.Count - 1);

            for (int i = 0; i < n; i++)
            {
                var operators = new List<char>();

                for (int j = 0; j < operands.Count - 1; j++)
                {
                    operators.Add(((i >> j) & 0x1) == 0x0 ? '+' : '*');
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

    private static ulong Compute(List<ulong> operands, List<char> operators)
    {
        if (operands.Count == 1)
            return operands[0];

        var op = operators[0];

        if (op == '+')
        {
            operands[1] += operands[0];
        }
        else
        {
            operands[1] *= operands[0];
        }

        operands.RemoveAt(0);
        operators.RemoveAt(0);

        return Compute(operands, operators);
    }
}