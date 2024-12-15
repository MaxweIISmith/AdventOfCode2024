namespace AdventOfCode2024;

public static class Problem09Part1
{
    public static void Solve()
    {
        var reader = File.OpenText("input.txt");

        var diskMap = reader.ReadLine();

        if (String.IsNullOrEmpty(diskMap))
            throw new InvalidOperationException();

        Func<int, int> getFileSize = index => Int32.Parse(diskMap.Substring(index, 1));

        int leftFileIndex = 0;
        int rightFileIndex = diskMap.Length - 1;
        if (rightFileIndex % 2 == 1)
            rightFileIndex--;

        var rightFileSize = getFileSize(rightFileIndex);

        ulong sum = 0;

        int sectorId = 0;

        while (true)
        {
            var leftFileSize = getFileSize(leftFileIndex);

            for (int k = 0; k < leftFileSize; k++)
            {
                sum += (ulong)(sectorId++ * (leftFileIndex / 2));
            }

            leftFileIndex++;
            if (leftFileIndex >= rightFileIndex)
                break;

            leftFileSize = getFileSize(leftFileIndex);

            for (int k = 0; k < leftFileSize; k++)
            {
                if (rightFileSize == 0)
                {
                    rightFileIndex -= 2;
                    if (leftFileIndex >= rightFileIndex)
                        break;

                    rightFileSize = getFileSize(rightFileIndex);
                }

                sum += (ulong)(sectorId++ * (rightFileIndex / 2));

                rightFileSize--;
            }

            leftFileIndex++;
            if (leftFileIndex >= rightFileIndex)
                break;
        }

        for (int k = 0; k < rightFileSize; k++)
        {
            sum += (ulong)(sectorId * (rightFileIndex / 2));
            sectorId++;
        }

        Console.WriteLine(sum);
    }
}