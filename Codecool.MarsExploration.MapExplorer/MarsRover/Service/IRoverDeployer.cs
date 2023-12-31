﻿using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapExplorer.MarsRover.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover.Service;

public interface IRoverDeployer
{
    public Rover DeployRover(Simulation simulation, int reach,RoverProgramTypes roverProgramType);
}