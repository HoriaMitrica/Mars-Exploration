namespace Codecool.MarsExploration.MapExplorer.Exploration.Service;

public interface IExplorationSimulator
{
    public ExplorationOutcome? Simulate(int minimumMineralsNeeded);
}