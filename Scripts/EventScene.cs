using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace FoodFight;
public partial class EventScene : Node2D, GameScene
{
    Label EventName;
    Label EventText;
    TextureRect EventImage;
    VBoxContainer OptionContainer;

    List<ClickableSlot> EventOptions = new List<ClickableSlot>();
    List<Label> OptionsLabels = new List<Label>();

    [Export]
    public Texture2D eventOptionTexture;
    
    public override void _Ready(){
        EventName = GetNode<Label>("EventName");
        EventText = GetNode<Label>("EventText");
        EventImage = GetNode<TextureRect>("EventImage");
        OptionContainer = GetNode<VBoxContainer>("OptionHolder");
        for(int i = 0; i < 4; i++){
            OptionsLabels.Add(OptionContainer.GetNode<Label>("Option" + (i + 1).ToString()));
        }
        for(int i = 0; i < 4; i++){
            EventOptions.Add(OptionsLabels[i].GetNode<ClickableSlot>("ClickableSlot" + (i + 1).ToString()));
            EventOptions[i].SetTexture(eventOptionTexture);
        } 
        for(int i = 0; i < 4; i++){
            EventOptions[i].Subscribe(new Callable(this, "HandleSelection"), i);
        }
        for(int i = 0; i < 3; i++){
            EventOptions[i].Subscribe(new Callable(this, "HandleSelection"), i);
        }
    }

    public void SetUp(){
        GD.Print("Setting up event but really doing nothing yet");
        Show();
        //TODO: Determine event based on given probabilities and put event data into the relevant places
    }

    public void HandleSelection(int optionNumber){
        GD.Print("Option " + optionNumber + " chosen.");
        if(optionNumber == 3){
            GameState.Instance.FinishedEncounter();
        }
    }

}
