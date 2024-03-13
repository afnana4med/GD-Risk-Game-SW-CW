extends OptionButton

func _ready():
	# Set the items for the OptionButton
	add_item("Human")
	add_item("AI agent")

	# Connect the _on_item_selected function to handle selection
	

func _on_item_selected(index):
	match index:
		0:
			print("Selected: Human")
			# Handle Human selection here
		1:
			print("Selected: AI agent")
			# Handle AI agent selection here
