using Godot;
using System;

namespace FoodFight;
public partial class GameOver : Node2D, GameScene
{
      public override void _Ready()
    {
    }

    public void SetUp()
    {
        SetScore();
        Show();
    }

    public void SetScore(){
        GetNode<Label>("Score").Text = "Score: " + GameState.Instance.Score;
    }

    public void BackToMenuPressed(){
        GameState.Instance.LoadEncounter(-1);
    }
}
