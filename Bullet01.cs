using Godot;
using System;

public partial class Bullet01 : Area2D
{
	int speed = 750;
	
	Vector2 velocity = Vector2.Zero;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		velocity.Y = -600;
		Position += velocity * (float)delta;
		
	}
	public override void _PhysicsProcess(double delta)
	{
		//velocity.Y = 2;
		
		
	}
	public void Start(Vector2 pos)
	{
		Position = pos;
		
		
	}
	private void _on_visible_on_screen_notifier_2d_screen_exited()
	{
		QueueFree();
	}
}



