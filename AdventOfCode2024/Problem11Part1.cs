namespace AdventOfCode2024
{
    public static class Problem11Part1
    {
        public static void Solve()
        {
            var reader = File.OpenText("input.txt");

            var stones = reader.ReadLine().Split().Select(UInt64.Parse).ToList();

            ulong count = 0;

            foreach (var stone in stones)
            {
                count += CalcCount(stone, 25);
            }

            Console.WriteLine(count);
        }

        private static ulong CalcCount(ulong stone, int lifeCounter)
        {
            if (lifeCounter == 0)
                return 1;

            if (stone == 0)
                return CalcCount(1, lifeCounter - 1);

            var digitCount = (int)Math.Log10(stone) + 1;

            if (digitCount % 2 == 0)
            {
                var tens = (ulong)Math.Pow(10, digitCount / 2);

                var firstHalf = stone / tens;
                var secondHalf = stone % tens;

                return CalcCount(firstHalf, lifeCounter - 1) + CalcCount(secondHalf, lifeCounter - 1);
            }

            return CalcCount(stone * 2024, lifeCounter - 1);
        }
    }
}