using System.Threading.Channels;
using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapExplorer.Exploration.Model;
using Codecool.MarsExploration.MapExplorer.Exploration.Service.ConstructingDirectory;
using Codecool.MarsExploration.MapExplorer.Exploration.Service.ExplorationDirectory;
using Codecool.MarsExploration.MapExplorer.Exploration.Service.MiningDirectory;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover.Model;
using Codecool.MarsExploration.MapExplorer.MarsRover.Service;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Service.SimulatorDirectory;

public class ExplorationSimulator: IExplorationSimulator
{
    private readonly Simulation _simulation;

    private readonly SimulationContext _simulationContext;

    private readonly IRoverDeployer _rover;

    private readonly IMapLoader _mapLoader;

    private readonly ExplorationSimulationSteps _explorationSimulationSteps;

    private readonly ILogger _logger;

    private readonly IConstructingSteps _constructingSteps = new ConstructingSimulationSteps();

    private CommandCenter.Service.CommandCenter _commandCenter;

    private readonly IMiningSteps _miningSteps = new MiningSimulationSteps();
    public ExplorationSimulator(Simulation simulation, ILogger logger,IRoverDeployer roverDeployer,IMapLoader mapLoader, int reach,RoverProgramTypes roverProgramType)
    {
        _simulation = simulation;
        _rover = roverDeployer;
        _mapLoader = mapLoader;
        _simulationContext = CreateContext(reach,roverProgramType);
        _explorationSimulationSteps = new ExplorationSimulationSteps(_simulationContext);
        _logger = logger;
    }

    private SimulationContext CreateContext(int reach,RoverProgramTypes roverProgramType)
    {
        var numberSteps = _simulation.NumberOfSteps;
        var rover = _rover.DeployRover(_simulation,reach,roverProgramType);
        var shipCoordinate = _simulation.LandingCoordinate;
        var map = _mapLoader.Load(_simulation.MapFilePath);
        var resources = _simulation.ElementsToScan;
        var numberStepsTimeOut = numberSteps+1;
        return new SimulationContext(numberSteps,numberStepsTimeOut,rover,shipCoordinate,map,resources);
    }

    public ExplorationOutcome? Simulate(int minimumMineralsNeeded)
    {
        List<Coordinate> coordinatesUsed = new List<Coordinate>();
        List<Coordinate> foundResources = new List<Coordinate>();
        List<Coordinate> SuitableCcCoordinates = new List<Coordinate>();
        ExplorationOutcome outcome;
        int currentStep = _simulation.CurrentStep; 
        while (currentStep < _simulationContext.totalNumberSteps)
        {
            outcome = ExplorationOutcomeSteps(coordinatesUsed, currentStep,  ref SuitableCcCoordinates, foundResources);

            if (outcome== ExplorationOutcome.Success)
            {
                _commandCenter = _constructingSteps.ConstructCommandCenter(_simulation,_simulationContext.Rover);
                
                var message = $"STEP: {currentStep}; EVENT CONSTRUCTING; UNIT: {_commandCenter.Rovers[0].ID}, POSITION: [{_commandCenter.Rovers[0].CurrentPosition.X},{_commandCenter.Rovers[0].CurrentPosition.Y}]";
                _logger.Log(message);
                _commandCenter.ChangeRoverProgram(_commandCenter.Rovers[0],RoverProgramTypes.Mining);
                _commandCenter.ScanForResources(_simulationContext.Map);
                DisplayMap(SuitableCcCoordinates,_commandCenter.NearbyResources);

                MiningSteps(currentStep);
                _commandCenter.ChangeRoverProgram(_commandCenter.Rovers[0],RoverProgramTypes.Constructing);
                _commandCenter.Rovers[0].CurrentPosition = SuitableCcCoordinates[1];
                _constructingSteps.ConstructCommandCenter(_simulation, _commandCenter.Rovers[0]);
                if (_simulation.NumberCommandCenters == 2 && _simulation.NumberRovers == 3)
                {
                    DisplayMap(SuitableCcCoordinates, _commandCenter.NearbyResources);
                    Console.WriteLine(outcome);
                    return outcome;
                }
                DisplayMap(SuitableCcCoordinates,_commandCenter.NearbyResources);
                return ExplorationOutcome.FailedConstruction;
            }
            
            if (outcome==ExplorationOutcome.LackOfResources)
            {
                _explorationSimulationSteps.Log(_logger,currentStep,_simulationContext.Rover.ID, _simulationContext.Rover.CurrentPosition,outcome.ToString());
                return outcome;
            }
            currentStep++;
            
        }
        
        outcome = ExplorationOutcome.Timeout;

        _explorationSimulationSteps.Log(_logger,currentStep,_simulationContext.Rover.ID, _simulationContext.Rover.CurrentPosition,outcome.ToString());
        DisplayMap(coordinatesUsed, _commandCenter.NearbyResources);
        return outcome; 
    }

