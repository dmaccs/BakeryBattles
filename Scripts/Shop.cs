using Godot;
using System;

namespace FoodFight;
public partial class Shop : Node2D, GameScene
{
    public void SetUp(){
        GD.Print("Setting up shop!");
        Show();
    }
}
