using System.Collections.Generic;
using Godot;

namespace Practice.GD_Risk_Game_SW_CW;

public class Player
{
    public string Name { get; private set; }
    public List<Territory> Territories { get; private set; }
    public Color TerritoryColor { get; set; }
    
    // Specific army types
    private int infantry;
    private int cavalry;
    private int artillery;

    public int Infantry
    {
        get => infantry;
        set { infantry = value; UpdateTotalArmies(); }
    }

    public int Cavalry
    {
        get => cavalry;
        set { cavalry = value; UpdateTotalArmies(); }
    }

    public int Artillery
    {
        get => artillery;
        set { artillery = value; UpdateTotalArmies(); }
    }

    public int TotalArmies { get; private set; } // Total of all army types

    public Player(string name, int initialArmies)
    {
        Name = name;
        Infantry = initialArmies; // Initialize all as infantry for simplicity
        Territories = new List<Territory>();
    }

    private void UpdateTotalArmies()
    {
        TotalArmies = Infantry + Cavalry * 5 + Artillery * 10; // Example conversion rates
    }

    

    // More methods can be added to handle army movements, battles, etc.
}