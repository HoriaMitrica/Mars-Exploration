using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Codecool.MarsExploration.MapExplorer.Configuration.Model;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration.Service;

public class ConfigurationValidator: IConfigurationValidator
{

    private readonly IMapLoader _mapLoader;
    private readonly ICoordinateCalculator _coordinateCalculator;
    
    public ConfigurationValidator()
    {
        _mapLoader = new MapLoader.MapLoader();
        _coordinateCalculator = new CoordinateCalculator();
    }
    
    public bool isValid(Simulation simulation)
    {
        if (isPathValid(simulation) && isLandingSpotValid(simulation.LandingCoordinate, simulation.MapFilePath) && areResourcesSpecified(simulation.ElementsToScan))
        {
            return true;
        }

        return false;
    }

    private bool isPathValid(Simulation simulatio)
    {
        var filePath = simulatio.MapFilePath;
        if (filePath.Length != 0)
        {
            var text = File.ReadAllText(filePath);
            if (text.Length != 0)
            {
                return true;
            }
        }
        return false;
    }

    private bool isLandingSpotValid(Coordinate coordinate,string filePath)
    {
        var map = _mapLoader.Load(filePath);
        var landingSpot = map.Representation[coordinate.X, coordinate.Y];
        if ( landingSpot == " " && isAdjacentCoordinateEmpty(coordinate,map.Representation))
        {
            return true;
        }
        return false;
    }

    private bool isAdjacentCoordinateEmpty(Coordinate coordinate, string[,] mapRepresentation)
    {
        var mapSize = mapRepresentation.GetLength(0);
        var adjacentCoordinates = _coordinateCalculator.GetAdjacentCoordinates(coordinate, mapSize, 1).ToList();
        if (adjacentCoordinates.Count != 0)
        {
            foreach (var adjacentCoordinate in adjacentCoordinates)
            {
                if (mapRepresentation[adjacentCoordinate.X, adjacentCoordinate.Y] == " ")
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool areResourcesSpecified(IEnumerable<string> resources)
    {
        var resourcesList = resources.ToList();
        if (resourcesList.Count != 0)
        {
            return true;
        }
        return false;
    }
    
    
}