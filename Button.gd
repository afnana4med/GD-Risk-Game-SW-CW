extends Button

var popup_menu

func _ready():
	# Assuming PopupMenu is a direct child of the Control node
	popup_menu = $PopupMenu
	popup_menu.hide()  # Hide the popup menu initially

func _on_Button_pressed():
	if popup_menu.visible:
		popup_menu.hide()
	else:
		# Position the PopupMenu below the button
		
		popup_menu.show()

func _on_PopupMenu_id_pressed(id):
	match id:
		0:  # Human
			print("Human selected")
			# Add your code here to handle when "Human" is selected
		1:  # AI Agent
			print("AI Agent selected")
			# Add your code here to handle when "AI Agent" is selected
		# Add more cases for additional options as needed



