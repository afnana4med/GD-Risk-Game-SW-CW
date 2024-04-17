# TerritoryManager.gd
extends Node

@export var game_controller: Node
signal territory_clicked(continent: String, country: String)

#func _ready():
	# Connect the 'pressed' signal for each TextureButton to the on_territory_pressed method
	#for territory_button in get_children():
		#territory_button.connect("pressed", self, "_on_territory_pressed", [territory_button.name])

func emmit_territory_clicked(continent: String, country: String):
	territory_clicked.emit(continent, country)
	_on_territory_pressed(continent, country)

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
