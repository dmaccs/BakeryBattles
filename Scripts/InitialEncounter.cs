using FoodFight;
using Godot;
using System;
using System.Collections.Generic;
using System.Data;

public partial class InitialEncounter : Node2D, GameScene
{
    private List<Area2D> selectionSlots = new List<Area2D>();
    private List<int> choices = new List<int>();
    ClickableSlot clickableSlot1;
    ClickableSlot clickableSlot2;
    ClickableSlot clickableSlot3;

    private List<ClickableSlot> clickableSlots = new List<ClickableSlot>();

    public override void _Ready()
    {
        clickableSlot1 = GetNode<ClickableSlot>("ClickableSlot1");
        clickableSlot2 = GetNode<ClickableSlot>("ClickableSlot2");
        clickableSlot3 = GetNode<ClickableSlot>("ClickableSlot3");
        clickableSlots.AddRange(new List<ClickableSlot> { clickableSlot1, clickableSlot2, clickableSlot3 });
        for(int i = 0; i < 3; i++){
            clickableSlots[i].Subscribe(new Callable(this, "HandleSelection"), i);
        }
        //SetUp();
    }

    public void SetUp(){
        GenerateChoices();
        PlaceChoices();
        Show();
    }

    private void GenerateChoices(){ //use some random functino to set this up
        choices.Clear();
        int choice1 = 0;
        int choice2 = 1;
        int choice3 = 2;
        choices.AddRange(new List<int> { choice1, choice2, choice3 });
    }

    private void PlaceChoices(){
        int totalWidth = 0;
        for(int i = 0; i < choices.Count; i++){
            Texture2D texture = ObjectDataStore.GetObjectData(choices[i]).Texture;
            GD.Print(i);
            clickableSlots[i].SetTexture(texture);
            totalWidth += texture.GetWidth();
        }
        int lastX = 0;
        for(int i = 0; i < choices.Count; i++){
            Texture2D texture = ObjectDataStore.GetObjectData(choices[i]).Texture;
            clickableSlots[i].Position = new Vector2(320 - totalWidth / 2 + lastX, 170);
            lastX += texture.GetWidth();
        }
    }

    private void OnSlotInputEvent(int slotIndex, InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent &&
            mouseEvent.Pressed &&
            mouseEvent.ButtonIndex == MouseButton.Left)
        {
            HandleSelection(slotIndex);
            Hide();
        }
    }

    private void HandleSelection(int slotIndex)
    {
        GD.Print(slotIndex);
        int ChosenID = choices[slotIndex];
        GD.Print($"Selected item {slotIndex} from slot {slotIndex}");
        GameState.Instance.AddObject(slotIndex);
        GameState.Instance.FinishedEncounter();
        // for(int i = 0; i < choices.Count; i++){
        //     choices[i].QueueFree();
        // }
    }
}
