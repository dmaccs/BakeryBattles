using Godot;
using Godot.Collections;
using System;

public partial class EventData : Node
{
    public Dictionary EventDictionary = new Dictionary();

    public static EventData Instance { get; set; } 

    public override void _Ready()
    {
        
        Instance = this;
        LoadEventDataFromJson("res://Data/dictionary.json");
        GD.Print("Something");

    }

    private void LoadEventDataFromJson(string filePath)
{
    if (Godot.FileAccess.FileExists(filePath))
    {
        var file = Godot.FileAccess.Open(filePath, Godot.FileAccess.ModeFlags.Read);
        var jsonText = file.GetAsText();
        GD.Print(jsonText);
        file.Close();

        var jsonResult = Json.ParseString(jsonText);
            if (jsonResult.GetType() == typeof(Error))
            {
                GD.PrintErr("Failed to parse JSON");
                return;
            }

        var jsonData = (Godot.Collections.Array)jsonResult;

        foreach (Godot.Collections.Dictionary eventData in jsonData)
        {
            int id = int.Parse((string)eventData["EventID"]);
            EventDictionary[id] = eventData;
        }
        GD.Print(EventDictionary.ToString());
    }
    else
    {
        GD.PrintErr("File not found: ", filePath);
    }
}

}