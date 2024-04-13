using LZWTask;

namespace LZWTests;

public class LZWTests
{
    public const double Epsilon = 0.0000001;

    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.TestCasesWithValidFilePaths))]
    public void CompressingAndDecompressingOf_ValidFiles_WithoutBWT_ShouldNotChangeTheFile(string filePath)
    {
        var original = File.ReadAllBytes(filePath);

        LZWTransform.Compress(filePath);
        LZWTransform.Decompress(filePath + ".zipped");

        var transformed = File.ReadAllBytes(filePath);

        Assert.That(Compare(original, transformed), Is.True);
    }

    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.TestCasesWithValidFilePaths))]
    public void CompressingAndDecompressingOf_ValidFiles_WithBWT_ShouldNotChangeTheFile(string filePath)
    {
        var original = File.ReadAllBytes(filePath);

        LZWTransform.Compress(filePath, true);
        LZWTransform.Decompress(filePath + ".zipped", true);

        var transformed = File.ReadAllBytes(filePath);

        Assert.That(Compare(original, transformed), Is.True);
    }

    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.TestCasesWithInvalidFilePaths))]
    public void CompressingAndDecompressingOf_InvalidFiles_WithoutBWT_ShouldReturn_CorrespondingValuesOfEachMethod(string filePath)
    {
        Assert.Multiple(() =>
        {
            Assert.That(Math.Abs(-1 - LZWTransform.Compress(filePath)), Is.LessThan(Epsilon));
            Assert.That(LZWTransform.Decompress(filePath), Is.False);
        });
    }

    [TestCaseSource(typeof(TestDataClass), nameof(TestDataClass.TestCasesWithInvalidFilePaths))]
    public void CompressingAndDecompressingOf_InvalidFiles_WithBWT_ShouldReturn_CorrespondingValuesOfEachMethod(string filePath)
    {
        Assert.Multiple(() =>
        {
            Assert.That(Math.Abs(-1 - LZWTransform.Compress(filePath, true)), Is.LessThan(Epsilon));
            Assert.That(LZWTransform.Decompress(filePath, true), Is.False);
        });
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
    
    private class TestDataClass
    {
        public static object[] TestCasesWithValidFilePaths =
        {
            "../../../TestFiles/MasterAndMargarita.txt",
            "../../../TestFiles/VideoWithCats.mp4",
            "../../../TestFiles/RickVisitsPissMaster.png"
        };
        
        public static object[] TestCasesWithInvalidFilePaths =
        {
            "../../../TestFiles/Empty.txt",
            "../../../TestFiles/Empty.cs",
        };
    }
}