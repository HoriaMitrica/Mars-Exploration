using System.Diagnostics;
using Codecool.MarsExploration.MapExplorer.Exploration.Service.LoggerDirectory;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MarsRover.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Service.MiningDirectory;

public class MiningSimulationSteps: IMiningSteps
{
    private readonly ILogger _logger;
    public void MoveRover(Coordinate resourceToMine, Rover currentRover, ref int currentStep)
    {
        if (resourceToMine.X > currentRover.CurrentPosition.X)
        {
            while (currentRover.CurrentPosition.X != resourceToMine.X)
            {
                currentStep++;
                currentRover.CurrentPosition =
                    currentRover.CurrentPosition with { X = currentRover.CurrentPosition.X + 1 };
                var message = $"STEP: {currentStep}; EVENT MOVING TO MINE; UNIT: {currentRover.ID}, POSITION: [{currentRover.CurrentPosition.X},{currentRover.CurrentPosition.Y}]";
                _logger.Log(message);
            }
        }else if (resourceToMine.X < currentRover.CurrentPosition.X)
        {
            while (currentRover.CurrentPosition.X != resourceToMine.X)
            {
                currentStep++;
                currentRover.CurrentPosition =
                    currentRover.CurrentPosition with { X = currentRover.CurrentPosition.X - 1 };
                var message = $"STEP: {currentStep}; EVENT MOVING TO MINE; UNIT: {currentRover.ID}, POSITION: [{currentRover.CurrentPosition.X},{currentRover.CurrentPosition.Y}]";
                _logger.Log(message);
            }
        }

        if (resourceToMine.Y > currentRover.CurrentPosition.Y)
        {
            while (currentRover.CurrentPosition.Y != resourceToMine.Y)
            {
                currentStep++;
                currentRover.CurrentPosition =
                    currentRover.CurrentPosition with { Y = currentRover.CurrentPosition.Y + 1 };
                var message = $"STEP: {currentStep}; EVENT MOVING TO MINE; UNIT: {currentRover.ID}, POSITION: [{currentRover.CurrentPosition.X},{currentRover.CurrentPosition.Y}]";
                _logger.Log(message);
                
            }
        }else if (resourceToMine.Y < currentRover.CurrentPosition.Y)
        {
            while (currentRover.CurrentPosition.Y != resourceToMine.Y)
            {
                currentStep++;
                currentRover.CurrentPosition =
                    currentRover.CurrentPosition with { Y = currentRover.CurrentPosition.Y - 1 };
                var message = $"STEP: {currentStep}; EVENT MOVING TO MINE; UNIT: {currentRover.ID}, POSITION: [{currentRover.CurrentPosition.X},{currentRover.CurrentPosition.Y}]";
                _logger.Log(message);
            }
        }
        
    }
    public void MineResource(ref int currentStep)
    {
        currentStep+=1;
        var message = $"Mining Minerals...Loading";
        _logger.Log(message);
    }
}