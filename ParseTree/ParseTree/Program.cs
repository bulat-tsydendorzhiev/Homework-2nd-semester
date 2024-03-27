using ParseTree;

if (args.Length != 1)
{
    Console.WriteLine("Incorrect file path. Use -help to learn more.");
    return;
}

if (args[0] == "-help")
{
    Console.WriteLine("Enter path to file with expression written in posfix form.");
    return;
}

string filePath = args[0];
if (!File.Exists(filePath))
{
    Console.WriteLine("File with such path doesn't exist.");
    return;
}

var expression = File.ReadAllText(filePath);

ParseTree.ParseTree tree = new();

try 
{
    tree.Build(expression);
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
    return;
}

try
{
    float result = tree.Calculate();
    tree.Print();
    Console.WriteLine($"={result}");
}
catch (ArgumentException ex1)
{
    Console.WriteLine(ex1.Message);
}
catch(InvalidOperationException ex2)
{
    Console.WriteLine(ex2.Message);
}