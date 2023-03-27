using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Service;

public interface IStepLogger
{
    public void Log(ILogger logger, int currentStep, string RoverID, Coordinate roverCoordinate, string foundOutcome);
}