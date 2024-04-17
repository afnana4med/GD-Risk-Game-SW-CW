using System.Collections.Generic;
using Godot;

namespace Practice.GD_Risk_Game_SW_CW;

public class Player
{
    public string Name { get; private set; }
    public List<Territory> Territories { get; private set; }
    public Color TerritoryColor { get; set; }
    
    public int Armies { get; set; }
    public int Infantry { get; set; }
    public int Cavalry { get; set; }
    public int Artillery { get; set; }

    public Player(string name, int initialArmies )
    
    {
        Name = name;
        Armies = initialArmies;
        Territories = new List<Territory>();
    }
    
    public void AddTerritory(Territory territory) {
        if (!Territories.Contains(territory)) {
            Territories.Add(territory);
            territory.Owner = this;
        }
    }

    public void RemoveTerritory(Territory territory) {
        if (Territories.Contains(territory)) {
            Territories.Remove(territory);
            territory.Owner = null;
        }
    }
    
}