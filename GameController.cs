using Godot;
using System.Collections.Generic;

using Godot;
using System.Collections.Generic;

public partial class GameController : Node
{
	private List<Player> players = new List<Player>();
	private int currentPlayerIndex = 0;

	public override void _Ready()
	{
		// Initialize players with unique colors
		players.Add(new Player("Player 1", new Color(1, 0, 0))); // Red
		players.Add(new Player("Player 2", new Color(0, 1, 0))); // Green
		players.Add(new Player("Player 3", new Color(0, 0, 1))); // Blue
		players.Add(new Player("Player 4", new Color(1, 1, 0))); // Yellow
		players.Add(new Player("Player 5", new Color(1, 0, 1))); // Magenta
		players.Add(new Player("Player 6", new Color(0, 1, 1))); // Cyan

		UpdatePlayerTurn();
	}

	private void UpdatePlayerTurn()
	{
		// Ensure the game cycles through each player for their turn
		currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
		var currentPlayer = players[currentPlayerIndex];
		GD.Print($"It's now {currentPlayer.ID}'s turn.");

		// Example: Change UI elements to reflect the current player's turn
		// This could include changing text colors or highlighting the player's UI panel
	}

	// Optionally, implement methods to manage game state, like territory control
	public void UpdateTerritoryColor(string territoryId, Color color)
	{
		// Your implementation to change the territory's color to show control
	}
}


public class Player
{
	public string ID { get; private set; }
	public Color Color { get; private set; }

	public Player(string id, Color color)
	{
		ID = id;
		Color = color;
	}
}

