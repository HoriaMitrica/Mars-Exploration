using Codecool.MarsExploration.MapExplorer.MarsRover.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Service;

public class ExplorationSimulationSteps
{
    private readonly SimulationContext _simulationContext;
    private readonly ICoordinateCalculator _coordinateCalculator;
    public ExplorationSimulationSteps(SimulationContext simulationContext)
    {
        _simulationContext = simulationContext;
        _coordinateCalculator = new CoordinateCalculator();
    }

    public Coordinate MoveRover(Coordinate roverCoordinate)
    {
        var adjacentCoordinates = _coordinateCalculator.GetAdjacentCoordinates(roverCoordinate, _simulationContext.Map.Dimension,_simulationContext.Rover.sightReach).ToList();
        foreach (var coordinate in adjacentCoordinates)
        {
            if (_simulationContext.Map.Representation[coordinate.X, coordinate.Y] == " ")
            {
                return coordinate;
            }
        }

        return roverCoordinate;
    }

    public int[] ScanArea(Coordinate roverCoordinate, List<Coordinate> foundResources)
    {
        int[] resources = new int[2]{0,0};
        
        for (var i = 1; i <= _simulationContext.Rover.sightReach; i++)
        {
            var adjacentCoordinates =
                _coordinateCalculator.GetAdjacentCoordinates(roverCoordinate, _simulationContext.Map.Dimension, i).ToList();
            foreach (var coordinate in adjacentCoordinates)
            {
                if (foundResources.Contains(coordinate))
                {
                    if (_simulationContext.Map.Representation[coordinate.X, coordinate.Y] ==
                        _simulationContext.resources.ToList()[0])
                    {
                        resources[0]++;
                        foundResources.Add(coordinate);
                    }

                    if (_simulationContext.Map.Representation[coordinate.X, coordinate.Y] ==
                        _simulationContext.resources.ToList()[1])
                    {
                        resources[1]++;
                        foundResources.Add(coordinate);
                    }
                }
                
            }
        }

        return resources;
    }
    
    
}