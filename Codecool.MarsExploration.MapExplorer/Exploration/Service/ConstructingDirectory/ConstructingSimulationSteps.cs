using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapExplorer.Exploration.Service.LoggerDirectory;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MarsRover.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Service.ConstructingDirectory;

public class ConstructingSimulationSteps: IConstructingSteps, IStepLogger
{
    
    public CommandCenter.Service.CommandCenter ConstructCommandCenter(Simulation simulation,Rover rover)
    {
        var idCc = $"command-center-{++simulation.NumberCommandCenters}";
        return new CommandCenter.Service.CommandCenter(rover, idCc,simulation);
    }

    public void Log(ILogger logger, int currentStep, string id, Coordinate stepCoordinate, string? foundOutcome)
    {
        var message = $"STEP: {currentStep}; EVENT Constructing; UNIT: {id}, POSITION: [{stepCoordinate.X},{stepCoordinate.Y}]";
        logger.Log(message);
    }
}