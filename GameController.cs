using System;
using System.Collections.Generic;
using System.Linq;
using Godot;


namespace Practice.GD_Risk_Game_SW_CW;

public partial class GameController : Node
{
	public List<Player> Players { get; private set; } = new List<Player>();
	public Graph gameBoard { get; private set; } = new Graph();
	private int currentPlayerIndex = 0;

	public enum GameState
	{
		Deploying,
		Attacking,
		Fortifying,
		WaitingForTurn
	}

	private GameState currentState;
	private Random random = new Random();

	public Player CurrentPlayer => Players[currentPlayerIndex];

	public override void _Ready()
	{
		InitializePlayers(3); // For a three-player game
		InitializeGameBoard();
		// InitialRandomDeployment();
		// StartTurn(); //after deployment of all armies
		// //currentState = GameState.Deploying; // Start with deploying troops
		// //deployTroops(); // Start game by deploying troops
	}
	

	private void InitializePlayers(int numberOfPlayers)
	{
		Dictionary<int, int> initialArmiesPerPlayer = new Dictionary<int, int>
		{
			{ 2, 40 }, { 3, 35 }, { 4, 30 }, { 5, 25 }, { 6, 20 }
		};
		int initialArmies = initialArmiesPerPlayer[numberOfPlayers];
		for (int i = 0; i < numberOfPlayers; i++)
		{
			var player = new Player($"Player {i + 1}", initialArmies);
			player.Infantry = initialArmies;
			Players.Add(player);
		}
	}

