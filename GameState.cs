using Godot;

namespace FoodFight;

public partial class GameState : Node
{
    Kitchen PlayerKitchen;
    Enemy Customer;

    public static GameState Instance { get; private set; }

    public int Health { get; set; }

    public Node CurrentScene {get; set; }

    int encoutnerNum = 0;

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
        CallDeferred(MethodName.LoadEncounter,encoutnerNum);
        Health = 1; // can make this change for different players
    }

    private void LoadEncounter(int encoutnerNum){
        CurrentScene.Free();
        PackedScene nextScene = GD.Load<PackedScene>("res://Scenes/kitchen.tscn");
        CurrentScene = nextScene.Instantiate();
        GetTree().Root.AddChild(CurrentScene);
        GetTree().CurrentScene = CurrentScene;
        if(encoutnerNum == 0){
            InitializeGame();
        }
    }

    private void InitializeGame(){ // sets up references for game
        Customer = GetTree().Root.GetNode<Enemy>("Kitchen/FoodCritic");
        PlayerKitchen = GetTree().Root.GetNode<Kitchen>("Kitchen"); 
    }

    public void GameOver(){
        GD.Print("Game Over!");
    }

}
