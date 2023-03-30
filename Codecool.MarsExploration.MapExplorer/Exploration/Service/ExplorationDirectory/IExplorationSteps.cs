using Codecool.MarsExploration.MapExplorer.Exploration.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Service.ExplorationDirectory;

public interface IExplorationSteps
{
    public Coordinate MoveRover(Coordinate roverCoordinate, List<Coordinate> coordinatesUsed);

     public void ScanArea(Coordinate roverCoordinate, List<Coordinate> foundResources, ref List<Coordinate> SuitableCcCoordinate);
    
    public ExplorationOutcome Analysis(List<Coordinate> suitableCcSpots, int currentStep, int totalNumberSteps);

}