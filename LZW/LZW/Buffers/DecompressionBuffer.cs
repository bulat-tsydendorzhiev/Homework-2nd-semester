namespace LZW;

/// <summary>
/// Buffer for LZW decompression.
/// </summary>
public class DecompressorBuffer
{
    /// <summary>
    /// Number of bits for coding one byte.
    /// </summary>
    public int BitsInCurrentByte { get; set; } = ByteSizeInBits;

    private List<byte> CurrentByte = new();

    private List<byte> RemainedBits = new();

    private const int ByteSizeInBits = 8;

    /// <summary>
    /// Tries to collect whole byte.
    /// </summary>
    /// <param name="valueOfByte">Value of byte.</param>
    /// <returns>true if the whole byte was collected; otherwise false.</returns>
    public bool CollectWholeByte(byte valueOfByte)
    {
        var binary = Converter.ConvertIntToBits(valueOfByte, ByteSizeInBits);

        while (binary.Count > 0)
        {
            RemainedBits.Add(binary[0]);
            binary.RemoveAt(0);
        }

        if (RemainedBits.Count < BitsInCurrentByte)
        {
            return false;
        }

        while (CurrentByte.Count < BitsInCurrentByte)
        {
            CurrentByte.Add(RemainedBits[0]);
            RemainedBits.RemoveAt(0);
        }

        return true;
    }

    /// <summary>
    /// Gets positive integer code of current byte. 
    /// </summary>
    /// <returns>Value of current byte.</returns>
    public int GetValueOfCurrentByte()
    {
        int value = Converter.ConvertBitsToInt(CurrentByte);
        CurrentByte.Clear();
        return value;
    }
}