using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Model;

public record Simulation(string MapFilePath,Coordinate LandingCoordinate,IEnumerable<string> ElementsToScan,int NumberOfSteps,int CurrentStep, int NumberRovers, int NumberCommandCenters)
{
    public string MapFilePath { get; init; }
    
    public Coordinate LandingCoordinate { get; init; }
    
    public IEnumerable<string> ElementsToScan { get; init; } 
    
    public int NumberOfSteps { get; init; }
    public int CurrentStep { get; init; }
    
    public int NumberRovers { get; set; }
    
    public int NumberCommandCenters { get; set; }
    
};