extends Node2D

# class member variables go here, for example:

var relative=Vector2(0,0)
var relativeX=0.0
export var saveRotation = 0.0



func _ready():
	set_process_input(true)
	pass
	
func _physics_process(delta):
	pass
func _input(event):
	if event is InputEventMouseMotion:
		relative=event.relative
		relativeX=relative.x
		if relativeX<100 and relativeX>-100:
			global_rotation_degrees+=relativeX*0.0125
			saveRotation = global_rotation_degrees

