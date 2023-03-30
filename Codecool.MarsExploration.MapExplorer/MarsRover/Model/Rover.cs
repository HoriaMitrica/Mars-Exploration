using System.Collections.Generic;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover.Model;

public record Rover()
{
    public string ID { get; set; }
    
    public Coordinate CurrentPosition { get; set; }

    public int SightReach { get; set; }

    public RoverProgramTypes RoverProgramType { get; set; }

    public Rover(string id, Coordinate currentPosition, int sightReach, RoverProgramTypes roverProgramType): this()
    {
        ID = id;
        CurrentPosition = currentPosition;
        SightReach = sightReach;
        RoverProgramType = roverProgramType;
    }
}
    