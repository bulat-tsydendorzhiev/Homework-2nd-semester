namespace LZW;

/// <summary>
/// Class <c>Burrows-Wheeler Transform</c> which has methods of direct and inverse transform using Burrows-Wheeler algorithm.
/// </summary>
public static class BWT
{
    private const int AlphabetSize = 256;

    /// <summary>
    /// Transforms byte array using the Burrows-Wheeler algorithm for further better compression.
    /// </summary>
    /// <param name="input">Byte array which will be transformed.</param>
    /// <returns>
    /// Transformed byte array by Burrows-Wheeler algorithm;
    /// Index of position the original array begins with in table of shifts.
    /// </returns>
    public static (byte[], int) Transform(byte[] input)
    {
        if (input.Length == 0)
        {
            return ([], 0);
        }

        int[] suffixArray = Enumerable.Range(0, input.Length).ToArray();
        Array.Sort(suffixArray, (i, j) => Compare(input, i, j));

        var output = new byte[input.Length];
        for (int i = 0; i < input.Length; ++i)
        {
            output[i] = suffixArray[i] > 0 ? input[suffixArray[i] - 1] : input[^1];
        }

        int endPosition = Array.IndexOf(suffixArray, 0);

        return (output, endPosition);
    }

    /// <summary>
    /// Makes inverse transform of Burrows-Wheeler byte array to original array.
    /// </summary>
    /// <param name="input">Transformed byte array by Burrows-Wheeler algorithm.</param>
    /// <param name="endPosition">Index of position the original array begins with in table of shifts.</param>
    /// <returns>Original byte array.</returns>
    public static byte[] InverseTransform(byte[] input, int endPosition)
    {
        if (input.Length == 0)
        {
            return [];
        }

        var count = new int[AlphabetSize];

        for (int i = 0; i < input.Length; ++i)
        {
            ++count[input[i]];
        }

        var sum = 0;
        for (int i = 0; i < AlphabetSize; ++i)
        {
            sum += count[i];
            count[i] = sum - count[i];
        }

        var positions = new int[input.Length];
        for (int i = 0; i < input.Length; ++i)
        {
            positions[count[input[i]]] = i;
            ++count[input[i]];
        }

        var output = new byte[input.Length];
        int index = positions[endPosition];
        for (int i = 0; i < input.Length; ++i)
        {
            output[i] = input[index];
            index = positions[index];
        }

        return output;
    }

    private static int Compare(byte[] input, int firstIndex, int secondIndex)
    {
        for (int i = 0; i < input.Length; ++i)
        {
            if (input[(i + firstIndex) % input.Length] < input[(i + secondIndex) % input.Length])
            {
                return -1;
            }
            if (input[(i + firstIndex) % input.Length] > input[(i + secondIndex) % input.Length])
            {
                return 1;
            }
        }
        
        return 0;
    }
}