    private void MiningSteps(int currentStep)
    {
        while (_commandCenter.Rovers.Count < 3)
        {
            for (var i = 0; i < _commandCenter.Rovers.Count && _commandCenter.Rovers.Count<3; i++)
            {
                _miningSteps.MoveRover(_commandCenter.NearbyResources[i], _commandCenter.Rovers[i], ref currentStep);
                _miningSteps.MineResource(ref currentStep);
                _miningSteps.MoveRover(_commandCenter.Position, _commandCenter.Rovers[i], ref currentStep);
                _commandCenter.ResourcesStored++;
                if (_commandCenter.ResourcesStored >= 2)
                {
                    _commandCenter.BuildRover();
                    _commandCenter.ResourcesStored -= 2;
                }
            }
        }
        while(_commandCenter.ResourcesStored<5){
            for (var i = 0; i < _commandCenter.Rovers.Count; i++)
            {
                _miningSteps.MoveRover(_commandCenter.NearbyResources[i], _commandCenter.Rovers[i], ref currentStep);
                _miningSteps.MineResource(ref currentStep);
                _miningSteps.MoveRover(_commandCenter.Position, _commandCenter.Rovers[i], ref currentStep);
                _commandCenter.ResourcesStored++;
                
            }
        }
        
    }

    private ExplorationOutcome ExplorationOutcomeSteps(List<Coordinate> coordinatesUsed,int currentStep,ref List<Coordinate> SuitableCcCoordinates, List<Coordinate> foundResources)
    {
        ExplorationOutcome outcome;
        _simulationContext.Rover.CurrentPosition =
            _explorationSimulationSteps.MoveRover(_simulationContext.Rover.CurrentPosition, coordinatesUsed);

        _explorationSimulationSteps.ScanArea(_simulationContext.Rover.CurrentPosition, foundResources,
            ref SuitableCcCoordinates);

        _explorationSimulationSteps.Log(_logger, currentStep, _simulationContext.Rover.ID,
            _simulationContext.Rover.CurrentPosition, "");
        outcome = _explorationSimulationSteps.Analysis(SuitableCcCoordinates, currentStep, _simulationContext.totalNumberSteps);
        return outcome;
    }

    private void DisplayMap(List<Coordinate> ccCoordinates,List<Coordinate> resourcesSeen)
    {
        string[,] mapp = new string[32, 32];
        for (var i = 0; i < mapp.GetLength(0); i++)
        {
            for (var j = 0; j < mapp.GetLength(1); j++)
            {
                var currentCoordinate = new Coordinate(i, j);
                if (ccCoordinates.Contains(currentCoordinate))
                {
                    mapp[i, j] = "C";
                }
                else
                {
                    mapp[i, j] = _simulationContext.Map.Representation[i, j];
                }
            }
        }

        for (var i = 0; i < mapp.GetLength(0); i++)
        {
            Console.Write($"{i % 10}| ");
            for (var j = 0; j < mapp.GetLength(1); j++)
            {
                if (mapp[i, j] == "%" && resourcesSeen.Contains(new Coordinate(i,j)))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                else if (mapp[i, j] == "C")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }

                Console.Write(mapp[i, j]);
            }

            Console.WriteLine();
        }

        Console.ForegroundColor = ConsoleColor.White;
    }
}