using Codecool.MarsExploration.MapExplorer.MarsRover.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Service.MiningDirectory;

public interface IMiningSteps
{
    public void MoveRover(Coordinate resourceToMine, Rover currentRover,ref int currentStep);

    public void MineResource(ref int currentStep);

}