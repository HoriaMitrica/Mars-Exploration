using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapExplorer.MarsRover.Model;
using Codecool.MarsExploration.MapExplorer.MarsRover.Service;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.CommandCenter.Service;


public class CommandCenter 
{
    private readonly ICoordinateCalculator _coordinateCalculator = new CoordinateCalculator();

    private readonly IRoverDeployer _roverDeployer = new RoverDeployer();
    public int SightReach { get; }

    public string ID { get; set; }
    public Coordinate Position { get; set; }
    public List<Rover> Rovers { get; } = new List<Rover>();
    
    public int ResourcesStored { get; set; }

    public Simulation Simulation { get; set; }
    public List<Coordinate> NearbyResources { get; set; } = new List<Coordinate>();
    public CommandCenter(Rover rover,string id,Simulation simulation)
    {
        Position = rover.CurrentPosition;
        SightReach = rover.SightReach;
        Rovers.Add(rover);
        ID = id;
        Simulation = simulation;
    }

    public void ScanForResources(Map map)
    {
        var currentPosAdjacent = _coordinateCalculator.GetAdjacentCoordinates(Position, map.Dimension,SightReach).ToList();
        var allCoordinates = _coordinateCalculator.GetAdjacentCoordinates(currentPosAdjacent, map.Dimension).ToList();
        foreach (var coord in allCoordinates)
        {
            if (map.Representation[coord.X, coord.Y] == "%")
            {
                NearbyResources.Add(coord);           
            }
        }
    }
    public void BuildRover()
    {
        _roverDeployer.DeployRover(Simulation,1,RoverProgramTypes.Mining);
    }

    public void ChangeRoverProgram(Rover rover,RoverProgramTypes roverProgramType)
    {
        rover.RoverProgramType = roverProgramType;
    }
    
}