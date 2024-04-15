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


