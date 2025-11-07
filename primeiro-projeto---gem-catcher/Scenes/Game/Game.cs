using Godot;
using System;

public partial class Game : Node2D
{
    private const float GEM_MARGIN = 100.0f;
    [Export] private PackedScene _gemScene;
    [Export] private Timer _spawnTimer;
    [Export] private Label _scoreLabel;
    [Export] private AudioStreamPlayer _soundBg;
    [Export] private AudioStreamPlayer2D _effects;
    [Export] private AudioStream _explodeSound;
    
    private int _score = 0;

    // 1. Adicione a "trava"
    private bool _isInitialized = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
       _spawnTimer.Timeout += SpawnGem;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (_isInitialized)
        {
            return;
        }
        Rect2 vpr = GetViewportRect();
        if (vpr.Size.X > 200) 
        {
            _isInitialized = true;
            SpawnGem();
            _spawnTimer.Start();
        }
    }

    private void OnScored()
    {
       GD.Print("Scored Received");
       _score++;
       _scoreLabel.Text = $"{_score:0000}";
       _effects.Play();
    }
    
    private void SpawnGem()
    {
       Rect2 vpr = GetViewportRect();
       Gem gem = (Gem)_gemScene.Instantiate();
       AddChild(gem);
       
       float rX = (float)GD.RandRange(
          vpr.Position.X + GEM_MARGIN, vpr.End.X - GEM_MARGIN
       );
       
       gem.Position = new Vector2(rX, -100);
       
       gem.OnScored += OnScored;
       gem.OnGemOffScreen += OnGameOver;
    }
    
    private void OnGameOver()
    {
       GD.Print("Game Over");
       foreach (Node node in  GetChildren())
       {
          if (IsInstanceValid(node))
          {
             node.SetProcess(false);
          }
       }
       _effects.Stop();
       _effects.Stream = _explodeSound;
       _effects.Play();
       _spawnTimer.Stop();
       _soundBg.Stop();
    }
}