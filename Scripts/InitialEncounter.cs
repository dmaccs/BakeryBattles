using FoodFight;
using Godot;
using System;
using System.Collections.Generic;
using System.Data;

public partial class InitialEncounter : Node2D
{
    private List<Area2D> selectionSlots = new List<Area2D>();
    private List<KitchenObject> choices = new List<KitchenObject>();

    public override void _Ready()
    {
        GenerateChoies();
        //PlaceChoices();
        CreateSelectionSlots();
    }

    private void GenerateChoies()
    {
        choices.Clear();
        //some function here to determine the choies randomly
        KitchenObject choice1 = KitchenObject.CreateInstance(0);
        KitchenObject choice2 = KitchenObject.CreateInstance(1);
        KitchenObject choice3 = KitchenObject.CreateInstance(2);
        choices.AddRange(new List<KitchenObject> { choice1, choice2, choice3 });
        choice1.SetDraggable(false);
        choice2.SetDraggable(false);
        choice3.SetDraggable(false);
        AddChild(choice1);
        AddChild(choice2);
        AddChild(choice3);
        //PlaceChoices();
    }

    public void SetUp(){
        GenerateChoies();
        PlaceChoices();
    }

    private void PlaceChoices()
    {
        //first call some function to determine the choices, manual for now:
        // Choose a random texture

        int spacing = 20;
        int count = 3;
        int totalWidth = spacing * (count - 1); //adding the space between the items for centering
        int screenwidth = 640;
        for (int i = 0; i < count; i++)
        {
            totalWidth += choices[i].sprite.Texture.GetWidth();
        }
        int currentX = (screenwidth - totalWidth) / 2;
        GD.Print("Start value:" + currentX);
        for (int i = 0; i < count; i++)
        {
            choices[i].Position = new Vector2(currentX, 170);
            GD.Print("Item X position = " + currentX);
            currentX += choices[i].sprite.Texture.GetWidth() + spacing;
        }
        GD.Print("Final value:" + currentX);
    }

    private void CreateSelectionSlots()
    {
        int spacing = 20;
        int screenwidth = 640;
        int count = 3;

        // Calculate total width
        int totalWidth = spacing * (count - 1);
        for (int i = 0; i < count; i++)
        {
            totalWidth += choices[i].sprite.Texture.GetWidth();
        }

        // Create and position slots
        int currentX = (screenwidth - totalWidth) / 2;
        for (int i = 0; i < count; i++)
        {
            Area2D slot = new Area2D();
            CollisionShape2D collision = new CollisionShape2D();
            RectangleShape2D shape = new RectangleShape2D();

            shape.Size = choices[i].sprite.Texture.GetSize();
            collision.Shape = shape;
            slot.AddChild(collision);
            slot.Position = new Vector2(currentX + shape.Size.X / 2, 170 + shape.Size.Y / 2);

            // Position KitchenObject
            choices[i].Position = new Vector2(currentX, 170);

            // Connect input handling
            int index = i;
            slot.InputEvent += (viewport, @event, shapeIdx) => OnSlotInputEvent(index, @event);

            AddChild(slot);
            selectionSlots.Add(slot);

            currentX += choices[i].sprite.Texture.GetWidth() + spacing;
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
        KitchenObject selectedObject = choices[slotIndex];
        GD.Print($"Selected {selectedObject.objectData.ItemName} from slot {slotIndex}");
        GameState.Instance.AddObject(selectedObject.objectData.ID);
        for(int i = 0; i < choices.Count; i++){
            choices[i].QueueFree();
        }
    }

    // private List<string> getTextures(List<int> ids)
    // {
    //     List<string> texturePaths = new List<string>();
    //     foreach (int objectId in ids)
    //     {
    //         texturePaths.Add(GameState.Instance.GetObjectTexture(objectId));
    //     }
    //     return texturePaths;
    // }

    // private List<int> getObjects(int num, int sheetId)
    // {
    //     if (sheetId == 0)
    //     {
    //         return new List<int> { 1, 2, 3 };
    //     }
    //     GD.Print("No Behaviour yet need to implement sheets to chose from");
    //     return new List<int> { 0, 0, 0, 0 };
    // }
}