	private void InitializeGameBoard()
	{
		// Step 1: Declare and Initialize Territories
		var territory1 = new Territory("North America - Alaska");
		var territory2 = new Territory("North America - NorthWestTerritory");
		var territory3 = new Territory("North America - Alberta");
		var territory4 = new Territory("North America - Ontario");
		var territory5 = new Territory("North America - WesternUnitedStates");
		var territory6 = new Territory("North America - EasternUnitedStates");
		var territory7 = new Territory("North America - Quebec");
		var territory8 = new Territory("North America - Central America");
		var territory9 = new Territory("North America - Greenland");

		var territory10 = new Territory("South America - Venezulea");
		var territory11 = new Territory("South America - Peru");
		var territory12 = new Territory("South America - Argentina");
		var territory13 = new Territory("South America - Brazil");

		var territory14 = new Territory("Europe - Iceland");
		var territory15 = new Territory("Europe - GreatBritian");
		var territory16 = new Territory("Europe - Scandinavia");
		var territory17 = new Territory("Europe - NorthernEurope");
		var territory18 = new Territory("Europe - WesternEurope");
		var territory19 = new Territory("Europe - SouthernEurope");
		var territory20 = new Territory("Europe - Ukraine");

		var territory21 = new Territory("Africa - Madagascar");
		var territory22 = new Territory("Africa - Egypt");
		var territory23 = new Territory("Africa - EastAfrica");
		var territory24 = new Territory("Africa - NorthAfrica");
		var territory25 = new Territory("Africa - Congo");
		var territory26 = new Territory("Africa - SouthAfrica");

		var territory27 = new Territory("Asia - Ural");
		var territory28 = new Territory("Asia - Afghanistan");
		var territory29 = new Territory("Asia - Siberia");
		var territory30 = new Territory("Asia - Yakutsk");
		var territory31 = new Territory("Asia - Kamchatka");
		var territory32 = new Territory("Asia - Irkutsk");
		var territory33 = new Territory("Asia - China");
		var territory34 = new Territory("Asia - Mongolia");
		var territory35 = new Territory("Asia - Japan");
		var territory36 = new Territory("Asia - Siam");
		var territory37 = new Territory("Asia - India");
		var territory38 = new Territory("Asia - MiddleEast");

		var territory39 = new Territory("Australia - Indonesia");
		var territory40 = new Territory("Australia - NewGuinea");
		var territory41 = new Territory("Australia - EasternAustralia");
		var territory42 = new Territory("Australia - WesternAustralia");
		
		// Add territories to the graph
		gameBoard.AddTerritory(territory1);
		gameBoard.AddTerritory(territory2);
		gameBoard.AddTerritory(territory3);
		gameBoard.AddTerritory(territory4);
		gameBoard.AddTerritory(territory5);
		gameBoard.AddTerritory(territory6);
		gameBoard.AddTerritory(territory7);
		gameBoard.AddTerritory(territory8);
		gameBoard.AddTerritory(territory9);
		gameBoard.AddTerritory(territory10);
		gameBoard.AddTerritory(territory11);
		gameBoard.AddTerritory(territory12);
		gameBoard.AddTerritory(territory13);
		gameBoard.AddTerritory(territory14);
		gameBoard.AddTerritory(territory15);
		gameBoard.AddTerritory(territory16);
		gameBoard.AddTerritory(territory17);
		gameBoard.AddTerritory(territory18);
		gameBoard.AddTerritory(territory19);
		gameBoard.AddTerritory(territory20);
		gameBoard.AddTerritory(territory21);
		gameBoard.AddTerritory(territory22);
		gameBoard.AddTerritory(territory23);
		gameBoard.AddTerritory(territory24);
		gameBoard.AddTerritory(territory25);
		gameBoard.AddTerritory(territory26);
		gameBoard.AddTerritory(territory27);
		gameBoard.AddTerritory(territory28);
		gameBoard.AddTerritory(territory29);
		gameBoard.AddTerritory(territory30);
		gameBoard.AddTerritory(territory31);
		gameBoard.AddTerritory(territory32);
		gameBoard.AddTerritory(territory33);
		gameBoard.AddTerritory(territory34);
		gameBoard.AddTerritory(territory35);
		gameBoard.AddTerritory(territory36);
		gameBoard.AddTerritory(territory37);
		gameBoard.AddTerritory(territory38);
		gameBoard.AddTerritory(territory39);
		gameBoard.AddTerritory(territory40);
		gameBoard.AddTerritory(territory41);
		gameBoard.AddTerritory(territory42);

		// Step 2: Define Adjacency (Edges)
		// North America Connections
		gameBoard.AddEdge(territory1, territory2); // Alaska - Northwest Territory
		gameBoard.AddEdge(territory1, territory3); // Alaska - Alberta
		gameBoard.AddEdge(territory1, territory31); // Alaska - Kamchatka (Asia)
		gameBoard.AddEdge(territory2, territory3); // Northwest Territory - Alberta
		gameBoard.AddEdge(territory2, territory4); // Northwest Territory - Ontario
		gameBoard.AddEdge(territory2, territory9); // Northwest Territory - Greenland
		gameBoard.AddEdge(territory3, territory4); // Alberta - Ontario
		gameBoard.AddEdge(territory3, territory5); // Alberta - Western United States
		gameBoard.AddEdge(territory4, territory5); // Ontario - Western United States
		gameBoard.AddEdge(territory4, territory6); // Ontario - Eastern United States
		gameBoard.AddEdge(territory4, territory7); // Ontario - Quebec
		gameBoard.AddEdge(territory4, territory9); // Ontario - Greenland
		gameBoard.AddEdge(territory5, territory6); // Western United States - Eastern United States
		gameBoard.AddEdge(territory5, territory8); // Western United States - Central America
		gameBoard.AddEdge(territory6, territory7); // Eastern United States - Quebec
		gameBoard.AddEdge(territory6, territory8); // Eastern United States - Central America
		gameBoard.AddEdge(territory7, territory9); // Quebec - Greenland

		// South America Connections
		gameBoard.AddEdge(territory10, territory11); // Venezuela - Peru
		gameBoard.AddEdge(territory10, territory13); // Venezuela - Brazil
		gameBoard.AddEdge(territory10, territory8); // Venezuela - Central America (North America)
		gameBoard.AddEdge(territory11, territory12); // Peru - Argentina
		gameBoard.AddEdge(territory11, territory13); // Peru - Brazil
		gameBoard.AddEdge(territory12, territory13); // Argentina - Brazil
		gameBoard.AddEdge(territory13, territory24); // Brazil - -NorthAfrica

		// Europe Connections
		gameBoard.AddEdge(territory14, territory15); // Iceland - Great Britain
		gameBoard.AddEdge(territory14, territory16); // Iceland - Scandinavia
		gameBoard.AddEdge(territory14, territory9); // Iceland - Greenland (North America)
		gameBoard.AddEdge(territory15, territory16); // Great Britain - Scandinavia
		gameBoard.AddEdge(territory15, territory17); // Great Britain - Northern Europe
		gameBoard.AddEdge(territory15, territory18); // Great Britain - Western Europe
		gameBoard.AddEdge(territory16, territory17); // Scandinavia - Northern Europe
		gameBoard.AddEdge(territory16, territory20); // Scandinavia - Ukraine
		gameBoard.AddEdge(territory17, territory18); // Northern Europe - Western Europe
		gameBoard.AddEdge(territory17, territory19); // Northern Europe - Southern Europe
		gameBoard.AddEdge(territory17, territory20); // Northern Europe - Ukraine
		gameBoard.AddEdge(territory18, territory19); // Western Europe - Southern Europe
		gameBoard.AddEdge(territory19, territory20); // Southern Europe - Ukraine
		gameBoard.AddEdge(territory19, territory22); // Southern Europe - Egypt (Africa)
		gameBoard.AddEdge(territory19, territory38); // Southern Europe - Middle East (Asia)
		gameBoard.AddEdge(territory20, territory38); // Ukraine - Middle East (Asia)


		// Asia Connections
		gameBoard.AddEdge(territory27, territory28); // Ural - Afghanistan
		gameBoard.AddEdge(territory27, territory29); // Ural - Siberia
		gameBoard.AddEdge(territory27, territory33); // Ural - China
		gameBoard.AddEdge(territory28, territory33); // Afghanistan - China
		gameBoard.AddEdge(territory28, territory37); // Afghanistan - India
		gameBoard.AddEdge(territory28, territory38); // Afghanistan - Middle East
		gameBoard.AddEdge(territory29, territory30); // Siberia - Yakutsk
		gameBoard.AddEdge(territory29, territory32); // Siberia - Irkutsk
		gameBoard.AddEdge(territory29, territory34); // Siberia - Mongolia
		gameBoard.AddEdge(territory29, territory33); // Siberia - China
		gameBoard.AddEdge(territory30, territory31); // Yakutsk - Kamchatka
		gameBoard.AddEdge(territory30, territory32); // Yakutsk - Irkutsk
		gameBoard.AddEdge(territory31, territory32); // Kamchatka - Irkutsk
		gameBoard.AddEdge(territory31, territory34); // Kamchatka - Mongolia
		gameBoard.AddEdge(territory31, territory35); // Kamchatka - Japan
		gameBoard.AddEdge(territory32, territory34); // Irkutsk - Mongolia
		gameBoard.AddEdge(territory33, territory34); // China - Mongolia
		gameBoard.AddEdge(territory33, territory36); // China - Siam
		gameBoard.AddEdge(territory34, territory35); // Mongolia - Japan
		gameBoard.AddEdge(territory36, territory37); // Siam - India
		gameBoard.AddEdge(territory36, territory39); // Siam - Indonesia

		// Africa Connections
		gameBoard.AddEdge(territory22, territory23); // Egypt - East Africa
		gameBoard.AddEdge(territory22, territory24); // Egypt - North Africa
		gameBoard.AddEdge(territory22, territory38); // Egypt - Middle East (Asia)
		gameBoard.AddEdge(territory23, territory24); // East Africa - North Africa
		gameBoard.AddEdge(territory23, territory25); // East Africa - Congo
		gameBoard.AddEdge(territory23, territory26); // East Africa - South Africa
		gameBoard.AddEdge(territory23, territory21); // East Africa - Madagascar
		gameBoard.AddEdge(territory24, territory25); // North Africa - Congo
		gameBoard.AddEdge(territory25, territory26); // Congo - South Africa



		// Australia Connections
		gameBoard.AddEdge(territory39, territory40); // Indonesia - New Guinea
		gameBoard.AddEdge(territory39, territory42); // Indonesia - Western Australia
		gameBoard.AddEdge(territory39, territory36); // Indonesia - Siam (Asia)

		gameBoard.AddEdge(territory40, territory41); // New Guinea - Eastern Australia
		gameBoard.AddEdge(territory40, territory42); // New Guinea - Western Australia
		gameBoard.AddEdge(territory40, territory39); // New Guinea - Indonesia
		gameBoard.AddEdge(territory42, territory41); // Western Australia - Eastern Australia
		gameBoard.AddEdge(territory42, territory39); // Western Australia - Indonesia
		
		// GD.Print($"Total territories after initialization: {gameBoard.Territories.Count}");
	}

