using Godot;
using System.Collections.Generic;

public partial class DropDownMenu : OptionButton
{
	// Initialize a list for the dropdown options
	private List<string> playerOptions = new List<string>
	{
		"Human", // Indicates a human player
		"Bot", // Indicates an AI-controlled player
		"None" // Indicates the player slot is not in use
	};

	public override void _Ready()
	{
		// Populate the dropdown with options
		foreach (var option in playerOptions)
		{
			AddItem(option);
		}

		// Connect the 'item_selected' signal to the '_on_item_selected' method
		var callable = new Callable(this, nameof(_on_item_selected));
		Connect("item_selected", callable);
	}

	private void _on_item_selected(int index)
	{
		GD.Print($"Player option selected: {GetItemText(index)}");
	}	
}
