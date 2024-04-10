using Routers;

namespace RoutersTests;

public class Tests
{
    [TestCase("../../../TestFiles/DisconnectedNetworkCase.txt")]
    public void DisconnectedNetworkException_ShouldBeThrownWith_DisconnectedNetworkCase(string filePath)
    {
        var network = new Network(filePath);
        Assert.Throws<DisconnectedNetworkException>(() => network.WriteConfigurationToFile(""));
    }

    [TestCase("../../../TestFiles/EmptyFileCase.txt")]
    [TestCase("../../../TestFiles/NetworkWithOneRouterCase.txt")]
    [TestCase("../../../TestFiles/NetworkWithNegativeBandwidthCase.txt")]
    public void IncorrectFormatOfFileException_ShouldBeThrownWith_FileWithIncorrectFormat(string filePath)
    {
        Assert.Throws<IncorrectFormatOfFileException>(() => new Network(filePath));
    }

    [TestCase("DefaultCorrectCase.txt")]
    [TestCase("ValidCase.txt")]
    public void ValidNetworksShouldGiveRightAnswer(string fileName)
    {
        var network = new Network($"../../../TestFiles/{fileName}");
        network.WriteConfigurationToFile($"../../../TestFiles/ValidTestCasesOutput/{fileName[..^4]}Output.txt");

        var result = File.ReadAllLines($"../../../TestFiles/ValidTestCasesOutput/{fileName[..^4]}Output.txt");
        var expectedResult = File.ReadAllLines($"../../../TestFiles/ValidTestCasesOutput/{fileName[..^4]}ExpectedOutput.txt");
        
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}
