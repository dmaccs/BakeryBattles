using Godot;
using System;

namespace FoodFight;
public partial class GameOver : Node2D
{
      public override void _Ready()
    {
        GetNode<Button>("Button").Connect("pressed", new Callable(GameState.Instance, "BackToMenu"));
    }

    public void SetScore(){
        GetNode<Label>("Score").Text = "Score: " + GameState.Instance.Score;
    }
}
