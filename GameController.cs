using System.Collections.Generic;
using Godot;

namespace Practice.GD_Risk_Game_SW_CW;

public partial class GameController : Node {
	public List<Player> Players { get; private set; } = new List<Player>();
	public Graph gameBoard { get; private set; } = new Graph();
	private int currentPlayerIndex = 0;

	public enum GameState { Deploying, Attacking, Fortifying, WaitingForTurn }
	private GameState currentState;

	public Player CurrentPlayer => Players[currentPlayerIndex];

	public override void _Ready() {
		InitializePlayers(3);  // For a three-player game
		InitializeGameBoard();
		currentState = GameState.Deploying; // Start with deploying troops
		deployTroops(); // Start game by deploying troops
	}

	private void InitializePlayers(int numberOfPlayers) {
		Dictionary<int, int> initialArmiesPerPlayer = new Dictionary<int, int> {
			{ 2, 40 }, { 3, 35 }, { 4, 30 }, { 5, 25 }, { 6, 20 }
		};
		int initialArmies = initialArmiesPerPlayer[numberOfPlayers];
		for (int i = 0; i < numberOfPlayers; i++) {
			var player = new Player($"Player {i + 1}", 0);
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
	}
	
	public void NextPhase() {
		currentState = currentState switch {
			GameState.Deploying => GameState.Attacking,
			GameState.Attacking => GameState.Fortifying,
			GameState.Fortifying => GameState.WaitingForTurn,
			GameState.WaitingForTurn => GameState.Deploying,
			_ => currentState
		};
		GD.Print($"Transitioned to {currentState}");
	}
	
	public void EndTurn() {
		currentPlayerIndex = (currentPlayerIndex + 1) % Players.Count;
		GD.Print($"Turn ended. Now it's {CurrentPlayer.Name}'s turn.");
		currentState = GameState.Deploying; // Start each turn by deploying troops
	}
	
	

	public void Attack()
	{
		GD.Print("ATTACK!");
	}

	public void Fortify()
	{
		GD.Print("FORTIFY!");
	}

	public void endTurn()
	{
		GD.Print("END TURN!");
	}

	public void cardTrade()
	{
		GD.Print("EXCHANGE CARD!");
	}

	public void endAttack()
	{
		GD.Print("END ATTACK!");
	}

	public void nextPhase()
	{
		GD.Print("NEXT PHASE");
	}

	public void deployTroops()
	{
		GD.Print($"{CurrentPlayer.Name} is deploying troops.");
	}
	
}


// Make sure to call InitializePlayers(numberOfPlayers) at the appropriate point in your game setup.



