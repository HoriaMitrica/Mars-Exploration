using System.Collections.Generic;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration;

public record Simulation(string MapFilePath,Coordinate landingCoordinate,IEnumerable<string> elementsToScan, int numberOfSteps, int currentStep );