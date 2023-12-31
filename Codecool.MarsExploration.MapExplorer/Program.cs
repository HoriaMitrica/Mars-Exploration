﻿using System;
using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapExplorer.Configuration.Service;
using Codecool.MarsExploration.MapExplorer.Exploration.Service;
using Codecool.MarsExploration.MapExplorer.Exploration.Service.SimulatorDirectory;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover.Model;
using Codecool.MarsExploration.MapExplorer.MarsRover.Service;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer;

class Program
{
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;
    private static readonly ILogger _logger = new LoggerConsole();
    private static Simulation _simulation;
    private static IConfigurationValidator _configurationValidator = new ConfigurationValidator();
    private static IRoverDeployer _roverDeployer = new RoverDeployer();
    private static IMapLoader _mapLoader = new MapLoader.MapLoader();
    public static void Main(string[] args)
    {
        string mapFile = $@"{WorkDir}\Resources\exploration-2.map";
        Coordinate landingSpot = new Coordinate(6, 6);
        List<string> elementsToScan = new List<string>() { "%"};
        int reach = 3;
        RoverProgramTypes roverProgramType = RoverProgramTypes.Exploring;
        int numberSteps = 1000;
        int minimumMineralsNeeded = 8;
        _simulation = new Simulation(mapFile, landingSpot, elementsToScan, numberSteps, 0,0,0);
        if (_configurationValidator.isValid(_simulation))
        {
            ExplorationSimulator explorationSimulator = new ExplorationSimulator(_simulation,_logger,_roverDeployer,_mapLoader, reach,roverProgramType);
            explorationSimulator.Simulate(minimumMineralsNeeded);  
            Console.WriteLine("The Rover magically teleported back to the spaceship!");
        }
        else
        {
            _logger.Log("Invalid Simulation");
        }
    }
}
