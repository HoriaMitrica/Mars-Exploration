using Codecool.MarsExploration.MapExplorer.Exploration.Model;
using Codecool.MarsExploration.MapExplorer.Exploration.Service.LoggerDirectory;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Service.ExplorationDirectory;

public class ExplorationSimulationSteps: IExplorationSteps, IStepLogger
{
    private readonly SimulationContext _simulationContext;
    private readonly ICoordinateCalculator _coordinateCalculator;
    private Random random = new Random();
    public ExplorationSimulationSteps(SimulationContext simulationContext)
    {
        _simulationContext = simulationContext;
        _coordinateCalculator = new CoordinateCalculator();
    }

    public Coordinate MoveRover(Coordinate roverCoordinate, List<Coordinate> coordinatesUsed)
    {
        var adjacentCoordinates = _coordinateCalculator.GetAdjacentCoordinates(roverCoordinate, _simulationContext.Map.Dimension,1).Where(coordinate => _simulationContext.Map.Representation[coordinate.X, coordinate.Y] == " ").ToList();
        var freeCoordinates = adjacentCoordinates.Where(coordinate => !coordinatesUsed.Contains(coordinate)).ToList();

        if (freeCoordinates.Count() > 0)
        {
            var randomCoordinate = freeCoordinates[random.Next(0, freeCoordinates.Count)];
            coordinatesUsed.Add(randomCoordinate);
            return randomCoordinate;
        }
        return adjacentCoordinates[random.Next(0, adjacentCoordinates.Count)];
    }

    public void ScanArea(Coordinate roverCoordinate, List<Coordinate> foundResources,ref List<Coordinate> SuitableCcCoordinate)
    {
        int minerals = 0;
        List<Coordinate> currentResourcesVisible = new List<Coordinate>();
        for (var i = 1; i <= _simulationContext.Rover.SightReach; i++)
        {
            var adjacentCoordinates =
                _coordinateCalculator.GetAdjacentCoordinates(roverCoordinate, _simulationContext.Map.Dimension, i).ToList();
            var allCoordinates = _coordinateCalculator.GetAdjacentCoordinates(adjacentCoordinates, _simulationContext.Map.Dimension).ToList();

            foreach (var coordinate in allCoordinates)
            {
                if (!foundResources.Contains(coordinate))
                {
                    if (_simulationContext.Map.Representation[coordinate.X, coordinate.Y] ==
                        _simulationContext.resources.ToList()[0])
                    {
                        minerals++;
                        currentResourcesVisible.Add(coordinate);
                    }
                }
            }
        }

        if (minerals >= 3)
        {
            foreach (var coord in currentResourcesVisible)
            {
                foundResources.Add(coord);
            }
            SuitableCcCoordinate.Add(roverCoordinate);
        }
        
    }
    
    public void Log(ILogger logger, int currentStep,string RoverID, Coordinate roverCoordinate,string foundOutcome )
    {
        if (foundOutcome == "")
        {
            var message = $"STEP: {currentStep}; EVENT POSITION; UNIT: {RoverID}, POSITION: [{roverCoordinate.X},{roverCoordinate.Y}]";
            logger.Log(message);
        }
        else
        {
            var message = $"STEP: {currentStep}; EVENT OUTCOME; OUTCOME:{foundOutcome}";
            logger.Log(message);
        }
    }

    public ExplorationOutcome Analysis(List<Coordinate> suitableCcSpots, int currentStep, int totalNumberSteps)
    {
        Console.WriteLine(String.Join("\n",suitableCcSpots));
        if (suitableCcSpots.Count>=2)
        {
            return ExplorationOutcome.Success;
        }
        
        if (suitableCcSpots.Count < 2 && currentStep>totalNumberSteps/2)
        {
            return ExplorationOutcome.LackOfResources;
        }
        return ExplorationOutcome.Timeout;
    }
}