using Godot;
using System;

namespace FoodFight;
public partial class GlobalTimer : Node
{
    private Timer gameTimer;
    public static GlobalTimer Instance;
    [Signal]
    public delegate void TimerTickEventHandler();

    public override void _Ready()
    {
        Instance = this;
        gameTimer = new();
        AddChild(gameTimer);
        gameTimer.OneShot = false;
        gameTimer.WaitTime = 0.1f;
        gameTimer.Timeout += () => EmitSignal(nameof(TimerTick));
        gameTimer.Start();
    }

}
