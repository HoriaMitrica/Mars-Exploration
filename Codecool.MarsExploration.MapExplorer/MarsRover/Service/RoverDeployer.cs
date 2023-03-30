using System.Linq;
using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace Codecool.MarsExploration.MapExplorer.MarsRover.Service;

public class RoverDeployer: IRoverDeployer
{
    private readonly ICoordinateCalculator _coordinateCalculator;
    private readonly IMapLoader _mapLoader;

    public RoverDeployer()
    {
        _coordinateCalculator = new CoordinateCalculator();
        _mapLoader = new MapLoader.MapLoader();
    }
    public Rover DeployRover(Simulation simulation, int reach,RoverProgramTypes roverProgramType)
    {
        var coordinateRover = new Coordinate(0,0);
        var id = $"rover-{++simulation.NumberRovers}";
        var map = _mapLoader.Load(simulation.MapFilePath).Representation;
        var mapSize = map.GetLength(0);
        var adjacentCoordinatesSpaceShip =
            _coordinateCalculator.GetAdjacentCoordinates(simulation.LandingCoordinate, mapSize, 1).ToList();
        foreach (var coordinate in adjacentCoordinatesSpaceShip)
        {
            if (map[coordinate.X, coordinate.Y] == " ")
            {
                
                coordinateRover = coordinate;
                break;
            }
        }

        return new Rover(id, coordinateRover, reach,roverProgramType);
    }
    
}