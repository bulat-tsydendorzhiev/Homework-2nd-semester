using Routers;

if (args.Length != 2)
{
    Console.WriteLine("Invalid paths of input and output files.");
    return 0;
}

if (!File.Exists(args[0]))
{
    Console.WriteLine("There is no file with such file path.");
    return 1;
}

try
{
    var network = new Network(args[0]);
    network.WriteConfigurationToFile(args[1]);
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
    return -1;
}
catch (IncorrectFormatOfFileException ex)
{
    Console.WriteLine(ex.Message);
    return -2;
}
catch (DisconnectedNetworkException ex)
{
    Console.WriteLine(ex.Message);
    return -3;
}

return 0;
