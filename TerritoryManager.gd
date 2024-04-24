
extends Node2D 
# Declare these variables at the top of your script, outside any function.
var selected_attacking_territory = null
var selected_defending_territory = null
var last_selected_button = null


 # Ensure your script extends a proper node type if needed.

# Make sure the game controller is correctly referenced.
@export var game_controller: Node2D
# Declare the variables at the class level as shown previously.


func _on_TerritoryButton_Pressed(territory_button: TextureButton):
	print("Button pressed: ", territory_button.name)  # Confirm function is called.
	var territory_name = territory_button.name
	var territory = game_controller.GetTerritoryByName(territory_name)
	
	if territory == null:
		print("Territory not found: ", territory_name)
		return

	print("Territory owner: ", territory.owner, " Current Player: ", game_controller.CurrentPlayer)
	
	if selected_attacking_territory == null and territory.owner == game_controller.CurrentPlayer:
		if last_selected_button:
			last_selected_button.modulate = Color.WHITE
		selected_attacking_territory = territory
		territory_button.modulate = Color.RED
		last_selected_button = territory_button
		print("Selected attacking territory: ", territory.name)
	elif selected_attacking_territory != null and territory.owner != game_controller.CurrentPlayer:
		selected_defending_territory = territory
		territory_button.modulate = Color.BLUE
		print("Selected defending territory: ", territory.name)
	elif selected_attacking_territory != null and territory == selected_attacking_territory:
		reset_selection()

func reset_selection():
	if last_selected_button:
		last_selected_button.modulate = Color.WHITE
	selected_attacking_territory = null
	selected_defending_territory = null
	last_selected_button = null
	print("Selections have been reset.")

