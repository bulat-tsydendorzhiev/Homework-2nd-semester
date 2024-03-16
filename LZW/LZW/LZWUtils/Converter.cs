namespace LZW;

/// <summary>
/// Class which has methods of converting number from and to binary representation.
/// </summary>
public static class Converter
{
    /// <summary>
    /// Converts binary representation of number to integer one.
    /// </summary>
    /// <param name="binary">Binary representation.</param>
    /// <returns>Integer value of binary representation.</returns>
    public static int ConvertBitsToInt(List<byte> binary)
    {
        int result = 0;

        foreach (var bit in binary)
        {
            result = (result << 1) + bit;
        }

        return result;
    }

    /// <summary>
    /// Converts number to its binary representation.
    /// </summary>
    /// <param name="value">Converting value.</param>
    /// <param name="byteSize">Number of digits in binary representation of value.</param>
    /// <returns>Binary representation of value.</returns>
    public static List<byte> ConvertIntToBits(int value, int byteSize)
    {
        var binary = new List<byte>();
        while (value > 0)
        {
            binary.Add((byte)(value % 2));
            value /= 2;
        }

        while (binary.Count < byteSize)
        {
            binary.Add(0);
        }

        binary.Reverse();

        return binary;
    }
}