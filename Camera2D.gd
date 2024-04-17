# Assume this script is attached to a node in the 'Continents' scene
func _ready():
	var canvas_layer_scene = preload("res://canvas_layer.tscn")
	var canvas_layer = canvas_layer_scene.instance()
	add_child(canvas_layer)
