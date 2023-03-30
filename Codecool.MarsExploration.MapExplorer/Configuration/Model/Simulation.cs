using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Model;

public record Simulation()
{
    public string MapFilePath { get; init; }
    
    public Coordinate LandingCoordinate { get; init; }
    
    public IEnumerable<string> ElementsToScan { get; init; } 
    
    public int NumberOfSteps { get; init; }
    public int CurrentStep { get; init; }
    
    public int NumberRovers { get; set; }
    
    public int NumberCommandCenters { get; set; }

    public Simulation(string mapFilePath, Coordinate landingCoordinate, IEnumerable<string> elementsToScan, int numberOfSteps, int currentStep, int numberRovers, int numberCommandCenters) : this()
    {
        MapFilePath = mapFilePath;
        LandingCoordinate = landingCoordinate;
        ElementsToScan = elementsToScan;
        NumberOfSteps = numberOfSteps;
        CurrentStep = currentStep;
        NumberRovers = numberRovers;
        NumberCommandCenters = numberCommandCenters;
    }
};