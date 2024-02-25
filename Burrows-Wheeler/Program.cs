const int alphabetSize = 256;

(string, int) Transform(string originalString)
{
    if (originalString == "")
    {
        return ("", 0);
    }

    int[] suffixArray = Enumerable.Range(0, originalString.Length).ToArray();

    Array.Sort(suffixArray, (i, j) => string.Compare($"{originalString.Substring(i)}{originalString.Remove(i)}", $"{originalString.Substring(j)}{originalString.Remove(j)}"));

    char[] resultArray = new char[originalString.Length];
    for (int i = 0; i < originalString.Length; ++i)
    {
        resultArray[i] = suffixArray[i] > 0 ? originalString[suffixArray[i] - 1] : originalString[originalString.Length - 1];
    }

    int endPosition = Array.IndexOf(suffixArray, 0);

    return (new string(resultArray), endPosition);
}

string InverseTransform(string bwtString, int endPosition)
{
    if (bwtString == "")
    {
        return "";
    }

    int[] count = new int[alphabetSize];
    for (int i = 0; i < bwtString.Length; ++i)
    {
        ++count[bwtString[i]];
    }

    int sum = 0;
    for (int i = 0; i < alphabetSize; ++i)
    {
        sum += count[i];
        count[i] = sum - count[i];
    }

    int[] positions = new int[bwtString.Length];
    for (int i = 0; i < bwtString.Length; ++i)
    {
        positions[count[bwtString[i]]] = i;
        ++count[bwtString[i]];
    }

    char[] resultArray = new char[bwtString.Length];
    int index = positions[endPosition];
    for (int i = 0; i < bwtString.Length; ++i)
    {
        resultArray[i] = bwtString[index];
        index = positions[index];
    }

    return new string(resultArray);
}

bool RunTestsForTransform()
{
    return Transform("ABACABA") == ("BCABAAA", 2) && Transform("banana") == ("nnbaaa", 3)
        && Transform("a") == ("a", 0) && Transform("") == ("", 0);
}

bool RunTestsForInverseTransform()
{
    return InverseTransform("BCABAAA", 2) == "ABACABA" && InverseTransform("nnbaaa", 3) == "banana"
        && InverseTransform("a", 0) == "a" && InverseTransform("", 0) == "";
}

bool RunTests()
{
    return RunTestsForTransform() && RunTestsForInverseTransform();
}

if (!RunTests())
{
    Environment.Exit(1);
}

Console.Write("Enter text for Burrows-Wheeler transformation: ");
string? text = Console.ReadLine();
if (text == null)
{
    Console.WriteLine("null error");
    Environment.Exit(1);
}

var (bwtString, endPosition) = Transform(text);
string inverseString = InverseTransform(bwtString, endPosition);
if (text != inverseString)
{
    Console.WriteLine("Algorithm works incorrectly");
}

Console.WriteLine($"Transformed string: {bwtString}");
Console.WriteLine($"Inverse transform string: {text}");
