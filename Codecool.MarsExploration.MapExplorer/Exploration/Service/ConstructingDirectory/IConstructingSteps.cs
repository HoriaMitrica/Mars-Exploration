using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.MarsRover.Model;

namespace Codecool.MarsExploration.MapExplorer.Exploration.Service.ConstructingDirectory;

public interface IConstructingSteps
{
    public CommandCenter.Service.CommandCenter ConstructCommandCenter(Simulation simulation,Rover rover);
}