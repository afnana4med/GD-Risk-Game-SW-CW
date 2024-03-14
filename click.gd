extends Button

var clickCount = 0

func _ready():
	# Set initial display
	update_display()

func _on_pressed():
	# Increment click count
	clickCount += 1
	# Update display
	update_display()


func update_display():
	# Update button text to show click count
	text =str(clickCount)


