using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MarsRover.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Service;

public class ExplorationSimulationSteps
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

    public int ScanArea(Coordinate roverCoordinate, List<Coordinate> foundResources)
    {
        
        int minerals = 0;
        int water = 0;
        for (var i = 1; i <= _simulationContext.Rover.sightReach; i++)
        {
            var adjacentCoordinates =
                _coordinateCalculator.GetAdjacentCoordinates(roverCoordinate, _simulationContext.Map.Dimension, i).ToList();
            foreach (var coordinate in adjacentCoordinates)
            {
                if (!foundResources.Contains(coordinate))
                {
                    if (_simulationContext.Map.Representation[coordinate.X, coordinate.Y] ==
                        _simulationContext.resources.ToList()[0])
                    {
                        minerals++;
                        foundResources.Add(coordinate);
                    }
                }
            }
        }
        return minerals;
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

    public ExplorationOutcome Analysis(int foundResources, int minimumResourcesNeeded, int currentStep, int totalNumberSteps)
    {
        int halfTotalSteps = totalNumberSteps / 4;
        int halfMinimumMinerals = minimumResourcesNeeded / 2;

        if (foundResources >= minimumResourcesNeeded )
        {
            return ExplorationOutcome.Success;
        }
        
        if (foundResources < halfMinimumMinerals && currentStep>halfTotalSteps)
        {
            return ExplorationOutcome.LackOfResources;
        }
        return ExplorationOutcome.Timeout;
    }
}