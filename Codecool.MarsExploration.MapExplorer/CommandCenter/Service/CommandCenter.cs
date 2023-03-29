using Codecool.MarsExploration.MapExplorer.MarsRover.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.CommandCenter.Service;

public class CommandCenter 
{
    public int SightReach { get; }

    public string ID { get; set; }
    public Coordinate Position { get; set; }
    public List<Rover> Rovers { get; } = new List<Rover>();
    
    public int ResourcesStored { get; set; }

    public CommandCenter(Rover rover,string id)
    {
        Position = rover.CurrentPosition;
        SightReach = rover.SightReach;
        Rovers.Add(rover);
        ID = id;
    }
    
    
}