using Godot;
using System;

public partial class Paddle : Area2D
{
	[Export] private float _speed = 100.0f;
	[Export] private float _margin = 100.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		MovePaddle(delta);
	}
	
	
	
	private void MovePaddle(double delta)
	{
		if (Input.IsActionPressed("right"))
		{
			Position += new Vector2(_speed * (float)delta, 0.0f);
		}

		if (Input.IsActionPressed("left"))
		{
			Position -= new Vector2(_speed * (float)delta, 0.0f);
		}
		
		RestrictingPaddlePosition();
	}
	
	private void RestrictingPaddlePosition()
	{
		Rect2 vpr = GetViewportRect();
		if (Position.X < vpr.Position.X + _margin)
		{
			Position = new Vector2(vpr.Position.X + _margin, Position.Y);
		}
		
		if (Position.X > vpr.End.X - _margin)
		{
			Position = new Vector2(vpr.End.X - _margin, Position.Y);
		}	
	}

}
