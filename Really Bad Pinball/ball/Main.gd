extends Node

# class member variables go here, for example:
var currentScene = null
var nextScene = null
var myPlayer = null
var gameLevel = GLOBAL.level

var numCoins = 0
var numCollectedCoins = 0
var board = null

func _ready():
	# Called every time the node is added to the scene.
	# Initialization here
	myPlayer = get_node("Ball")
	nextScene=get_tree().get_root().get_child(get_tree().get_root().get_child_count()-1)
	board = get_node("Board")
	for node in board.get_children():
		if not node.get("isCoin") == null:
			numCoins += 1
	print(numCoins)
	set_process(true)
	pass


func _physics_process(delta):
	if(Input.is_action_pressed("Change_Level") and gameLevel == 0):
		_load_scene("level2.tscn")
		GLOBAL.level += 1
	if (myPlayer.global_position.y > 5000):
		_load_scene("ballWorld.tscn")
	if numCollectedCoins >= numCoins and GLOBAL.level == 0:
		_load_scene("level2.tscn")
		GLOBAL.level += 1


func _load_scene(scene):
	nextScene.queue_free()
	var s = load(scene)
	nextScene = s.instance()
	get_tree().get_root().add_child(nextScene)