extends RigidBody2D 
signal hit
const PUSH_FORCE = 6000
var forcePos = Vector2(0,0)
var velocity = Vector2(100,50)
var forceVectorRight = Vector2(PUSH_FORCE,0)
var forceVectorLeft = Vector2(-PUSH_FORCE, 0)
var forceVectorUp = Vector2(0,-PUSH_FORCE)
var forceVectorDown = Vector2(0,PUSH_FORCE)
var currentSlowTimeScale = 0.5
var boosterSound = null

var insideCollision = false
var recoveringCollision = false

var mouseButtonClicked = false
var fireImpulse = false
var impulseDirection = "" 
var max_speed = 100
#impulseDirection should be left,right,up,down

signal slowingDown
signal speedingUp

func _input(event):
	# Mouse in viewport coordinates
	if event is InputEventMouseButton:
		#mouse motion code here
		mouseButtonClicked = true

		pass
	elif event is InputEventMouseMotion:
		var maxX = get_viewport_rect().size.x;
		if event.position.x <= 20:
			get_viewport().warp_mouse(Vector2(maxX - 25, event.position.y))

		if event.position.x >= maxX - 20:
			get_viewport().warp_mouse(Vector2(25, event.position.y))

func _ready():
	# Called every time the node is added to the scene.
	# Initialization here
	
	boosterSound = self.get_parent().get_node("BoostSound")
	
	currentSlowTimeScale = 0.5
	pass

func _physics_process(delta):
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.'
	
	var boardRotation = get_node("../Board").get("saveRotation")
	
	forceVectorRight = Vector2(PUSH_FORCE*cos(boardRotation*PI/180), PUSH_FORCE*sin(boardRotation*PI/180))
	forceVectorLeft = Vector2(-PUSH_FORCE*cos(boardRotation*PI/180), -PUSH_FORCE*sin(boardRotation*PI/180))
	forceVectorUp = Vector2(PUSH_FORCE*sin(boardRotation*PI/180),-PUSH_FORCE*cos(boardRotation*PI/180))
	forceVectorDown = Vector2(-PUSH_FORCE*sin(boardRotation*PI/180),PUSH_FORCE*cos(boardRotation*PI/180))
	
	if abs(velocity.x) > max_speed or abs(velocity.y) > max_speed:
		var new_speed = velocity.normalized()
		new_speed *= max_speed
		velocity = new_speed #set_linear_velocity(new_speed)

	if insideCollision:
		_slowTime(delta)
	if recoveringCollision:
		_speedTime(delta)
	if fireImpulse and mouseButtonClicked:
		_applyImpulse(impulseDirection)
	mouseButtonClicked = false


	pass

func _applyImpulse(direction):
	if direction == "right":
		self.set_axis_velocity(velocity)
		apply_impulse(forcePos, forceVectorRight)
	elif direction == "left":
		self.set_axis_velocity(velocity)
		apply_impulse(forcePos, forceVectorLeft)
	elif direction == "up":
		apply_impulse(forcePos, forceVectorUp)
	elif direction == "down":
		apply_impulse(forcePos, forceVectorDown)
	else: 
		print("error")
	fireImpulse = false
	mouseButtonClicked = false
	recoveringCollision = true
	insideCollision = false
	boosterSound.play()
	pass


func _speedTime(delta):
#	currentSlowTimeScale += (1 * delta)
#	if currentSlowTimeScale >= 1:
	#	recoveringCollision = false
	currentSlowTimeScale = 1
	Engine.time_scale = currentSlowTimeScale
	emit_signal("speedingUp")
	pass
	
func _slowTime(delta):
	currentSlowTimeScale -= (5*delta)#(3.5 * delta)
	if currentSlowTimeScale < 0.1:#< 0.005:
		currentSlowTimeScale =0.1#= .005

	Engine.time_scale = currentSlowTimeScale
	#print("inSlowMo")
	emit_signal("slowingDown")
	pass


func _on_Area2D_body_entered_Right( body ):
	#if(Input.is_mouse_button_pressed(BUTTON_LEFT)) :
	insideCollision = true
	recoveringCollision = false


	if body.get_name() == "Ball":
		fireImpulse = true
		impulseDirection = "right"


func _on_Area2D_body_entered_Left( body ):
	insideCollision = true
	recoveringCollision = false

	if body.get_name() == "Ball":
		fireImpulse = true
		impulseDirection = "left"


func _on_Area2D_body_entered_Up( body ):
	insideCollision = true
	recoveringCollision = false
	if body.get_name() == "Ball":
		fireImpulse = true
		impulseDirection = "up"


func _on_Area2D_body_entered_Down( body ):
	insideCollision = true
	recoveringCollision = false
	if body.get_name() == "Ball":
		fireImpulse = true
		impulseDirection = "down"


func _on_goDown_body_exited( body ):
	Engine.time_scale = 1
	insideCollision = false
	recoveringCollision = true
	pass # replace with function body



func _on_goUp_body_exited( body ):
	Engine.time_scale = 1
	insideCollision = false
	recoveringCollision = true
	pass # replace with function body


func _on_goRight_body_exited( body ):
	Engine.time_scale = 1
	insideCollision = false
	recoveringCollision = true
	pass # replace with function body


func _on_goLeft_body_exited( body ):
	Engine.time_scale = 1
	insideCollision = false
	recoveringCollision = true
	pass # replace with function body
