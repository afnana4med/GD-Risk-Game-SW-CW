using System.Collections.Generic;
using Godot;

namespace Practice.GD_Risk_Game_SW_CW;

public class Player
{
    public string Name { get; private set; }
    public List<Territory> Territories { get; private set; }
    public Color TerritoryColor { get; set; }
    public int Infantry { get; set; }
    public int Cavalry { get; set; }
    public int Artillery { get; set; }

    public Player(string name)
    {
        Name = name;
        Territories = new List<Territory>();
    }
}