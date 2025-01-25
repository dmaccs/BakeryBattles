using Godot;

namespace FoodFight;

public partial class GameState : Node
{
    Kitchen PlayerKitchen;
    public Enemy Customer;

    public static GameState Instance { get; private set; }

    public int Health { get; set; }

    public Node CurrentScene {get; set; }

    int encoutnerNum = 0;

    int firstObjectID = 0;

    public override void _Ready()
    {
        Instance = this;
        Viewport root = GetTree().Root;    
        CurrentScene = root.GetChild(-1); 
    }

    public void FightOver(bool didWin){
        PlayerKitchen.StopFight(didWin);
    }

    public void StartGame(int CharacterChoice = 0){ //placeholder, eventually 0,1,2 etc will choose which char you use
        encoutnerNum = 0;
        CallDeferred(MethodName.LoadEncounter);
        Health = 1; // can make this change for different players
    }

    private void LoadEncounter(){
        CurrentScene.Free();
        PackedScene nextScene = null;
        if(encoutnerNum == 0){
            nextScene = GD.Load<PackedScene>("res://Scenes/initial_encounter.tscn");
        } else if(encoutnerNum == 1){
            nextScene = GD.Load<PackedScene>("res://Scenes/kitchen.tscn");
        }
        CurrentScene = nextScene.Instantiate();
        GetTree().Root.AddChild(CurrentScene);
        GetTree().CurrentScene = CurrentScene;
        if(encoutnerNum == 1){
            InitializeGame();
        }
        encoutnerNum++;
    }

    public void AddItemToPlayer(KitchenObject item)

    {
        ////GD.Print($"Item {item.objectData.ItemName} added to player.");
        //CallDeferred(MethodName.LoadEncounter,encoutnerNum);
    }

    public void InitializeKitchen(int objectID){
        
        firstObjectID = objectID;
        CallDeferred(MethodName.LoadEncounter);
    }

    private void InitializeGame(){ // sets up references for game
        Customer = GetTree().Root.GetNode<Enemy>("Kitchen/FoodCritic");
        PlayerKitchen = GetTree().Root.GetNode<Kitchen>("Kitchen"); 
        KitchenObject firstObject = KitchenObject.CreateInstance(firstObjectID);
        PlayerKitchen.PlayerStation.SetSlot(firstObject,0);
    }

    public string GetObjectTexture(int id){
        switch (id){
            case 1:
                return "res://Sprites/Toaster.png";
            case 2:
                return "res://Sprites/Blender.png";
            case 3:
                return "res://Sprites/Oven.png";
            default:
                GD.Print("No Texture with this ID exists");
                return "";
        }
    }

    public void GameOver(){
        GD.Print("Game Over!");
    }

}
