using System.Text;

namespace Routers;

/// <summary>
/// Class which works with topology of the network by making its optimal configuration.
/// </summary>
public class Network
{
    private List<(int Router, int ConnectedRouter, int Bandwidth)> _connections = new();
    
    private int _numberOfRouters;

    /// <summary>
    /// Initializes a new instance of the <see cref="Network"/> class.
    /// </summary>
    /// <param name="filePath">Reading file path.</param>
    /// <exception cref="IncorrectFormatOfFileException">Throws when file is empty.</exception>
    /// <exception cref="IncorrectFormatOfFileException">Throws when invalid format of connections was given from file.</exception>
    /// <exception cref="IncorrectFormatOfFileException">Throws when invalid connection values was given from file.</exception>
    public Network(string filePath)
    {
        GetNetworkFromFile(filePath);
    }
    
    private void GetNetworkFromFile(string inputFilePath)
    {
        var connections = File.ReadLines(inputFilePath).ToList();
        
        if (!connections.Any())
        {
            throw new IncorrectFormatOfFileException("File cannot be empty.");
        }

        foreach (var connection in connections)
        {
            var routers = connection.Replace('(', ' ').Replace(')', ' ').Split(':');
            var connectedRouters = routers[1].Split(',');
            if (routers.Length != 2)
            {
                throw new IncorrectFormatOfFileException("Invalid format of connections.");
            }
            
            int router = int.Parse(routers[0]);
            
            foreach (var item in connectedRouters)
            {
                var routerAndBandwidth = item.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (routerAndBandwidth.Length != 2)
                {
                    throw new IncorrectFormatOfFileException("Invalid format of connections.");
                }
                
                int connectedRouter = int.Parse(routerAndBandwidth[0]);
                int bandwidth = int.Parse(routerAndBandwidth[1]);
                
                if (!IsValidConnection(router, connectedRouter, bandwidth))
                {
                    throw new IncorrectFormatOfFileException("Invalid connection values was given.");
                }
                
                _connections.Add((router, connectedRouter, bandwidth));
                _numberOfRouters = Math.Max(_numberOfRouters, connectedRouter);
            }
        }
    }

    /// <summary>
    /// Writes configuration to the file.
    /// </summary>
    /// <exception cref="DisconnectedNetworkException">Throws when network is disconnected.</exception>
    /// <param name="outputFilePath">File in which configuration will be written.</param>
    public void WriteConfigurationToFile(string outputFilePath)
    {
        var configuration = GetConfiguration();
        
        using (var writer = new StreamWriter(outputFilePath, false, Encoding.UTF8))
        {
            foreach (var connection in configuration)
            {
                writer.Write($"{connection.Key}:");
                writer.Write(string.Join(',', connection.Value));
                writer.Write("\n");
            }
        }
    }

    private Dictionary<string, List<string>> GetConfiguration()
    {
        var rawConfiguration = FindMaximumSpanningTree();
        var configuration = new Dictionary<string, List<string>>();
        
        foreach (var (router, connectedRouter, bandwidth) in rawConfiguration)
        {
            string strRouter = router.ToString();
            
            if (!configuration.ContainsKey(strRouter))
            {
                configuration[strRouter] = new();
            }
            
            configuration[strRouter].Add($" {connectedRouter} ({bandwidth})");
        }
        
        return configuration;
    }

    private List<(int Router, int ConnectedRouter, int Bandwidth)> FindMaximumSpanningTree()
    {
        var sortedConnections = _connections.OrderByDescending((connection) => connection.Bandwidth).ToList();
        var disjointSetUnion = new DisjointSetUnion(_numberOfRouters);
        var result = new List<(int, int, int)>();
        
        int count = 0;
        int i = 0;
        
        while (count < _numberOfRouters - 1 && i < sortedConnections.Count)
        {
            int routerParent = disjointSetUnion.Find(sortedConnections[i].Router - 1);
            int connectedRouterParent = disjointSetUnion.Find(sortedConnections[i].ConnectedRouter - 1);
            
            if (routerParent != connectedRouterParent)
            {
                result.Add(sortedConnections[i]);
                disjointSetUnion.Unite(routerParent, connectedRouterParent);
                ++count;
            }

            ++i;
        }
        
        if (count != _numberOfRouters - 1)
        {
            throw new DisconnectedNetworkException("Network is disconnected.");
        }
        
        return result;
    }

    private static bool IsValidConnection(int router, int connectedRouter, int bandwidth) => router > 0 && connectedRouter > 0 && bandwidth > 0;
}