	// Randomly assign initial armies to each player's territory
	private void InitialRandomDeployment()
	{
		GD.Print("\n"+$"Starting deployment. Total players: {Players.Count}, Total territories: {gameBoard.Territories.Count}" + "\n");
		var unclaimedTerritories = gameBoard.Territories.Values.ToList();

		// Phase 1: Claim all territories
		while (unclaimedTerritories.Any())
		{
			foreach (var player in Players)
			{
				if (!unclaimedTerritories.Any()) break;
				var territory = unclaimedTerritories[random.Next(unclaimedTerritories.Count)];
				territory.Owner = player;
				player.AddTerritory(territory);
				territory.Armies = 1;  // Assign 1 army initially when claiming
				player.Infantry--;
				unclaimedTerritories.Remove(territory);

				// Print the initial assignment of each territory
				GD.Print($"{player.Name} has claimed {territory.Name} with 1 army.");
			}
		}

		// Phase 2: Place remaining armies
		foreach (var player in Players)
		{
			while (player.Infantry > 0)
			{
				var randomTerritory = player.Territories[random.Next(player.Territories.Count)];
				randomTerritory.Armies++;
				player.Infantry--;

				// Print the distribution of each army
				GD.Print($"{player.Name} places an additional army in {randomTerritory.Name}. Total now: {randomTerritory.Armies}");
			}
		}

		PrintInitialSetup();
	}

