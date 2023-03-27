using System;

namespace Codecool.MarsExploration.MapExplorer.Logger;

public class LoggerConsole: ILogger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}