using System;
using System.Collections.Generic;
using System.Linq;
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

    public Player()
    {
        throw new NotImplementedException();
    }

    private void UpdateTotalArmies()
    {
        TotalArmies = Infantry + Cavalry * 5 + Artillery * 10; // Example conversion rates
    }

    public void AddTerritory(Territory territory)
    {
        if (!Territories.Contains(territory))
        {
            Territories.Add(territory);
            territory.Owner = this; // Set this player as the owner of the territory
        }
    }

    public void RemoveTerritory(Territory territory)
    {
        if (Territories.Contains(territory))
        {
            Territories.Remove(territory);
            territory.Owner = null; // Remove ownership of the territory
        }
    }
    
    public List<int> RollDice(int numberOfDice)
    {
        List<int> results = new List<int>();
        Random random = new Random();
        for (int i = 0; i < numberOfDice; i++)
        {
            results.Add(random.Next(1, 7));  // Rolling a six-sided die
        }
        return results;
    }


    // Method to handle the outcome of a battle where 'this' is the attacker
    public void resolveAttack(Territory attackingTerritory, Territory defendingTerritory)
    {
        Player attacker = attackingTerritory.Owner;
        Player defender = defendingTerritory.Owner;

        // Ensure there are enough armies to attack and defend
        if (attackingTerritory.Armies < 2)
        {
            GD.Print("You need at least two armies to attack from a territory.");
            return;
        }

        // Determine the number of dice each side can roll
        int attackDiceCount = Math.Min(3, attackingTerritory.Armies - 1);
        int defendDiceCount = Math.Min(2, defendingTerritory.Armies);

        // Roll the dice
        List<int> attackerDice = attacker.RollDice(attackDiceCount).OrderByDescending(d => d).ToList();
        List<int> defenderDice = defender.RollDice(defendDiceCount).OrderByDescending(d => d).ToList();

        // Compare the dice and determine losses
        int attackerLosses = 0, defenderLosses = 0;
        int comparisons = Math.Min(attackerDice.Count, defenderDice.Count);
        for (int i = 0; i < comparisons; i++)
        {
            if (attackerDice[i] > defenderDice[i])
            {
                defenderLosses++;
            }
            else
            {
                attackerLosses++; // Ties go to the defender
            }
        }

        // Apply the results
        attackingTerritory.Armies -= attackerLosses;
        defendingTerritory.Armies -= defenderLosses;

        // If the territory is captured
        if (defendingTerritory.Armies == 0)
        {
            defender.RemoveTerritory(defendingTerritory);
            attacker.AddTerritory(defendingTerritory);
            int armiesToMove = Math.Max(attackDiceCount, attackerLosses); // As per rules, move in at least as many armies as the number of dice rolled
            attackingTerritory.Armies -= armiesToMove;
            defendingTerritory.Armies = armiesToMove;
        }

        // Check if the attack phase should continue or if the attacker decides to stop
        // This can be decided through UI interaction or player input
        this.UpdateTotalArmies();
        defender.UpdateTotalArmies();
    }

    
    



    // More methods can be added to handle army movements, battles, etc.
}