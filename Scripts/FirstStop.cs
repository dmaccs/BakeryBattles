using FoodFight;
using Godot;
using System;

public partial class FirstStop : Node2D
{
    Sprite2D choiceSprite;
    
    public override void _Ready()
    {
        choiceSprite = GetNode<Sprite2D>("FirstPlace");
    }
    public void setup(){
        if(choiceSprite != null){
            try{
                choiceSprite.Texture = GD.Load<Texture2D>("res://PlayerShopTextures/Player" + GameState.Instance.PlayerID + ".png");
            } catch (Exception e){
                GD.Print("Player" + GameState.Instance.PlayerID + ".png not found" + " Error: " + e);
            }
            
        }
    }

    public void FirstChoicePressed(InputEvent @event){
        if (@event is InputEventMouseButton mouseEvent &&
            mouseEvent.Pressed &&
            mouseEvent.ButtonIndex == MouseButton.Left)
        {
            GameState.Instance.LoadEncounter(GameState.Instance.PlayerID);
            Hide();
        }
    }
}
