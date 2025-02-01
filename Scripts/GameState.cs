using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using Godot;

namespace FoodFight;

public partial class GameState : Node
{
    private Texture2D emptyHeartTexture;

    private Texture2D fullHeartTexture;
    Kitchen PlayerKitchen;
    public Enemy Customer;

    public static GameState Instance { get; private set; }

    public int Health { get; set; }

    public Node RootScene { get; set; }

    int encoutnerNum = 0;

    HashSet<int> InstantiatedScenes = new HashSet<int>();

    public int Score = -69;
    int firstObjectID = 0;
    private uint seed;

    public int PlayerID = 0;

    Control UI;
    //All of the loaded scenes
    private InitialEncounter initialEncounter;
    private BattleTransition EndFightSceneNode;
    private FirstStop FirstStop;
    private NextStop NextStop;
    private TitleScreen TitleScreen;
    private GameOver GameoverScreen;

    public override void _Ready()
    {
        TitleScreen = GetNode<TitleScreen>("/root/Menu");
        Instance = this;
        Viewport root = GetTree().Root;
        RootScene = root.GetChild(-1);
        UI = GetNode<Control>("/root/GameUi");
        emptyHeartTexture = GD.Load<Texture2D>("res://Sprites/emptyheart.png");
        fullHeartTexture = GD.Load<Texture2D>("res://Sprites/heart.png");
        GD.Print("Game State Ready");
    }

    public void FightOver(bool didWin)
    {
        PlayerKitchen.StopFight(didWin);
        EndFight();
        if (!didWin)
        {
            LoseLife();
            EndFightSceneNode.GetNode<RichTextLabel>("Control/Container/Rewards").Visible = false;
            GD.Print("Lost Fight");
        }
        else
        {
            GD.Print("Won Fight");
            EndFightSceneNode.GetNode<RichTextLabel>("Control/Container/Rewards").Visible = true;
        }
        if (Health <= 0)
        {
            EndFightSceneNode.Hide();
        }
    }

    private void EndFight()
    {
        if (EndFightSceneNode == null)
        {
            PackedScene EndFightScene = GD.Load<PackedScene>("res://Scenes/BattleTransition.tscn");
            EndFightSceneNode = (BattleTransition)EndFightScene.Instantiate();
            GetTree().Root.AddChild(EndFightSceneNode);
            GetTree().CurrentScene = EndFightSceneNode;
        }
        else
        {
            EndFightSceneNode.Show();
        }
    }

    private void LoseLife()
    {
        if (Health <= 0) return;

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
        encoutnerNum = 0;
        Health = 5; // can make this change for different players
        Score = 0;
        LoadEncounter();
    }

    public uint GetSeed()
    {
        return seed;
    }

    public void LoadNextScene(int SceneType)
    {
        int sceneMod = SceneType % 2;
        switch (sceneMod)
        {
            case 0:
                LoadInitialEncounter();
                break;
            case 1:
                LoadKitchen();
                break;
            case 2:
                //LoadFight();
                break;
            default:
                GD.Print("No Scene with this ID exists");
                break;
        }
    }

    private void LoadKitchen()
    {
        PlayerKitchen.Show();
        PlayerKitchen.SetUpFight();
        if (initialEncounter != null)
        {
            initialEncounter.Hide();
        }
    }


    private void LoadInitialEncounter()
    {
        initialEncounter.SetUp();
        initialEncounter.Show();
        if (EndFightSceneNode != null)
        {
            EndFightSceneNode.Hide();
        }
        if (PlayerKitchen != null)
        {
            PlayerKitchen.Hide();
        }
    }

    public void LoadEncounter()
    {
        //CurrentScene.Free();
        PackedScene nextScene = null;
        Node nextSceneNode = null;
        if (encoutnerNum == 0)
        {
            if(initialEncounter == null){
                nextScene = GD.Load<PackedScene>("res://Scenes/initial_encounter.tscn");
                nextSceneNode = nextScene.Instantiate();
                initialEncounter = nextSceneNode as InitialEncounter;
                GetTree().Root.AddChild(nextSceneNode);
            } else {
                LoadInitialEncounter();
            }
        } else if (encoutnerNum == 1)
        {
            if(PlayerKitchen == null){
                nextScene = GD.Load<PackedScene>("res://Scenes/kitchen.tscn");
                nextSceneNode = nextScene.Instantiate();
                PlayerKitchen = nextSceneNode as Kitchen;
                GetTree().Root.AddChild(nextSceneNode);
            } else {
                LoadKitchen();
            }
        } else {
            LoadNextScene(encoutnerNum);
            encoutnerNum++;
            return;
        }

        
        //GetTree().CurrentScene = nextSceneNode;
        if (encoutnerNum == 1)
        {
            InitializeGame();
        }
        encoutnerNum++;
    }

    public void LoadEncounter(int choice)
    {
        GD.Print($"Choice {choice} selected.");
        if(!InstantiatedScenes.Contains(choice)){
            object LoadedScene = InstantiateScene(0);
            //(GameScene)LoadedScene.Load();
        }
    }

    private object InstantiateScene(int sceneNum){
        string PackedScenePath = ScenePaths.GetScenePath(sceneNum);
        PackedScene nextScene = GD.Load<PackedScene>(PackedScenePath);
        Node nextSceneNode = nextScene.Instantiate();
        GetTree().Root.AddChild(nextSceneNode);
        InstantiatedScenes.Add(sceneNum);
        return nextSceneNode;
    }

    public void AddItemToPlayer(KitchenObject item)

    {
        ////GD.Print($"Item {item.objectData.ItemName} added to player.");
        //CallDeferred(MethodName.LoadEncounter,encoutnerNum);
    }

    public void AddObject(int objectID)
    {

        LoadEncounter();
        KitchenObject firstObject = KitchenObject.CreateInstance(objectID);
        PlayerKitchen.PlayerStation.AddObject(firstObject);
    }

    private void InitializeGame()
    { // sets up references for game
        Customer = GetTree().Root.GetNode<Enemy>("Kitchen/FoodCritic");
        PlayerKitchen = GetTree().Root.GetNode<Kitchen>("Kitchen");
        UI.Visible = true;
    }

    public string GetObjectTexture(int id)
    {
        switch (id)
        {
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

    public void GameOver()
    {
        bool wasNull = GameoverScreen == null;
        if(GameoverScreen == null){
            PackedScene GameOver = GD.Load<PackedScene>("res://Scenes/game_over.tscn");
            GameoverScreen = (GameOver)GameOver.Instantiate();
            GetTree().Root.AddChild(GameoverScreen);
        }
        GameoverScreen.Show();
        GameoverScreen.SetScore();
        PlayerKitchen.Hide();  
        }

    public void BackToMenu()
    {
        TitleScreen.Show();
        GameoverScreen.Hide();
    }

    public void ResetGame()
    {
        if(PlayerKitchen != null){
            GD.Print("Resetting game!");
            PlayerKitchen.ResetKitchen();
        }
        ResetLife();
    }

}
