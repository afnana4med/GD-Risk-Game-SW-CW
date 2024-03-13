extends Node2D

func _ready():
	pass

func _draw():
	draw_dotted_line(Vector2(100, 100), Vector2(300, 300), Color(1, 1, 1), 2, 2)

func draw_dotted_line(from: Vector2, to: Vector2, color: Color, step: int, size: int):
	var length = (to - from).length()
	var segments = int(length / step)
	var direction = (to - from).normalized()
	var step_vec = direction * step

	var current = from
	for i in range(segments):
		draw_line(current, current + direction * size, color, size)
		current += step_vec
