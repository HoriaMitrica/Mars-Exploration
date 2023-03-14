using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Configuration.Service;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover.Model;
using Codecool.MarsExploration.MapExplorer.MarsRover.Service;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Service;

public class ExplorationSimulator
{
    private readonly Simulation _simulation;

    private readonly SimulationContext _simulationContext;

    private readonly IRoverDeployer _rover;

    private readonly IMapLoader _mapLoader;

    private readonly IConfigurationValidator _configurationValidator;
    private readonly ExplorationSimulationSteps _explorationSimulationSteps;
    public ExplorationSimulator(Simulation simulation)
    {
        _simulation = simulation;
        _simulationContext = CreateContext();
        _explorationSimulationSteps = new ExplorationSimulationSteps(_simulationContext);
    }

    private SimulationContext CreateContext()
    {
        var numberSteps = _simulation.numberOfSteps;
        Rover rover = _rover.DeployRover(_simulation);
        var shipCoordinate = _simulation.landingCoordinate;
        var map = _mapLoader.Load(_simulation.MapFilePath);
        var resources = _simulation.elementsToScan;
        var numberStepsTimeOut = numberSteps+1;
        return new SimulationContext(numberSteps,numberStepsTimeOut,rover,shipCoordinate,map,resources);
    }

    public ExplorationOutcome? Simulate()
    {
        int res = 0;
        Coordinate RoverCurrentCoordinate = _simulationContext.Rover.currentPosition;
        List<Coordinate> foundResources = new List<Coordinate>();
        while step<maxstep
        {
            RoverCurrentCoordinate = _explorationSimulationSteps.MoveRover(RoverCurrentCoordinate);
            res+=scan
                    log
                        step++
                        
        }
        analysis(res)
        return null; 
    }
    
}