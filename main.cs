using Godot;
using System;

public partial class main : Node
{
	[Export]
	public PackedScene EnemyScene { get; set;}
	private int _score;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NewGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void game_over()
	{
		GetNode<Timer>("EnemyTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
	}
	public void NewGame(){
		_score = 0;
		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Marker2D>("StartPosition");
		player.Start(startPosition.Position );
		GetNode<Timer>("StartTimer").Start();
	}
	private void _on_start_timer_timeout()
	{

		GetNode<Timer>("EnemyTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
		// Replace with function body.
	}


	private void OnScoreTimerTimeout()
	{
		_score++;
		// Replace with function body.
	}


	



	private void OnEnemyTimerTimeout()
	{
		Enemy enemy = EnemyScene.Instantiate<Enemy>();
		var enemySpawnLocation = GetNode<PathFollow2D>("EnemyPath/EnemySpawnLocation");
		enemySpawnLocation.ProgressRatio = GD.Randf();
		float direction = enemySpawnLocation.Rotation + Mathf.Pi/2;
		enemy.Position = enemySpawnLocation.Position;
		direction += (float)GD.RandRange(-Mathf.Pi /4, Mathf.Pi / 4);
		enemy.Rotation = direction;
		var velocity = new Vector2((float)GD.RandRange(150.0,250.0), 0);
		enemy.LinearVelocity = velocity.Rotated(direction);
		AddChild(enemy);
	}
	
	private void _on_player_shoot(PackedScene bullet01Scene, Vector2 location)
	{
		var position = location;
		var bullet01 = bullet01Scene.Instantiate<Bullet01>();
		bullet01.Start(position);
		AddChild(bullet01);
		
	// Replace with function body.
	}
}