	private void PrintInitialSetup()
	{
		GD.Print("Initial setup complete. Here are the territory assignments and army placements:");
		foreach (var player in Players)
		{
			GD.Print($"{player.Name} has the following territories with armies:");
			foreach (var territory in player.Territories)
			{
				GD.Print($"  - {territory.Name} with {territory.Armies} armies.");  // Utilizes the Territory's ToString method
			}
		}
	}

	
	
	
	
	
	
	private int CalculateCardBonus(Player player) {
		// Placeholder for card trading logic
		return 0; // Placeholder return value
	}

	private void PrintPlayerArmies() {
		GD.Print("Current infantry distribution:");
		foreach (var player in Players) {
			GD.Print($"{player.Name} has {player.Infantry} infantry.");
		}
	}
	

// Method to handle card trades, which could be part of the player's turn actions
	private void HandleCardTrades(Player player) {
		// Logic to determine if a set can be traded in and calculate bonus
	}


	private void StartTurn()
	{
		//GD.Print("Starting turn for " + CurrentPlayer.Name);
		GD.Print("");
		CalculateNewArmies(CurrentPlayer);
		// GD.Print(CurrentPlayer.Name + " receives " + CurrentPlayer.Infantry +
		//          " new troops. Total available for deployment: " + CurrentPlayer.Infantry);
		// HandleCardTrades(CurrentPlayer); // Handle card trades at the start of the turn
		// PrintPlayerArmies();
		// PromptPlayerDeployment(CurrentPlayer);
	}
	
	
	private void CalculateNewArmies(Player player) {
		// 1. Calculate armies from territories
		int territoryCount = player.Territories.Count;
		int newArmiesFromTerritories = Math.Max(territoryCount / 3, 3); // Ensure minimum 3 armies

		// 2. Calculate armies from continent control
		int newArmiesFromContinents = CalculateContinentBonus(player);

		// 3. Calculate armies from trading in cards
		int newArmiesFromCards = CalculateCardBonus(player);

		// Sum up all new armies
		int totalNewArmies = newArmiesFromTerritories + newArmiesFromContinents + newArmiesFromCards;
		player.Infantry += totalNewArmies;

		GD.Print($"{player.Name} receives {totalNewArmies} new troops. Total available for deployment: {player.Infantry}");
		
		PrintInitialSetup();
	}
	
	private Dictionary<string, Player> territoryOwners = new Dictionary<string, Player>();
	
	private Dictionary<string, int> continentValues = new Dictionary<string, int> {
		{"North America", 5},
		{"South America", 2},
		{"Europe", 5},
		{"Africa", 3},
		{"Asia", 7},
		{"Australia", 2}
	};

