using Godot;
using System;

public partial class ClickableSlot : Node2D
{
    TextureRect slotTexture;
    TextureButton slotButton;
    private int slotNum = -1;
    Callable subscribedFunction;

    public override void _Ready()
    {
        //slotTexture = GetNode<TextureRect>("TextureRect");
        slotButton = GetNode<TextureButton>("TextureButton");
    }

    public void OnSlotPressed(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent &&
            mouseEvent.Pressed &&
            mouseEvent.ButtonIndex == MouseButton.Left)
        {
            GD.Print("Calling function (This fails silently)");
            subscribedFunction.Call(slotNum);
            GD.Print("Slot pressed");
        }
    }

    public void OnSlotButtonPressed(){
        GD.Print("Calling function (This fails silently)");
        subscribedFunction.Call(slotNum);
        GD.Print("Slot button pressed");
    }


    public void SetTexture(Texture2D texture)
    {
        slotButton.TextureNormal = texture;
        //slotTexture.Texture = texture;
    }

    public void Subscribe(Callable func, int newSlotNum){
        slotNum = newSlotNum;
        subscribedFunction = func;

    }
}
