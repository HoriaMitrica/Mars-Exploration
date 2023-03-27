using System.IO;
using System.Linq;
using System.Net;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.MapLoader;

public class MapLoader: IMapLoader
{
    public Map Load(string mapFile)
    {
        var map = File.ReadAllLines(mapFile).ToList();
        string[,] representationArray = new string[map.Count, map.Count];
        for (var i = 0; i < map.Count; i++)
        {
            var row = map[i];
            for (var j = 0; j < map.Count; j++)
            {
                representationArray[i, j] = row[j].ToString();
            }
        }

        return new Map(representationArray, true);
    }
}