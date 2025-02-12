using FoodFight;
using Godot;
using System;
using System.Collections.Generic;
using System.Dynamic;

public partial class NextStop : Node2D, GameScene
{
    CollisionShape2D choice1;
    CollisionShape2D choice2;
    CollisionShape2D choice3;
    bool setup = false;

    private Dictionary<int, int> options = new Dictionary<int, int>();
    public void SetUp(){
        if(!setup){
            SetClickEvents();
        }
        GetOptions(1);
        Show();
    }

    private void SetClickEvents(){
        setup = true;
        Area2D choice1 = GetNode<Sprite2D>("Choice1").GetNode<Area2D>("Area2D");
        Area2D choice2 = GetNode<Sprite2D>("Choice2").GetNode<Area2D>("Area2D");
        Area2D choice3 = GetNode<Sprite2D>("Choice3").GetNode<Area2D>("Area2D");
        choice1.InputEvent += (viewport, @event, shapeIdx) => SelectionChosen(1, @event);
        choice2.InputEvent += (viewport, @event, shapeIdx) => SelectionChosen(2, @event);
        choice3.InputEvent += (viewport, @event, shapeIdx) => SelectionChosen(3, @event);
    }

    private void SelectionChosen(int choice, InputEvent @event){
        if (@event is InputEventMouseButton mouseEvent &&
            mouseEvent.Pressed &&
            mouseEvent.ButtonIndex == MouseButton.Left)
        {
            HandleSelection(choice);
            Hide();
        }
    }
  
    private void GetOptions(int currLevel){ // converts choice to the id of the next place you are going
        options.Clear();
        options.Add(1, 6);
        options.Add(2, 7);
        options.Add(3, 8);
        //TODO: use a function to determine the options based on the current level
        //30% chance of a forest 20% chance of a 
    }
    
    private void HandleSelection(int choice){
        if(options.ContainsKey(choice)){
            GameState.Instance.LoadEncounter(options[choice]);
        }
    }
}
