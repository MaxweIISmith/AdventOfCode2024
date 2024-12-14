namespace AdventOfCode2024
{
    public static class Problem11Part2
    {
        #region Private Fields

        private static readonly Dictionary<StoneInfo, ulong> History = new();

        #endregion Private Fields

        #region Public Methods

        public static void Solve()
        {
            var reader = File.OpenText("input.txt");

            var stones = reader.ReadLine().Split().Select(UInt64.Parse).ToList();

            ulong count = 0;

            foreach (var stone in stones)
            {
                count += CalcCount(stone, 75);
            }

            Console.WriteLine(count);
        }

        #endregion Public Methods

        #region Private Methods

        private static ulong CalcCount(ulong stone, int lifeCounter)
        {
            if (History.TryGetValue(new StoneInfo(stone, lifeCounter), out var value))
            {
                return value;
            }

            if (lifeCounter == 0)
                return 1;

            if (stone == 0)
            {
                var res = CalcCount(1, lifeCounter - 1);
                History[new StoneInfo(1, lifeCounter - 1)] = res;
                return res;
            }

            var digitCount = (int)Math.Log10(stone) + 1;

            if (digitCount % 2 == 0)
            {
                var tens = (ulong)Math.Pow(10, digitCount / 2);

                var firstHalf = stone / tens;
                var secondHalf = stone % tens;

                var res1 = CalcCount(firstHalf, lifeCounter - 1);
                History[new StoneInfo(firstHalf, lifeCounter - 1)] = res1;

                var res2 = CalcCount(secondHalf, lifeCounter - 1);
                History[new StoneInfo(secondHalf, lifeCounter - 1)] = res2;

                return res1 + res2;
            }

            var res3 = CalcCount(stone * 2024, lifeCounter - 1);
            History[new StoneInfo(stone * 2024, lifeCounter - 1)] = res3;
            return res3;
        }

        #endregion Private Methods

        #region Private Structs

        private struct StoneInfo : IEquatable<StoneInfo>
        {
            #region Public Constructors

            public StoneInfo(ulong stone, int counter)
            {
                Stone = stone;
                Counter = counter;
            }

            #endregion Public Constructors

            #region Public Properties

            public int Counter { get; set; }
            public ulong Stone { get; set; }

            #endregion Public Properties

            #region Public Methods

            public bool Equals(StoneInfo other)
            {
                return Stone == other.Stone && Counter == other.Counter;
            }

            public override bool Equals(object? obj)
            {
                return obj is StoneInfo other && Equals(other);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Stone, Counter);
            }

            #endregion Public Methods
        }

        #endregion Private Structs
    }
}