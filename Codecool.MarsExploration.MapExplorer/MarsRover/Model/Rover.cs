using System.Collections.Generic;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover.Model;

public record Rover(int ID, Coordinate currentPosition, int sightReach, IEnumerable<Coordinate> resourcesCoordinates);