namespace AdventOfCode2024
{
    public static class Problem19Part1
    {
        public static void Solve()
        {
            var reader = File.OpenText("input.txt");

            var parts = reader.ReadLine().Split(",").Select(s => s.Trim()).ToList();

            var parts2 = new List<string>();

            foreach (var part in parts)
            {
                var tempParts = parts.ToList();
                tempParts.Remove(part);

                if (!CanCombineStringFromParts(part, tempParts))
                {
                    parts2.Add(part);
                }
            }

            int count = 0;
            while (!reader.EndOfStream)
            {
                var str = reader.ReadLine();

                if (String.IsNullOrEmpty(str))
                    continue;

                if (CanCombineStringFromParts(str, parts2))
                {
                    count++;
                }
            }

            Console.WriteLine(count);
        }

        private static bool CanCombineStringFromParts(string str, List<string> parts, int index = 0)
        {
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
                    return true;

                if (CanCombineStringFromParts(str, parts, newIndex))
                {
                    return true;
                }
            }

            return false;
        }
    }
}