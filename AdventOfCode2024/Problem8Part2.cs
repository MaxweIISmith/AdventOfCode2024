using System.Text;
using AdventOfCode2024.Helpers;

namespace AdventOfCode2024;

public static class Problem8Part2
{
    public static void Solve()
    {
        var reader = File.OpenText("input.txt");

        var map = new List<StringBuilder>();
        var dict = new Dictionary<char, HashSet<Vec2i>>();

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            if (string.IsNullOrEmpty(line))
                continue;

            map.Add(new StringBuilder(line));

            for (int i = 0; i < line.Length; i++)
            {
                var ch = line[i];

                if (ch != '.')
                {
                    var x = i;
                    var y = map.Count - 1;

                    if (!dict.TryGetValue(ch, out var locations))
                    {
                        locations = new HashSet<Vec2i>();
                        dict[ch] = locations;
                    }

                    locations.Add(new Vec2i(x, y));
                }
            }
        }

        var antidoteLocations = new HashSet<Vec2i>();

        var maxX = map[0].Length;
        var maxY = map.Count;

        Func<Vec2i, bool> isOkAntinodeLocation =
            vec => vec.X >= 0 && vec.X < maxX &&
                   vec.Y >= 0 && vec.Y < maxY;

        foreach (var antenna in dict.Keys)
        {
            var locations = dict[antenna];

            if (locations.Count < 2)
                continue;

            foreach (var antenna1 in locations)
            {
                foreach (var antenna2 in locations)
                {
                    if (antenna1 == antenna2)
                        continue;

                    var diff = antenna1 - antenna2;

                    var antinode = antenna1 + diff;
                    while (true)
                    {
                        if (!isOkAntinodeLocation(antinode))
                            break;

                        antidoteLocations.Add(antinode);
                        antinode += diff;
                    }

                    antinode = antenna1 - diff;
                    while (true)
                    {
                        if (!isOkAntinodeLocation(antinode))
                            break;

                        antidoteLocations.Add(antinode);
                        antinode -= diff;
                    }
                }
            }
        }

        Console.WriteLine(antidoteLocations.Count);
    }
}