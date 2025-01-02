namespace AdventOfCode2024
{
    public static class Problem19Part2
    {
        #region Private Fields

        private static readonly Dictionary<string, ulong> History = new Dictionary<string, ulong>();

        #endregion Private Fields

        #region Public Methods

        public static void Solve()
        {
            var reader = File.OpenText("input.txt");

            var parts = reader.ReadLine().Split(",").Select(s => s.Trim()).ToList();

            ulong count = 0;
            while (!reader.EndOfStream)
            {
                var str = reader.ReadLine();

                if (String.IsNullOrEmpty(str))
                    continue;

                count += CanCombineStringFromParts(str, parts);
            }

            Console.WriteLine(count);
        }

        #endregion Public Methods

        #region Private Methods

        private static ulong CanCombineStringFromParts(string str, List<string> parts, int index = 0)
        {
            if (History.TryGetValue(str.Substring(index), out var value))
            {
                return value;
            }

            ulong count = 0;

            foreach (var part in parts)
            {
                if (str.Length - index < part.Length)
                    continue;

                bool isEqual = true;
                for (int i = 0; i < part.Length; i++)
                {
                    if (str[index + i] != part[i])
                    {
                        isEqual = false;
                        break;
                    }
                }

                if (!isEqual)
                    continue;

                var newIndex = index + part.Length;

                if (newIndex == str.Length)
                {
                    count++;
                    continue;
                }

                count += CanCombineStringFromParts(str, parts, newIndex);
            }

            History[str.Substring(index)] = count;

            return count;
        }

        #endregion Private Methods
    }
}