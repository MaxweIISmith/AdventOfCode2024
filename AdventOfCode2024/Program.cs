//https://adventofcode.com/2024/

namespace Advent2024
{
    internal static class Program
    {
        #region Private Methods

        private static void Main()
        {
            var reader = File.OpenText("input.txt");

            var rules = new Dictionary<int, HashSet<int>>();

            var sum = 0;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (String.IsNullOrEmpty(line))
                    continue;

                if (line.Contains('|'))
                {
                    var parts = line.Split('|');
                    var page1 = Int32.Parse(parts[0]);
                    var page2 = Int32.Parse(parts[1]);

                    if (!rules.TryGetValue(page2, out var set))
                    {
                        set = new HashSet<int>();
                        rules[page2] = set;
                    }

                    set.Add(page1);
                }
                else
                {
                    var pages = line.Split(',').Select(Int32.Parse).ToList();

                    bool isOk = true;

                    for (int i = 0; i < pages.Count - 1 && isOk; i++)
                    {
                        if (rules.TryGetValue(pages[i], out var set))
                        {
                            for (int j = i + 1; j < pages.Count; j++)
                            {
                                if (set.Contains(pages[j]))
                                {
                                    Console.WriteLine($"{i} - {j}");
                                    isOk = false;
                                }
                            }
                        }
                    }

                    if (isOk)
                    {
                        var middleIndex = pages.Count / 2;

                        sum += pages[middleIndex];
                    }
                    else
                    {
                        Console.WriteLine(line);
                    }
                }
            }

            Console.WriteLine(sum);

#if DEBUG
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
#endif
        }

        #endregion Private Methods
    }
}