	private Dictionary<string, List<string>> continentTerritories = new Dictionary<string, List<string>>() {
		{"North America", new List<string>{"Alaska", "Alberta", "Central America", "Eastern United States", "Greenland", "Northwest Territory", "Ontario", "Quebec", "Western United States"}},
		{"South America", new List<string>{"Argentina", "Brazil", "Peru", "Venezuela"}},
		{"Europe", new List<string>{"Great Britain", "Iceland", "Northern Europe", "Scandinavia", "Southern Europe", "Ukraine", "Western Europe"}},
		{"Africa", new List<string>{"Congo", "East Africa", "Egypt", "Madagascar", "North Africa", "South Africa"}},
		{"Asia", new List<string>{"Afghanistan", "China", "India", "Irkutsk", "Japan", "Kamchatka", "Middle East", "Mongolia", "Siam", "Siberia", "Ural", "Yakutsk"}},
		{"Australia", new List<string>{"Eastern Australia", "Indonesia", "New Guinea", "Western Australia"}}
	};

	
	private int CalculateContinentBonus(Player player) {
		if (player == null) {
			GD.Print("Error: Player object is null.");
			return 0;
		}

		int continentBonus = 0;
		foreach (var continent in continentValues.Keys) {
			if (DoesPlayerControlContinent(player, continent)) {
				continentBonus += continentValues[continent];
			}
		}
		return continentBonus;
	}
	
	private bool DoesPlayerControlContinent(Player player, string continent) {
		if (continentTerritories == null || territoryOwners == null) {
			GD.Print("Error: Data structures are not initialized.");
			return false;
		}

		foreach (var territory in continentTerritories[continent]) {
			if (!territoryOwners.ContainsKey(territory) || territoryOwners[territory] != player) {
				return false; // As soon as one territory is found not controlled by the player, return false
			}
		}
		return true; // Player controls all territories in the continent
	}
	
	/// <summary>
	/// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	/// </summary>
	/// <param name="player"></param>

	private void PromptPlayerDeployment(Player player) {
		// This would interact with the UI to allow the player to deploy their troops
		// Depending on your UI setup, you might use signals or direct method calls here
	}
	
	public void PlayerControlledDeployment(Player player, Territory territory, int armyCount) {
		if (player.Infantry >= armyCount) {
			territory.Armies += armyCount;
			player.Infantry -= armyCount;
			GD.Print($"{player.Name} has deployed {armyCount} armies to {territory.Name}.");
		} else {
			GD.Print($"Not enough available armies. {player.Name} has only {player.Infantry} available.");
		}
	}
	
	
	
	
	public void deployTroops()
	{
		foreach (var player in Players) {
			var territories = gameBoard.Territories.Values.Where(t => t.Owner == player).ToList();
			int armiesPerTerritory = player.Infantry / territories.Count;
			foreach (var territory in territories) {
				territory.Armies += armiesPerTerritory;
			}
			player.Infantry %= territories.Count;  // Remainder armies to be placed manually or added to specific territories
		}
	}
	
