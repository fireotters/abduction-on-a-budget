using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelHandling
{
    public static List<World> worlds = new List<World>()
    {
        new World(world: 0, name: "World 1", levels: new List<Level>()
        {
            new Level(world: 0, level: 0, humans: 3, "Welcome to Earth"),
            new Level(world: 0, level: 1, humans: 2, "The Reservoir"),
            new Level(world: 0, level: 2, humans: 2, "Abandonned Sewer"),
            new Level(world: 0, level: 3, humans: 0, "Blank"),
            new Level(world: 0, level: 4, humans: 0, "Blank"),
        }),
        new World(world: 1, name: "World 2", levels: new List<Level>()
        {
            new Level(world: 1, level: 0, humans: 0, "Blank"),
            new Level(world: 1, level: 1, humans: 0, "Blank"),
            new Level(world: 1, level: 2, humans: 0, "Blank"),
            new Level(world: 1, level: 3, humans: 0, "Blank"),
            new Level(world: 1, level: 4, humans: 0, "Blank"),
        }),
        new World(world: 2, name: "World 3", levels: new List<Level>()
        {
            new Level(world: 2, level: 0, humans: 0, "Blank"),
            new Level(world: 2, level: 1, humans: 0, "Blank"),
            new Level(world: 2, level: 2, humans: 0, "Blank"),
            new Level(world: 2, level: 3, humans: 0, "Blank"),
            new Level(world: 2, level: 4, humans: 0, "Blank"),
        }),
    };
}

public class World
{
    public int WorldNum;
    public string WorldName;
    public List<Level> LevelsInWorld;
    public int TotalHumansInWorld;

    public World(int world, string name, List<Level> levels)
    {
        WorldNum = world;
        WorldName = name;
        LevelsInWorld = levels;

        // Find total human count for this world
        int totalHumans = 0;
        foreach (Level level in levels)
        {
            totalHumans += level.HumansPresent;
        }
        TotalHumansInWorld = totalHumans;
    }
}

public class Level
{
    public int WorldNum;
    public int LevelNum;
    public string LevelString;
    public int HumansPresent;
    public string LevelName;

    public Level(int world, int level, int humans, string levelName)
    {
        WorldNum = world;
        LevelNum = level;
        HumansPresent = humans;
        LevelName = levelName;

        // Add zeroes to LevelString if necessary
        string worldStr = (world < 10 ? "0" : "") + world.ToString();
        string levelStr = (level < 10 ? "0" : "") + level.ToString();
        LevelString = $"Level-W{worldStr}-L{levelStr}";
    }
}