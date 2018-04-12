extends Camera2D
# class member variables go here, for example:
# var a = 2
# var b = "textvar"
var zoomingIn = false
var zoomingOut = false

func _ready():
	# Called every time the node is added to the scene.
	# Initialization here
	zoom.y = 2.0
	zoom.x = 2.0
	pass

func _process(delta):
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.



	if zoomingIn:
		_zoomIn(delta)
		
	if zoomingOut:
		_zoomOut(delta)
	pass
	
func _zoomIn(delta):
	zoom.y -= .4 * delta
	zoom.x -= .4 * delta
	zoom = Vector2(zoom.x, zoom.y)
	if zoom.y <= .4:
		zoom.y = .4
		zoom.x = .4
		zoomingIn = false
	#print(zoom.y)
	pass
	
func _zoomOut(delta):
	zoom.y += .3 * delta
	zoom.x += .3 * delta
	if zoom.y >= 2.0:
		zoom.y = 2.0
		zoom.x = 2.0
		zoomingOut = false
	pass



func _on_Ball_slowingDown():
	zoomingIn = true
	zoomingOut = false
	pass # replace with function body



func _on_Ball_speedingUp():
	zoomingOut = true
	zoomingIn = false
	pass # replace with function body

