extends Area2D

# class member variables go here, for example:
# var a = 2
# var b = "textvar"
export var isCoin = true
var numcoinsCollected = 0

var collectCoinSound = null


func _ready():
	# Called every time the node is added to the scene.
	# Initialization here
	collectCoinSound = self.get_parent().get_parent().get_node("CollectCoinSound")
	pass

func _on_Coin_area_entered( area ):
	print(area.get_name())
	if area.get_name() == "Area2D":
		self.queue_free()
	numcoinsCollected = self.get_parent().get_parent().get("numCollectedCoins")
	numcoinsCollected += 1
	collectCoinSound.play()
	self.get_parent().get_parent().set("numCollectedCoins", numcoinsCollected)