namespace LZW;

/// <summary>
/// Class which has method of compressing byte array.
/// </summary>
public static class LZWCompressor
{
    private const int InitialTableSize = 256;

    /// <summary>
    /// Compresses byte array using LZW encoding algorithm.
    /// </summary>
    /// <param name="input">Byte array.</param>
    /// <returns>Compressed byte array.</returns>
    /// <exception cref="ArgumentException">Input cannot not empty.</exception>
    public static byte[] Compress(byte[] input)
    {
        if (input.Length == 0)
        {
            throw new ArgumentException("input cannot be empty");
        }

        CompressionBuffer buffer = new();

        var table = InitEncodingTable();
        List<byte> element = new();
        int currentMaxSize = InitialTableSize;

        for (int i = 0; i < input.Length; ++i)
        {
            List<byte> newElement = [.. element, input[i]];

            if (table.Contains(newElement))
            {
                element = newElement;
            }
            else
            {
                var value = table.GetValue(element);

                buffer.AddNewByte(value);

                if (table.Size == currentMaxSize)
                {
                    currentMaxSize *= 2;
                    ++buffer.BitsInCurrentByte;
                }

                table.Add(newElement, table.Size);

                element.Clear();
                element.Add(input[i]);
            }
        }
        var lastValue = table.GetValue(element);

        buffer.AddNewByte(lastValue);
        buffer.AddLastByte();

        return buffer.CompressedBytes.ToArray();
    }

    private static Trie InitEncodingTable()
    {
        var table = new Trie();
        for (int i = 0; i < InitialTableSize; ++i)
        {
            table.Add(new List<byte> { (byte)i }, i);
        }

        return table;
    }
}