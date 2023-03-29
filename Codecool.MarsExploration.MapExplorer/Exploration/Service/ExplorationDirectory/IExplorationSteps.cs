using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Service.ExplorationDirectory;

public interface IExplorationSteps
{
    public Coordinate MoveRover(Coordinate roverCoordinate, List<Coordinate> coordinatesUsed);

     public int ScanArea(Coordinate roverCoordinate, List<Coordinate> foundResources);
    
    public ExplorationOutcome Analysis(int foundResources, int minimumResourcesNeeded, int currentStep, int totalNumberSteps);

}