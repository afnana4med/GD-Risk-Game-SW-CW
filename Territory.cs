namespace Practice.GD_Risk_Game_SW_CW;
using System.Collections.Generic;

// Territory.cs
public class Territory
{
    public string Name { get; private set; }
    public Player Occupant { get; set; }
    public int Armies { get; set; }

    public Territory(string name)
    {
        Name = name;
    }
}

// Graph.cs


public class Graph
{
    public Dictionary<string, Territory> Territories { get; private set; }
    public Dictionary<Territory, List<Territory>> Adjacencies { get; private set; }

    public Graph()
    {
        Territories = new Dictionary<string, Territory>();
        Adjacencies = new Dictionary<Territory, List<Territory>>();
    }

    public void AddTerritory(Territory territory)
    {
        Territories[territory.Name] = territory;
        Adjacencies[territory] = new List<Territory>();
    }

    public void AddEdge(Territory from, Territory to)
    {
        if (Adjacencies.ContainsKey(from) && Adjacencies.ContainsKey(to))
        {
            Adjacencies[from].Add(to);
            Adjacencies[to].Add(from);
        }
    }
}
