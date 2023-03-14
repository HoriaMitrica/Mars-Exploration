using Codecool.MarsExploration.MapExplorer.MarsRover.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration;

public record SimulationContext(int totalNumberSteps, int timeoutSteps, Rover Rover, Coordinate spaceShip, Map Map,IEnumerable<string> resources, ExplorationOutcome ExplorationOutcome);