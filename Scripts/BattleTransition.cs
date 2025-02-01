using FoodFight;
using Godot;
using System;
namespace FoodFight;
public partial class BattleTransition : Node2D
{
    public override void _Ready()
    {
        GetNode<TextureButton>("Continue").Connect("pressed", new Callable(GameState.Instance, "LoadEncounter"));
    }

    public void HideNode(){
        Hide();
    }
}
