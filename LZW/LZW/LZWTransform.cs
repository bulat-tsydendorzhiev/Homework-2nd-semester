using LZW;

namespace LZWTask;

/// <summary>
/// Class which has methods of compressing, decompressing files and writting them to a new path.
/// </summary>
public static class LZWTransform
{
    /// <summary>
    /// Compresses file using LZW decoding algorithm considering the need to use Burrows-Wheeler transform;
    /// Writes it to a new path with file extension ".zipped".
    /// </summary>
    /// <param name="filePath">File path.</param>
    /// <param name="shouldUseBWT">Shows the need to use Burrows-Wheeler transform.</param>
    /// <returns>Ratio of compressing.</returns>
    public static double Compress(string filePath, bool shouldUseBWT = false)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("There is no such file!");
            return -1;
        }

        var input = File.ReadAllBytes(filePath);

        if (shouldUseBWT)
        {
            var (bwtResult, endPosition) = BWT.Transform(input);

            var endPositionBytes = BitConverter.GetBytes(endPosition);

            input = [.. endPositionBytes, .. bwtResult];
        }

        if (input.All((bit) => bit == 0))
        {
            Console.WriteLine("File cannot be empty!");
            return -1;
        }

        byte[] compressed;
        
        try
        {
            compressed = LZWCompressor.Compress(input);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            return -1;
        }

        string newFilePath = filePath + ".zipped";

        File.WriteAllBytes(newFilePath, compressed);

        var originalFileSize = new FileInfo(filePath).Length;
        var compressedFileSize = new FileInfo(newFilePath).Length;

        return (double)originalFileSize / compressedFileSize;
    }

    /// <summary>
    /// Decompresses file using LZW decoding algorithm considering the need to use Burrows-Wheeler transform;
    /// Writes it to a new path.
    /// </summary>
    /// <param name="filePath">File path.</param>
    /// <param name="shouldUseBWT">Shows the need to use Burrows-Wheeler transform.</param>
    /// <returns>true if invalid file path was given; otherwise false.</returns> 
    public static bool Decompress(string filePath, bool shouldUseBWT = false)
    {
        if (!File.Exists(filePath) || filePath[^7..] != ".zipped")
        {
            Console.WriteLine("There is no such file!");
            return false;
        }

        var input = File.ReadAllBytes(filePath);

        byte[] decompressed;
        
        try
        {
            decompressed = LZWDecompressor.Decompress(input);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

        string newFilePath = filePath[..^7];

        File.WriteAllBytes(newFilePath, shouldUseBWT
                                        ? BWT.InverseTransform(decompressed[4..], BitConverter.ToInt32(decompressed, 0))
                                        : decompressed);
        return true;
    }
}