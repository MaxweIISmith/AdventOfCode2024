namespace AdventOfCode2024;

public static class Problem09Part2
{
    public static void Solve()
    {
        var reader = File.OpenText("input.txt");

        var diskMap = reader.ReadLine().Select(c => c - '0').ToList();


        var fileInfos = new List<FileInfo>();

        for (int i = 0; i < diskMap.Count; i++)
        {
            var fileSize = diskMap[i];

            fileInfos.Add(new FileInfo()
            {
                FileId = i % 2 == 0 ? i / 2 : -1,
                FileSize = fileSize,
            });
        }

        for (int k = fileInfos.Count - 1; k >= 0; k--)
        {
            var fileInfo = fileInfos[k];

            if (fileInfo.FileId == -1 || fileInfo.IsProcessed)
                continue;

            var fileSize = fileInfos[k].FileSize;

            for (int i = 0; i < k; i++)
            {
                if (fileInfos[i].FileId != -1)
                    continue;

                var emptyFile = fileInfos[i];

                if (fileSize <= emptyFile.FileSize)
                {
                    var prevEmptyFile = fileInfos[k - 1];
                    prevEmptyFile.FileSize += fileSize;
                    emptyFile.FileSize -= fileSize;

                    fileInfos.Remove(fileInfo);
                    fileInfos.Insert(i, fileInfo);

                    break;
                }
            }

            fileInfo.IsProcessed = true;
        }

        ulong sum = 0;
        ulong sectorId = 0;
        for (int k = 0; k < fileInfos.Count; k++)
        {
            var fileInfo = fileInfos[k];

            var fileId = (ulong)(fileInfo.FileId == -1 ? 0 : fileInfo.FileId);

            for (int i = 0; i < fileInfo.FileSize; i++)
            {
                sum += sectorId++ * fileId;
            }
        }

        Console.WriteLine(sum);
    }

    private class FileInfo
    {
        public int FileSize { get; set; }

        public int FileId { get; set; }

        public bool IsProcessed { get; set; }

        public override string ToString()
        {
            return $"ID={FileId}, Size={FileSize}";
        }
    }
}