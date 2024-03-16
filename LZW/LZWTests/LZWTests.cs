using LZWTransform;

namespace LZWTests;

public class LZWTests
{

    [TestCase("..\\..\\..\\TestFiles\\MasterAndMargarita.txt")]
    [TestCase("..\\..\\..\\TestFiles\\видео с котиками.mp4")]
    [TestCase("..\\..\\..\\TestFiles\\Rick visiting piss master.png")]
    public void CompressingAndDecompressingWithoutBWTShouldNotChangeTheFile(string filePath)
    {
        var original = File.ReadAllBytes(filePath);

        LZWTransform.LZWTransform.Compress(filePath);
        LZWTransform.LZWTransform.Decompress(filePath + ".zipped");

        var transformed = File.ReadAllBytes(filePath);

        Assert.That(Compare(original, transformed), Is.True);
    }

    [TestCase("..\\..\\..\\TestFiles\\MasterAndMargarita.txt")]
    [TestCase("..\\..\\..\\TestFiles\\видео с котиками.mp4")]
    [TestCase("..\\..\\..\\TestFiles\\Rick visiting piss master.png")]
    public void CompressingAndDecompressingWithBWTShouldNotChangeTheFile(string filePath)
    {
        var original = File.ReadAllBytes(filePath);

        LZWTransform.LZWTransform.Compress(filePath, true);
        LZWTransform.LZWTransform.Decompress(filePath + ".zipped", true);

        var transformed = File.ReadAllBytes(filePath);

        Assert.That(Compare(original, transformed), Is.True);
    }

    [TestCase("..\\..\\..\\TestFiles\\Empty.cs")]
    public void Compressing_EmptyFileWithoutBWTShouldReturnMinusOne(string filePath)
    {
        Assert.That(Math.Abs(-1 - LZWTransform.LZWTransform.Compress(filePath)), Is.LessThan(0.0000001));
    }

    [TestCase("..\\..\\..\\TestFiles\\Empty.cs")]
    public void Decompressing_EmptyFileWithoutBWTShouldReturnFalse(string filePath)
    {
        Assert.That(LZWTransform.LZWTransform.Decompress(filePath), Is.False);
    }

    [TestCase("..\\..\\..\\TestFiles\\Empty.cs")]
    public void Compressing_EmptyFileWithBWTShouldReturnMinusOne(string filePath)
    {
        Assert.That(Math.Abs(-1 - LZWTransform.LZWTransform.Compress(filePath, true)), Is.LessThan(0.0000001));
    }

    [TestCase("..\\..\\..\\TestFiles\\Empty.cs")]
    public void Decompressing_EmptyFileWithBWTShouldReturnFalse(string filePath)
    {
        Assert.That(LZWTransform.LZWTransform.Decompress(filePath, true), Is.False);
    }

    [TestCase("..\\..\\..\\TestFiles\\Empty.txt")]
    public void Compressing_NonExistingFileWithoutBWTShouldReturnMinusOne(string filePath)
    {
        Assert.That(Math.Abs(-1 - LZWTransform.LZWTransform.Compress(filePath)), Is.LessThan(0.0000001));
    }

    [TestCase("..\\..\\..\\TestFiles\\Empty.txt")]
    public void Decompressing_NonExistinFileWithoutBWTShouldReturnFalse(string filePath)
    {
        Assert.That(LZWTransform.LZWTransform.Decompress(filePath), Is.False);
    }


    [TestCase("..\\..\\..\\TestFiles\\Empty.txt")]
    public void Compressing_NonExistingFileWithBWTShouldReturnMinusOne(string filePath)
    {
        Assert.That(Math.Abs(-1 - LZWTransform.LZWTransform.Compress(filePath, true)), Is.LessThan(0.0000001));
    }

    [TestCase("..\\..\\..\\TestFiles\\Empty.txt")]
    public void Decompressing_NonExistinFileWithBWTShouldReturnFalse(string filePath)
    {
        Assert.That(LZWTransform.LZWTransform.Decompress(filePath, true), Is.False);
    }

    private static bool Compare(byte[] input, byte[] output)
    {
        if (input.Length != output.Length)
        {
            return false;
        }

        for (int i = 0; i < input.Length; ++i)
        {
            if (input[i] != output[i])
            {
                return false;
            }
        }

        return true;
    }
}