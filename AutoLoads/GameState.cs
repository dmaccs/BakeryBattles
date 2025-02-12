using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Godot;

namespace FoodFight;

public partial class GameState : Node
{
    private Texture2D emptyHeartTexture;

    private Texture2D fullHeartTexture;
    public KitchenStations kitchenStations;
    public Enemy Customer;

    public static GameState Instance { get; private set; }

    public int Health { get; set; }

    public Node RootScene { get; set; }

    int encounterNum = 0;

    public int coins { get; set; } = 0;

    public int Score = -69;
    int firstObjectID = 0;
    private uint seed;

    public int PlayerID = 1;

    public Random RandomNumberGenerator;

    GameUi  UI;
    //All of the loaded scenes
    // private InitialEncounter initialEncounter;
    // private BattleTransition EndFightSceneNode;
    // private FirstStop FirstStop;
    // private NextStop NextStop;
    // private TitleScreen TitleScreen;
    // private GameOver GameoverScreen;

    private Dictionary<int, GameScene> GameScenes = new Dictionary<int, GameScene>();

    private int currScene = -1;

    public override void _Ready()
    {
        GameScenes.Add(-1,GetNode<TitleScreen>("/root/Menu"));
        Instance = this;
        Viewport root = GetTree().Root;
        RootScene = root.GetChild(-1);
        UI = GetNode<GameUi>("/root/GameUi");
        emptyHeartTexture = GD.Load<Texture2D>("res://Sprites/emptyheart.png");
        fullHeartTexture = GD.Load<Texture2D>("res://Sprites/heart.png");
        GD.Print("Game State Ready");
    }

    public void FightOver(bool didWin)
    {
        //PlayerKitchen.StopFight(didWin);
        EndFight();
        if (!didWin)
        {
            LoseLife();
            ((Node2D)GameScenes[4]).GetNode<RichTextLabel>("Control/Container/Rewards").Visible = false; //TODO: these scenes don't exist in new encounter
            GD.Print("Lost Fight");
            ((Encounter)GameScenes[8]).StopFight();
        }
        else
        {
            coins += 30;
            UI.UpdateCoins();
            GD.Print("Won Fight");
            ((Node2D)GameScenes[4]).GetNode<RichTextLabel>("Control/Container/Rewards").Visible = true; //TODO: these scenes don't exist in new encounter
            ((Encounter)GameScenes[8]).StopFight();
        }
        if (Health <= 0)
        {
            ((Node2D)GameScenes[4]).Hide();
        }
    }

    private void EndFight()
    {
        if (!GameScenes.ContainsKey(4))
        {
            PackedScene EndFightScene = GD.Load<PackedScene>("res://Scenes/BattleTransition.tscn");
            GameScenes.Add(4,(GameScene)EndFightScene.Instantiate());
            GetTree().Root.AddChild((Node2D)GameScenes[4]);
            ((Node2D)GameScenes[4]).ZIndex = 1;

        }
        else
        {
            ((Node2D)GameScenes[4]).Show();
        }
    }

    private void LoseLife()
    {
        if (Health <= 0) return;
        //TODO: move into UI
        var heartContainer = UI.GetNode<HBoxContainer>("TextureRect/HBoxContainer");
        var healthIcon = heartContainer.GetNode<TextureRect>($"Health{Health}");
        healthIcon.Texture = emptyHeartTexture;
        Health--;

        if (Health <= 0)
        {
            // Handle game over
            GD.Print("Game Over!");
            GameOver();
        }
    }

    private void ResetLife(){
        var heartContainer = UI.GetNode<HBoxContainer>("TextureRect/HBoxContainer");
        for (int i = 1; i <= 5; i++)
        {
            var healthIcon = heartContainer.GetNode<TextureRect>($"Health{i}");
            healthIcon.Texture = fullHeartTexture;
        }
    }

    public void StartGame(int CharacterChoice = 0)
    { //placeholder, eventually 0,1,2 etc will choose which char you use
        GD.Randomize();
        seed = GD.Randi();
        GD.Seed(seed);
        RandomNumberGenerator = new Random((int)seed);
        encounterNum = 0;
        Health = 5; // can make this change for different players
        Score = 0;
        UI.Visible = true;
        UI.StartRun();
        LoadEncounter(0);
        if(!GameScenes.ContainsKey(9)){
            GameScenes.Add(9, (GameScene)InstantiateScene(9));
            kitchenStations = (KitchenStations)GameScenes[9];
            kitchenStations.Hide();
        }
    }

    public uint GetSeed()
    {
        return seed;
    }

    public void LoadEncounter(int choice)
    {
        GameScene currentScene = null;
        if(GameScenes.TryGetValue(currScene, out currentScene)){
            ((Node2D)currentScene).Hide();
        }
        currScene = choice;
        GD.Print($"Choice {choice} selected.");
        encounterNum++;
        if(!GameScenes.ContainsKey(choice)){
            object LoadedScene = InstantiateScene(choice);
            GameScenes.Add(choice,(GameScene)LoadedScene);
            GD.Print("Loaded Scene" + choice);
            GD.Print("Now setting up scene " + choice);
            GameScenes[choice].SetUp();
        } else {
            GD.Print("Setting Up Scene " + choice);
            GameScenes[choice].SetUp();
        }
    }

    private object InstantiateScene(int sceneNum){
        string PackedScenePath = ScenePaths.GetScenePath(sceneNum);
        PackedScene nextScene = GD.Load<PackedScene>(PackedScenePath);
        Node nextSceneNode = nextScene.Instantiate();
        GetTree().Root.AddChild(nextSceneNode);
        return nextSceneNode;
    }

    public void AddObject(int objectID)
    {
        if(kitchenStations == null){
            kitchenStations = (KitchenStations)GameScenes[9];
        }
        KitchenObject firstObject = KitchenObject.CreateInstance(objectID);
        kitchenStations.AddObject(firstObject);
    }

    public void ChangeCoins(int changeAmount){
        coins+= changeAmount;
        if(coins<0){
            coins = 0;
        }
        UI.UpdateCoins();
    }

    public void FinishedEncounter(){
        Node2D sceneNode = (Node2D)GameScenes[currScene];
        LoadEncounter(1);
        sceneNode.Hide();
    }

    public void GameOver()
    {
        if(!GameScenes.ContainsKey(3)){
            PackedScene GameOver = GD.Load<PackedScene>("res://Scenes/game_over.tscn");
            GameScenes.Add(3,(GameScene)GameOver.Instantiate());
            GetTree().Root.AddChild((Node2D)GameScenes[3]);
        }
        GameOver gameOverScene = (GameOver)GameScenes[3];
        gameOverScene.Show();
        gameOverScene.SetScore();
        Node2D theScene = (Node2D)GameScenes[currScene];
        theScene.Hide();
        UI.Visible = false;
        UI.GameOver();
        currScene = 3;
        }

    public void ResetGame()
    {
        // if(PlayerKitchen != null){
        //     GD.Print("Resetting game!");
        //     PlayerKitchen.ResetKitchen();
        // }
        coins = 0;
        UI.UpdateCoins();
        ResetLife();
    }

}
