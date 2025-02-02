using FoodFight;
using Godot;
using System;
namespace FoodFight;
public partial class BattleTransition : Node2D, GameScene
{
    public override void _Ready()
    {

    }

    public void HideNode(){
        Hide();
    }

    public void SetUp()
    {
        GD.Print("setting up battle transition");
        Show();
    }

    private void ContinuePressed(){
        Hide();
        GameState.Instance.FinishedEncounter();
    }
}
