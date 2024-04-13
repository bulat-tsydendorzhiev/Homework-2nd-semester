using BurrowsWheeler;

bool RunTestsForTransform()
{
    return BWT.Transform("ABACABA") == ("BCABAAA", 2) && BWT.Transform("banana") == ("nnbaaa", 3)
        && BWT.Transform("a") == ("a", 0) && BWT.Transform("") == ("", 0);
}

bool RunTestsForInverseTransform()
{
    return BWT.InverseTransform("BCABAAA", 2) == "ABACABA" && BWT.InverseTransform("nnbaaa", 3) == "banana"
        && BWT.InverseTransform("a", 0) == "a" && BWT.InverseTransform("", 0) == "";
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

var (bwtString, endPosition) = BWT.Transform(text);
string inverseString = BWT.InverseTransform(bwtString, endPosition);
if (text != inverseString)
{
    Console.WriteLine("Algorithm works incorrectly");
}

Console.WriteLine($"Transformed string: {bwtString}");
Console.WriteLine($"Inverse transform string: {text}");
