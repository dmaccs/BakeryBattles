using FoodFight;
using Godot;
using System;

public partial class FirstStop : Node2D, GameScene
{
    Sprite2D choiceSprite;
    
    public override void _Ready()
    {
        choiceSprite = GetNode<Sprite2D>("FirstPlace");
    }
    public void SetUp(){
        if(choiceSprite != null){
            choiceSprite.Texture = GD.Load<Texture2D>("res://PlayerShopTextures/Player" + GameState.Instance.PlayerID + ".png");
        }
        Show();
    }

    public void FirstChoicePressed(Node viewport, InputEvent @event, int shape_idx){
        if (@event is InputEventMouseButton mouseEvent &&
            mouseEvent.Pressed &&
            mouseEvent.ButtonIndex == MouseButton.Left)
        {
            GameState.Instance.LoadEncounter(2);//GameState.Instance.PlayerID);
            Hide();
        }
    }
}
