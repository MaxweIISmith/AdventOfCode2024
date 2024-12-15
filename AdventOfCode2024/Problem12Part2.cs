using AdventOfCode2024.Helpers;

namespace AdventOfCode2024
{
    public class Problem12Part2
    {
        #region Public Methods

        public static void Solve()
        {
            var plantMap = new PlantMap(File.ReadAllLines("input.txt"));

            var flags = new bool[plantMap.MaxX, plantMap.MaxY];

            int result = 0;

            for (int x = 0; x < plantMap.MaxX; x++)
            {
                for (int y = 0; y < plantMap.MaxY; y++)
                {
                    if (flags[x, y])
                        continue;

                    var regionCoordinates = new List<Vec2i>();

                    var plantType = plantMap[x, y];

                    FindPlantCoords(plantMap, plantType, x, y, regionCoordinates);

                    foreach (var coord in regionCoordinates)
                    {
                        flags[coord.X, coord.Y] = true;
                    }

                    var sides = CalcSides(regionCoordinates);

                    var area = regionCoordinates.Count;

                    result += sides * area;
                }
            }

            Console.WriteLine(result);
        }

        #endregion Public Methods

        #region Private Methods

        private static int CalcSides(List<Vec2i> coords)
        {
            var minX = coords.Min(v => v.X);
            var maxX = coords.Max(v => v.X);
            var minY = coords.Min(v => v.Y);
            var maxY = coords.Max(v => v.Y);

            var sizeX = maxX - minX + 3;
            var sizeY = maxY - minY + 3;

            var field = new bool[sizeX, sizeY];

            foreach (var c in coords)
            {
                var x = c.X - minX + 1;
                var y = c.Y - minY + 1;

                field[x, y] = true;
            }

            int sideCount = 0;

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    var length = 0;

                    while (field[x, y + length] && !field[x - 1, y + length])
                    {
                        length++;
                    }

                    if (length > 0)
                    {
                        sideCount++;
                        y += length - 1;
                    }

                }
            }

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    var length = 0;

                    while (field[x, y + length] && !field[x + 1, y + length])
                    {
                        length++;
                    }

                    if (length > 0)
                    {
                        sideCount++;
                        y += length - 1;
                    }

                }
            }

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    var length = 0;

                    while (field[x + length, y] && !field[x + length, y - 1])
                    {
                        length++;
                    }

                    if (length > 0)
                    {
                        sideCount++;
                        x += length - 1;
                    }
                }
            }

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    var length = 0;

                    while (field[x + length, y] && !field[x + length, y + 1])
                    {
                        length++;
                    }

                    if (length > 0)
                    {
                        sideCount++;
                        x += length - 1;
                    }
                }
            }

            return sideCount;
        }

        private static void FindPlantCoords(PlantMap plantMap, char plantType, int x, int y, List<Vec2i> coordsList)
        {
            if (x < 0 || x >= plantMap.MaxX)
                return;

            if (y < 0 || y >= plantMap.MaxY)
                return;

            if (plantMap[x, y] != plantType)
                return;

            var coords = new Vec2i(x, y);

            if (coordsList.Contains(coords))
                return;

            coordsList.Add(new Vec2i(x, y));

            FindPlantCoords(plantMap, plantType, x - 1, y, coordsList);
            FindPlantCoords(plantMap, plantType, x + 1, y, coordsList);
            FindPlantCoords(plantMap, plantType, x, y - 1, coordsList);
            FindPlantCoords(plantMap, plantType, x, y + 1, coordsList);
        }

        #endregion Private Methods

        #region Private Classes

        private class PlantMap
        {
            #region Private Fields

            private List<string> _map;

            #endregion Private Fields

            #region Public Constructors

            public PlantMap(IEnumerable<string> mapLines)
            {
                _map = new List<string>(mapLines);
            }

            #endregion Public Constructors

            #region Public Properties

            public int MaxX
            {
                get { return _map[0].Length; }
            }

            public int MaxY
            {
                get { return _map.Count; }
            }

            #endregion Public Properties

            #region Public Indexers

            public char this[int x, int y]
            {
                get { return _map[y][x]; }
            }

            #endregion Public Indexers
        }

        #endregion Private Classes
    }
}