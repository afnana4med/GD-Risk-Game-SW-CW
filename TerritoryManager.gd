# TerritoryManager.gd
extends Node

@export var game_controller: Node
signal territory_clicked(continent: String, country: String)


func emmit_territory_clicked(continent: String, country: String):
	territory_clicked.emit(continent, country)
	_on_territory_pressed(continent, country)
	
#func update_map_display(territories):
	## This function updates the map display based on the current army count in each territory
		#for territory_name in territories:
			#var button = $continents.get_node(territory_name)  # Adjust path as necessary
			#var army_count = territories[territory_name]["armies"]
			#button.get_node("ArmyCountLabel").text = str(army_count)

func _on_territory_pressed(continent: String, country: String):
	var current_player = game_controller.CurrentPlayer
	var current_action = game_controller.CurrentAction  # Ensure this property is exposed in GameController

	match current_action:
		"deploy":
			deploy_armies(current_player, country, 1)  # Assuming 1 army for simplicity
		"attack":
			attempt_attack(current_player, country)
		"fortify":
			begin_fortification(current_player, country)
		"move":
			move_armies(current_player, country)
		_:
			print("Unhandled action type")

func deploy_armies(player, territory_name, number_of_armies):
	print("Deploying %d armies to %s by %s" % [number_of_armies, territory_name, player.name])

func attempt_attack(player, territory_name):
	print("%s is attacking %s" % [player.name, territory_name])

func begin_fortification(player, territory_name):
	print("%s is fortifying %s" % [player.name, territory_name])

func move_armies(player, territory_name):
	print("%s is moving armies to %s" % [player.name, territory_name])
