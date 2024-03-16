namespace LZW;

/// <summary>
/// Class which has method of decompressing byte array compressed by LZW algorithm.
/// </summary>
public static class LZWDecompressor
{
    private const int InitialTableSize = 256;

    /// <summary>
    /// Decompresses byte array using LZW decoding algorithm.
    /// </summary>
    /// <param name="input">Byte array.</param>
    /// <returns>Decompressed byte array.</returns>
    /// <exception cref="ArgumentException">Input cannot be empty.</exception>
    public static byte[] Decompress(byte[] input)
    {
        if (input.Length == 0)
        {
            throw new ArgumentException("Input cannot be empty");
        }

        DecompressorBuffer buffer = new();

        var table = InitDecodingTable();
        var fullByte = new List<byte>();
        List<byte> previous = [.. table[input[0]]];
        List<byte> output = [.. previous];

        var currentMaxSize = InitialTableSize;

        for (int i = 1; i < input.Length; ++i)
        {
            if (table.Count == currentMaxSize)
            {
                currentMaxSize *= 2;

                ++buffer.BitsInCurrentByte;
            }


            if (!buffer.CollectWholeByte(input[i]))
            {
                continue;
            }

            int code = buffer.GetValueOfCurrentByte();

            List<byte> entry = table.ContainsKey(code) ? table[code] : [.. previous, previous[0]];

            output.AddRange(entry);

            table.Add(table.Count, [.. previous, entry[0]]);

            previous = entry;
        }

        return output.ToArray();
    }

    private static Dictionary<int, List<byte>> InitDecodingTable()
    {
        var table = new Dictionary<int, List<byte>>();
        for (int i = 0; i < InitialTableSize; ++i)
        {
            table.Add(i, new List<byte>() { (byte)i });
        }

        return table;
    }
}