using Godot;
using System;

public partial class AppleTree : Node2D
{
	public string state = "No-Apples"; // Apples, No-Apples
	public bool playerInArea = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Timer growthTimer = GetNode<Timer>("growth-timer");
		growthTimer.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		AnimatedSprite2D treeSprite = GetNode<AnimatedSprite2D>("apple-tree-sprite");
		if (state == "No-Apples")
		{
			treeSprite.Play("no-apples");
		}
		else if (state == "Apples")
		{
			treeSprite.Play("apples");
			if (playerInArea == true)
			{
				if (Input.IsActionJustPressed("interact"))
				{
					state = "No-Apples";
					_dropApple();
				}
			}
		}
	}

	public void _on_pickablearea_body_entered(Node body)
	{
		if (body.HasMethod("player"))
		{
			playerInArea = true;
		}
	}

	public void _on_pickablearea_body_exited(Node body)
	{
		if (body.HasMethod("player"))
		{
			playerInArea = false;
		}
	}

	public void _on_growth_timer_timeout()
	{
		if (state == "No-Apples")
		{
			state = "Apples";
		}
	}

	public async void _dropApple() {
		Timer growthTimer = GetNode<Timer>("growth-timer");
		Marker2D marker = GetNode<Marker2D>("Marker2D");
		PackedScene apple = GD.Load<PackedScene>("://res/Scenes/apple.tscn");
		Sprite2D appleInstance = (Sprite2D)apple.Instantiate();
		appleInstance.GlobalPosition = marker.GlobalPosition;
		GetParent().AddChild(appleInstance);
		growthTimer.Start();
	}

}
