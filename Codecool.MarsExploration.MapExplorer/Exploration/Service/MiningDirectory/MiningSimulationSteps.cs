using System.Diagnostics;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MarsRover.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Service.MiningDirectory;

public class MiningSimulationSteps: IMiningSteps, ILogger
{
    public void MoveRoverToResource(Coordinate resourceToMine, Rover currentRover)
    {
        int step = 0;
        if (resourceToMine.X > currentRover.CurrentPosition.X)
        {
            while (currentRover.CurrentPosition.X != resourceToMine.X)
            {
                step++;
                currentRover.CurrentPosition =
                    currentRover.CurrentPosition with { X = currentRover.CurrentPosition.X + 1 };
                var message = $"STEP: {step}; EVENT MOVING TO MINE; UNIT: {currentRover.ID}, POSITION: [{currentRover.CurrentPosition.X},{currentRover.CurrentPosition.Y}]";
                Log(message);
            }
        }
    }

    public void MineResource()
    {
        throw new NotImplementedException();
    }

    public void ReturnRoverToCc(CommandCenter.Service.CommandCenter commandCenter, Rover currentRover)
    {
        throw new NotImplementedException();
    }

    public void Log(string message)
    {
        throw new NotImplementedException();
    }
}