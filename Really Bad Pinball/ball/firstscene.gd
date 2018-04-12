extends Node2D

var nextScene = null

func _ready():
	nextScene = get_tree().get_root().get_child(get_tree().get_root().get_child_count()-1)
	

func _input(event):
	# Mouse in viewport coordinates
	if event is InputEventMouseButton:
		nextScene.queue_free()
		var s = load("ballWorld.tscn")
		nextScene = s.instance()
		get_tree().get_root().add_child(nextScene)
