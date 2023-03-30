using Codecool.MarsExploration.MapExplorer.Configuration.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Service;

public interface IConfigurationValidator
{
    public bool isValid(Simulation simulation);
}