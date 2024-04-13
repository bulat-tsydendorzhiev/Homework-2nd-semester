using LZWTask;

if (args[0] == "-help")
{
    string message = """
                    LZW compression/decompression console app.
                    
                    If you want to make compression/decompression of the file,
                    then you should enter two arguments:
                    1) path to file
                    2) key("-compress"/"-decompress" for compression/decompression)
                    """;

    Console.WriteLine(message);
    return;
}

if (args.Length != 2)
{
    Console.WriteLine("Incorrect number of values. Enter -help for further instructions.");
    return;
}

string path = args[0];
string key = args[1];

switch (key)
{
    case "-compress":
        var ratioWithoutBWT = LZWTransform.Compress(path);
        var ratioWithBWT = LZWTransform.Compress(path, true);

        if (ratioWithoutBWT < 0 || ratioWithBWT < 0)
        {
            return;
        }

        Console.WriteLine("Compression has completed!");
        Console.WriteLine($"Ratio without BWT = {ratioWithoutBWT}");
        Console.WriteLine($"Ratio with BWT = {ratioWithBWT}");
        Console.WriteLine($"Compression with BWT is better in {ratioWithBWT / ratioWithoutBWT} times.");
        break;
    case "-decompress":
        if (!LZWTransform.Decompress(path, true))
        {
            return;
        }

        Console.WriteLine("Decompression has completed!");
        break;
    default:
        Console.WriteLine("Invalid key value was given!");
        break;
}
