using Godot;
using System;
using System.Collections;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;

public partial class Player : CharacterBody2D
{

	public const float Speed = 120;
	private string playerState; // Will control the player anims via strings so keep the string close to the anims if needed.

	public override void _PhysicsProcess(double delta)
	{
		Vector2 dir = Input.GetVector("left", "right", "up", "down"); //Vectors: -x , x, y, -y. They match with the controls.

		if (dir.X == 0 && dir.Y == 0)
		{
			playerState = "Idle";
		}
		else if (dir.X != 0 || dir.Y != 0)
		{
			playerState = "walking";
		}

		Velocity = dir * Speed;
		MoveAndSlide();
		playAnims(dir);
	}

	public void playAnims(Vector2 dir)
	{
		AnimatedSprite2D Sprite = GetNode<AnimatedSprite2D>("player-sprite");
		switch (playerState)
		{
			case "Idle":
				Sprite.Play("Idle");
				break;
			case "walking":
				switch (dir.X)
				{
					case 1:
						Sprite.Play("R-walk");
						break;
					case -1:
						Sprite.Play("L-walk");
						break;
				}
				switch (dir.Y)
				{
					case 1:
						Sprite.Play("D-walk");
						break;
					case -1:
						Sprite.Play("U-walk");
						break;
				}
				if (dir.X > 0.5 && dir.Y < -0.5)
				{
					Sprite.Play("UR-walk");
				}
				else if (dir.X > 0.5 && dir.Y > 0.5)
				{
					Sprite.Play("DR-walk");
				}
				else if (dir.X < -0.5 && dir.Y > 0.5)
				{
					Sprite.Play("DL-walk");
				}
				else if (dir.X < -0.5 && dir.Y < -0.5)
				{
					Sprite.Play("UL-walk");
				}

				break;
		}
	}
	public void player(){}
}
