namespace LZW;

/// <summary>
/// Buffer for LZW compression.
/// </summary>
public class CompressionBuffer
{
    /// <summary>
    /// Compressed bytes.
    /// </summary>
    public List<byte> CompressedBytes { get; private set; } = new();

    /// <summary>
    /// Number of bits for coding one byte.
    /// </summary>
    public int BitsInCurrentByte { get; set; } = ByteSizeInBits;

    private List<byte> CurrentBits = new();

    private const int ByteSizeInBits = 8;

    /// <summary>
    /// Adds new byte to compressed bytes.
    /// </summary>
    /// <param name="value">Adding value.</param>
    public void AddNewByte(int value)
    {
        var binaryValue = Converter.ConvertIntToBits(value, BitsInCurrentByte);

        while (binaryValue.Count + CurrentBits.Count >= ByteSizeInBits)
        {
            while (CurrentBits.Count < ByteSizeInBits)
            {
                CurrentBits.Add(binaryValue[0]);
                binaryValue.RemoveAt(0);
            }

            CompressedBytes.Add((byte)Converter.ConvertBitsToInt(CurrentBits));
            CurrentBits.Clear();
        }

        CurrentBits = binaryValue;
    }

    /// <summary>
    /// Adds last byte.
    /// </summary>
    public void AddLastByte()
    {
        if (CurrentBits.Count != 0 && CurrentBits.Any((bit) => bit != 0))
        {
            while (CurrentBits.Count < ByteSizeInBits)
            {
                CurrentBits.Add(0);
            }

            CompressedBytes.Add((byte)Converter.ConvertBitsToInt(CurrentBits));
        }
    }
}