	public void nextPhase()
	{
		currentState = currentState switch {
			GameState.Deploying => GameState.Attacking,
			GameState.Attacking => GameState.Fortifying,
			GameState.Fortifying => GameState.WaitingForTurn,
			GameState.WaitingForTurn => GameState.Deploying,
			_ => currentState
		};
		GD.Print($"Transitioned to {currentState}");
		if (currentState == GameState.Deploying) {
			deployTroops();
		}
	}
	
	
	/// <summary>
	/// /////////////////////////////////////////////////////////Attacking////////////////////////////////////////////////////////////////////////////////////////////////
	/// </summary>

private void PromptForContinuedAttack()
{
    // Display UI prompt asking the player if they want to continue attacking.
    // The player’s response will determine the next action.
    // For example, you might have a modal dialogue with "Attack Again" and "End Attacks" buttons.
}

// Example callback method for the "Attack Again" button
private void OnAttackAgainSelected()
{
    // The player has chosen to attack again.
    // Show UI for selecting the next attacking and defending territories.
}

public Territory GetTerritoryByName(string name)
{
	if (gameBoard.Territories.ContainsKey(name))
		return gameBoard.Territories[name];
	return null;
}

// resolve attcak
public void ResolveAttack(Territory attackingTerritory, Territory defendingTerritory)
{
    if (!IsValidAttack(attackingTerritory, defendingTerritory))
    {
        GD.Print("Invalid attack. The territories are not adjacent or the attacking territory does not have at least two armies.");
        return;
    }

    // Announce attack
    GD.Print($"{attackingTerritory.Owner.Name} is attacking {defendingTerritory.Name} from {attackingTerritory.Name}");

    // Determine the number of dice
    int attackerDiceCount = attackingTerritory.Owner.DecideNumberOfDiceToRoll(attackingTerritory);
    int defenderDiceCount = defendingTerritory.Owner.DecideNumberOfDiceToRoll(defendingTerritory);

    // Roll the dice
    List<int> attackerDice = attackingTerritory.Owner.RollDice(attackerDiceCount).OrderByDescending(d => d).ToList();
    List<int> defenderDice = defendingTerritory.Owner.RollDice(defenderDiceCount).OrderByDescending(d => d).ToList();

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
    
    GD.Print($"Battle results: {attackingTerritory.Owner.Name} lost {attackerLosses} armies. {defendingTerritory.Owner.Name} lost {defenderLosses} armies.");

    // If the territory is captured
    if (defendingTerritory.Armies == 0)
    {
        CaptureTerritory(attackingTerritory, defendingTerritory, attackerDiceCount, attackerLosses);
    }
    else
    {
	    GD.Print($"{defendingTerritory.Owner.Name} successfully defended {defendingTerritory.Name}."); 
    }

    // Check if the attacker wants to continue
    PromptForContinuedAttack();
}

private void PrintArmiesAfterAttack(Player attacker, Player defender)
{
	GD.Print($"{attacker.Name}'s armies:");
	foreach (var territory in attacker.Territories)
	{
		GD.Print($"  - {territory.Name} has {territory.Armies} armies.");
	}
    
	GD.Print($"{defender.Name}'s armies:");
	foreach (var territory in defender.Territories)
	{
		GD.Print($"  - {territory.Name} has {territory.Armies} armies.");
	}
}

// Add this method to the GameController class
private void CaptureTerritory(Territory attackingTerritory, Territory defendingTerritory, int diceRolled, int attackerLosses)
{
    Player attacker = attackingTerritory.Owner;
    Player defender = defendingTerritory.Owner;

    defender.RemoveTerritory(defendingTerritory);
    attacker.AddTerritory(defendingTerritory);
    int armiesToMove = Math.Max(diceRolled, attackerLosses); // As per rules, move in at least as many armies as the number of dice rolled
    attackingTerritory.Armies -= armiesToMove;
    defendingTerritory.Armies = armiesToMove;

    GD.Print($"{attacker.Name} has captured {defendingTerritory.Name} and moved {armiesToMove} armies.");
    GD.Print($"{attackingTerritory.Owner.Name} captured {defendingTerritory.Name}.");
    GD.Print($"{attackingTerritory.Name} now has {attackingTerritory.Armies} armies after moving {armiesToMove} armies to {defendingTerritory.Name}.");
    GD.Print($"{defendingTerritory.Name} now has {defendingTerritory.Armies} armies.");
}

// Update this existing method in GameController to include additional attack logic
private bool IsValidAttack(Territory attackingTerritory, Territory defendingTerritory)
{
    // Check adjacency and if the attacking territory has enough armies to attack
    return gameBoard.AreTerritoriesAdjacent(attackingTerritory, defendingTerritory) && attackingTerritory.Armies > 1;
}






/// <summary>
/// /////////////////////////////////////////////////////////////////END ATTACK////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>

	
public void endAttack()
	{
		GD.Print("Ending attack phase.");
		currentState = GameState.Fortifying;  // Transition to the fortifying phase
		GD.Print("Transitioned to fortifying phase. You can now move armies to strengthen your defenses.");
		// Call any necessary methods to update the game state or UI here if needed
	}

	public void Fortify()
	{
		GD.Print("FORTIFY!");
	}

	public void endTurn()
	{
		currentPlayerIndex = (currentPlayerIndex + 1) % Players.Count;
		
		currentState = GameState.Deploying;
		GD.Print($"Turn ended. Now it's {CurrentPlayer.Name}'s turn.");
		deployTroops();
	}

	public void cardTrade()
	{
		GD.Print("EXCHANGE CARD!");
	}

	

	

	
	
}


// Make sure to call InitializePlayers(numberOfPlayers) at the appropriate point in your game setup.



