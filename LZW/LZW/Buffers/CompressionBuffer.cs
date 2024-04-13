namespace LZW;

/// <summary>
/// Buffer for LZW compression.
/// </summary>
public class CompressionBuffer
{
    /// <summary>
    /// Compressed bytes.
    /// </summary>
    public List<byte> CompressedBytes { get; private set; } = [];

    /// <summary>
    /// Number of bits for coding one byte.
    /// </summary>
    public int BitsInCurrentByte { get; set; } = ByteSizeInBits;

    private List<bool> _currentBits = [];

    private const int ByteSizeInBits = 8;

    /// <summary>
    /// Adds new byte to compressed bytes.
    /// </summary>
    /// <param name="value">Adding value.</param>
    public void AddNewByte(int value)
    {
        var binaryValue = Converter.ConvertIntToBits(value, BitsInCurrentByte);

        while (binaryValue.Count + _currentBits.Count >= ByteSizeInBits)
        {
            while (_currentBits.Count < ByteSizeInBits)
            {
                _currentBits.Add(binaryValue[0]);
                binaryValue.RemoveAt(0);
            }

            CompressedBytes.Add((byte)Converter.ConvertBitsToInt(_currentBits));
            _currentBits.Clear();
        }

        _currentBits = binaryValue;
    }

    /// <summary>
    /// Adds last byte to compressed bytes.
    /// </summary>
    public void AddLastByte()
    {
        if (_currentBits.Count != 0 && _currentBits.Any((bit) => bit))
        {
            while (_currentBits.Count < ByteSizeInBits)
            {
                _currentBits.Add(false);
            }

            CompressedBytes.Add((byte)Converter.ConvertBitsToInt(_currentBits));
        }
    }
}