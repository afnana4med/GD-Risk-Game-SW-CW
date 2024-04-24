
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
    public List<Card> Cards { get; set; } = new List<Card>();
    
    // Specific army types
    private int infantry;
    private int cavalry;
    private int artillery;

    
    public class Card {
        public string Type { get; set; } // e.g., Infantry, Cavalry, Artillery, Wild
        public Territory Territory { get; set; } // Optional, links card to territory
    }
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

    
    
    
    
    public int DecideNumberOfDiceToRoll(Territory territory)
    {
        // Implement logic for the player (or AI) to decide how many dice to roll based on the territory's army count
        // This can involve player input or AI decision making
        return 0;
    }

    public List<int> RollDice(int numberOfDice)
    {
        Random random = new Random();
        List<int> diceRolls = new List<int>();
        for (int i = 0; i < numberOfDice; i++)
        {
            diceRolls.Add(random.Next(1, 7)); // Simulate rolling a six-sided die
        }
        return diceRolls;
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