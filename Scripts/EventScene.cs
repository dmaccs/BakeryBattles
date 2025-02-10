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

    List<int> RewardIDs = new List<int>(4);

    [Export]
    public Texture2D eventOptionTexture;
    
    public override void _Ready(){
        EventName = GetNode<Label>("EventName");
        EventText = GetNode<Label>("EventText");
        EventImage = GetNode<TextureRect>("EventImage");
        OptionContainer = GetNode<VBoxContainer>("OptionHolder");
        for(int i = 0; i < 4; i++){
            OptionsLabels.Add(OptionContainer.GetNode<Label>("Option" + (i + 1).ToString()));
            RewardIDs.Add(0);
        }
        for(int i = 0; i < 4; i++){
            EventOptions.Add(OptionsLabels[i].GetNode<ClickableSlot>("ClickableSlot" + (i + 1).ToString()));
            EventOptions[i].SetTexture(eventOptionTexture);
        } 
        for(int i = 0; i < 4; i++){
            EventOptions[i].Subscribe(new Callable(this, "HandleSelection"), i);
        }
    }

    public void SetUp(){
        GD.Print("Setting up event but really doing nothing yet");
        Show();
        //TODO: Determine event based on given probabilities and put event data into the relevant places
        int NextStop = GameState.Instance.RandomNumberGenerator.Next(0,19);
        LoadEvent(2);
    }

    public void HandleSelection(int optionNumber){
        GD.Print("Option " + optionNumber + " chosen.");
        EventRewards.GetRewards(RewardIDs[optionNumber]);
        //TODO: do something to see if event is still ongoing otherwise remove the options and put just a leave button
        if(RewardIDs[optionNumber] != 0){
            EventOptions[0].Show();
            OptionsLabels[0].Text = "Continue";
            RewardIDs[0] = 0;
            for(int i = 1; i<4; i++){
                EventOptions[i].Hide();
                OptionsLabels[i].Hide();
            }
        }

        // if(optionNumber == 3){
        //     GameState.Instance.FinishedEncounter();
        // }
    }

// Keys: EventID,Name,Description,Option1,Result1,Option2,Result2,Option3,Result3,Option4,Result4
    private void LoadEvent(int eventID){
        RewardIDs.Clear();
        Godot.Collections.Dictionary res = (Godot.Collections.Dictionary)EventData.Instance.EventDictionary[eventID];
        EventName.Text = (string)res["Name"];
        EventText.Text = (string)res["Description"];//GD.Print(res["Name"]);
        for(int i = 0; i < 4; i++){
            if((string)res["Option" + (i+1).ToString()] == ""){
                EventOptions[i].Hide();
                OptionsLabels[i].Hide();
            } else {
                OptionsLabels[i].Text = (string)res["Option" + (i+1).ToString()];
                RewardIDs.Add((int)res["Result" + (i+1).ToString()]);
                EventOptions[i].Show();
                OptionsLabels[i].Show();
            }
        }
    }

}
