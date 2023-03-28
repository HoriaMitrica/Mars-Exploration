using Codecool.MarsExploration.MapExplorer.MarsRover.Model;

namespace Codecool.MarsExploration.MapExplorer.CommandCenter.Service;

public class CommandCenter 
{
    public int SightReach { get; }

    public List<Rover> Rovers = new List<Rover>();
    
    public int ResourcesStored { get; set; }

    public CommandCenter(int sightReach, List<Rover> rovers, int resourcesStored)
    {
        SightReach = sightReach;
        Rovers = rovers;
        ResourcesStored = resourcesStored;
    }
}