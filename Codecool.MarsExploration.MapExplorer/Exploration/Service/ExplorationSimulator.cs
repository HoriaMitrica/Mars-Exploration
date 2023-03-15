using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Configuration.Service;
using Codecool.MarsExploration.MapExplorer.Logger;
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

    private readonly ExplorationSimulationSteps _explorationSimulationSteps;

    private readonly ILogger _logger;
    public ExplorationSimulator(Simulation simulation, ILogger logger,IRoverDeployer roverDeployer,IMapLoader mapLoader, int reach)
    {
        _simulation = simulation;
        _rover = roverDeployer;
        _mapLoader = mapLoader;
        _simulationContext = CreateContext(reach);
        _explorationSimulationSteps = new ExplorationSimulationSteps(_simulationContext);
        _logger = logger;
    }

    private SimulationContext CreateContext(int reach)
    {
        var numberSteps = _simulation.numberOfSteps;
        var rover = _rover.DeployRover(_simulation,reach);
        var shipCoordinate = _simulation.landingCoordinate;
        var map = _mapLoader.Load(_simulation.MapFilePath);
        var resources = _simulation.elementsToScan;
        var numberStepsTimeOut = numberSteps+1;
        return new SimulationContext(numberSteps,numberStepsTimeOut,rover,shipCoordinate,map,resources);
    }

    public ExplorationOutcome? Simulate(int minimumMineralsNeeded, int minimumWaterNeeded)
    {
        int[] totalResources = new int[2]{0,0};
        int[] minimumResourcesNeeded = new int[2]{minimumMineralsNeeded,minimumWaterNeeded};
        Coordinate RoverCurrentCoordinate = _simulationContext.Rover.currentPosition;
        List<Coordinate> coordinatesUsed = new List<Coordinate>();
        List<Coordinate> foundResources = new List<Coordinate>();
        int currentStep = _simulation.currentStep; 
        while (currentStep < _simulationContext.totalNumberSteps)
        {
            RoverCurrentCoordinate = _explorationSimulationSteps.MoveRover(RoverCurrentCoordinate,coordinatesUsed);
            
            var currentResources = _explorationSimulationSteps.ScanArea(RoverCurrentCoordinate, foundResources);
            totalResources[0] += currentResources[0];
            totalResources[1] += currentResources[1];
            
            _explorationSimulationSteps.Log(_logger,currentStep,_simulationContext.Rover.ID, RoverCurrentCoordinate,"");
            currentStep++;
        }
        
        var outcome = _explorationSimulationSteps.Analysis(totalResources, minimumResourcesNeeded,currentStep, _simulationContext.totalNumberSteps);
        _explorationSimulationSteps.Log(_logger,currentStep,_simulationContext.Rover.ID, RoverCurrentCoordinate,outcome.ToString());


        string[,] mapp = new string[32, 32];
        for (var i = 0; i < mapp.GetLength(0); i++)
        {
            for (var j = 0; j < mapp.GetLength(1); j++)
            {
                var currentCoordinate = new Coordinate(i, j);
                if (coordinatesUsed.Contains(currentCoordinate))
                {
                    mapp[i, j] = "1";
                }
                else
                {
                    mapp[i, j] = _simulationContext.Map.Representation[i, j];
                }
            }
        }
        
        for (var i = 0; i < mapp.GetLength(0); i++)
        {
            Console.Write($"{i%10}| ");
            for (var j = 0; j < mapp.GetLength(1); j++)
            {
                if (mapp[i, j] == "%" || mapp[i, j] == "*")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.ForegroundColor= ConsoleColor.Cyan;
                }
                Console.Write(mapp[i,j]);
            }

            Console.WriteLine();
        }

        Console.WriteLine(totalResources[0]);
        Console.WriteLine(totalResources[1]);
        return outcome; 
    }
    
    
    
}