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
        List<Coordinate> freeCoordinates = new List<Coordinate>();
        var adjacentCoordinates = _coordinateCalculator.GetAdjacentCoordinates(roverCoordinate, _simulationContext.Map.Dimension,1).ToList();
        foreach (var coordinate in adjacentCoordinates)
        {
            if (_simulationContext.Map.Representation[coordinate.X, coordinate.Y] == " " && !coordinatesUsed.Contains(coordinate))
            {
                freeCoordinates.Add(coordinate);
                
            }
        }

        if (freeCoordinates.Count >0)
        {
            var randomCoordinate = freeCoordinates[random.Next(0, freeCoordinates.Count - 1)];
            coordinatesUsed.Add(randomCoordinate);
            return randomCoordinate;
        }
        return coordinatesUsed[coordinatesUsed.Count-1];
    }

    public int[] ScanArea(Coordinate roverCoordinate, List<Coordinate> foundResources)
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

                    if (_simulationContext.Map.Representation[coordinate.X, coordinate.Y] ==
                        _simulationContext.resources.ToList()[1])
                    {
                        water++;
                        foundResources.Add(coordinate);
                    }
                }
                
            }
        }
        int[] resources = new int[2]{minerals,water};
        Console.WriteLine(_simulationContext.resources.ToList()[0]);
        Console.WriteLine(_simulationContext.resources.ToList()[1]);
        return resources;
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

    public ExplorationOutcome Analysis(int[] foundResources, int[] minimumResourcesNeeded, int currentStep, int totalNumberSteps)
    {
        if (foundResources[0] >= minimumResourcesNeeded[0] && foundResources[1] >= minimumResourcesNeeded[1])
        {
            return ExplorationOutcome.Success;
        }
        
        if (foundResources[0] < minimumResourcesNeeded[0] || foundResources[1] < minimumResourcesNeeded[1])
        {
            return ExplorationOutcome.LackOfResources;
        }
        
        if (currentStep < totalNumberSteps)
        {
            return ExplorationOutcome.Timeout;
        }

        return ExplorationOutcome.Error;
    }
    
}