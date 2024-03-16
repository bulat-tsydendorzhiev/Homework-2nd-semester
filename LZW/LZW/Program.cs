using LZWTransform;

if (args.Length != 2)
{
    Console.WriteLine("Incorrect number of values!");
    return;
}

string path = args[0];
string key = args[1];

switch (key)
{
    case "-c":
        var ratioWithoutBWT = LZWTransform.LZWTransform.Compress(path);
        var ratioWithBWT = LZWTransform.LZWTransform.Compress(path, true);
        if (ratioWithoutBWT < 0 || ratioWithBWT < 0)
        {
            return;
        }
        Console.WriteLine($"Ratio without BWT = {ratioWithoutBWT}");
        Console.WriteLine($"Ratio with BWT = {ratioWithBWT}");
        break;
    case "-u":
        if (!LZWTransform.LZWTransform.Decompress(path, true))
        {
            return;
        }
        Console.WriteLine("Decompression has completed");
        break;
    default:
        Console.WriteLine("Invalid key value!");
        break;
}
