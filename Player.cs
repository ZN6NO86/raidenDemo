using Godot;
using System;

public partial class Player : Area2D
{
	[Signal]	
	public delegate void HitEventHandler();
	[Signal]
	public delegate void ShootEventHandler(PackedScene bullet01Scene, Vector2 location, Vector2 velocity);
	[Export]
	public int Speed { get; set; } = 400;
	[Export]
	public PackedScene Bullet01Scene = ResourceLoader.Load<PackedScene>("res://Bullet01.tscn");
	//public PackedScene Bullet01Scene { get; set;}
	bool shootFlag = true;
	public Vector2 ScreenSize;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}
	
	/*public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButton)
		{
			if (mouseButton.ButtonIndex == MouseButton.Left && mouseButton.Pressed)
			{
				EmitSignal(SignalName.Shoot, Bullet01Scene, Position);
			}
		}
	}*/
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero; // The player's movement vector.

		if (Input.IsActionPressed("move_right"))
		{
			velocity.X += 1;
		}

		if (Input.IsActionPressed("move_left"))
		{
			velocity.X -= 1;
		}

		if (Input.IsActionPressed("move_down"))
		{
			velocity.Y += 1;
		}

		if (Input.IsActionPressed("move_up"))
		{
			velocity.Y -= 1;
		}
		if (Input.IsActionPressed("shoot"))
		{
			if (shootFlag)
			{
				EmitSignal(SignalName.Shoot, Bullet01Scene, Position, velocity);
				shootFlag = false;
				GetNode<Timer>("ShootTimer").Start();
			}
			
			//Bullet01 bullet01 = Bullet01Scene.Instantiate<Bullet01>();
			//Console.WriteLine("shilihua");
			//bullet01.Start(Position);
			//AddChild(bullet01);
		}

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}
		
		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
			
		if (velocity.X < 0)
			animatedSprite2D.FlipH = true;
		else
			animatedSprite2D.FlipH = false;
	}
	private void _on_body_entered(Node2D body)
	{
		//Hide();
		//EmitSignal(SignalName.Hit);
		//GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	
	}
	public void Start(Vector2 position)
	{
		Position = position;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}
	private void _on_shoot_timer_timeout()
	{
		shootFlag = true;
		// Replace with function body.
	}
}






