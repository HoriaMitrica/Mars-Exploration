using System.Collections.Generic;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover.Model;

public record Rover(string ID, Coordinate CurrentPosition,int SightReach, RoverProgramTypes RoverProgramType)
{
    public string ID { get; init; }
    
    public Coordinate CurrentPosition { get; set; }

    public int SightReach { get; init; }

    public RoverProgramTypes RoverProgramType { get; init; }
}
    