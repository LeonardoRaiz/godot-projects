using Godot;
using System;

public partial class Gem : Area2D
{
	[Export] private float _speed = 100.0f;
	[Signal] public delegate void OnScoredEventHandler();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AreaEntered += OnAreaEntered; //Signal connection
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += new Vector2(0.0f,_speed * (float)delta);
	}

	private void OnAreaEntered(Area2D area)
	{
		GD.Print("Area entered");
		EmitSignal(SignalName.OnScored); // Custom Signal emit to the game 
		QueueFree(); // Destroy the gem
	}
}
