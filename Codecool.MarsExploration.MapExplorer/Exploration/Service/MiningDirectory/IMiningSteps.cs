using Codecool.MarsExploration.MapExplorer.MarsRover.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Service.MiningDirectory;

public interface IMiningSteps
{
    public void MoveRoverToResource(Coordinate resourceToMine, Rover currentRover);

    public void MineResource();

    public void ReturnRoverToCc(CommandCenter.Service.CommandCenter commandCenter, Rover currentRover);
}