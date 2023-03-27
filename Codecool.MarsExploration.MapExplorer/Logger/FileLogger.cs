using System.Net;

namespace Codecool.MarsExploration.MapExplorer.Logger;

public class FileLogger: ILogger
{
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;
    
    public void Log(string message)
    {
        var filePath = $"{WorkDir}\\Resources\\Output";
        File.WriteAllText(filePath,message);
    }
}