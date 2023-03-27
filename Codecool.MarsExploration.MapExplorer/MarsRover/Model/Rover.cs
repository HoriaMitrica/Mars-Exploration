using System.Collections.Generic;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover.Model;

public record Rover(string ID, Coordinate currentPosition, int sightReach, IEnumerable<Coordinate>? resourcesCoordinates=null);