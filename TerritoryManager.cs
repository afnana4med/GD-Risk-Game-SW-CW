
// TerritoryManager.cs
using Godot;
using System.Collections.Generic;
using Practice.GD_Risk_Game_SW_CW;

using Godot;
using System.Collections.Generic;

public class TerritoryManager : Node
{
	[Signal]
	delegate void TerritoryClaimed(string territoryName, string playerName); // Adjusted for Godot's string type compatibility

	private GameController gameController;
    
	public override void _Ready()
	{
		gameController = GetNode<GameController>("/root/GameController");
        
		// Connect the 'pressed' signal of each TextureButton (child) to the OnTerritoryPressed method
		foreach (Node child in GetChildren())
		{
			if (child is TextureButton territoryButton)
			{
				territoryButton.Connect("pressed", this, nameof(OnTerritoryPressed), new Godot.Collections.Array { territoryButton.Name });
			}
		}
	}

	private void OnTerritoryPressed(string territoryName)
	{
		Player currentPlayer = gameController.CurrentPlayer;

		// Find the territory by name
		Territory territory = gameController.FindTerritoryByName(territoryName);

		if (territory != null && territory.Occupant == null)
		{
			// Logic to assign the territory to the current player
			territory.Occupant = currentPlayer;
			currentPlayer.Territories.Add(territory);

			// Emit a signal to update UI or other game components
			EmitSignal(nameof(TerritoryClaimed), territoryName, currentPlayer.Name);

			// Update game state as needed
			gameController.SwitchToNextPlayer();
		}
	}
}



