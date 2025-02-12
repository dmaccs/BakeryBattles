using Godot;
using System;

namespace FoodFight;
public partial class Encounter : Node2D, GameScene
{
    private Enemy enemy;
    private TextureRect BackgroundTexture;

    private Button StartButton;
    public override void _Ready()
    {
        enemy = GetNode<Enemy>("Enemy");
        BackgroundTexture = GetNode<TextureRect>("BackgroundTexture");
        StartButton = GetNode<Button>("Button");
        ConnectTimer();
        GameState.Instance.Customer = enemy;
    }

    public void _on_button_pressed(){
        GD.Print("FIGHT START!");
        StartButton.Hide();
        enemy.StartFight();
        GlobalTimer.Instance.StartTicking();
    }

    public int getNextFight(int locationType){
        if(locationType == 0){
            GD.Print("GINGERBREAD MAN!");
            return 1;
        }
        return 0;
    }

    private void ConnectTimer(){
        GlobalTimer.Instance.Connect("TimerTick",Callable.From(HandleTick));
        //TODO: make sure you disconnect this when getting rid of encounter, we can also start and stop the timer 
        //when needed as well as running all things connected from this single instance, also maybe make it like 0.01seconds instead?
        //idk
    }

    public void SetUp()
    {
        StartButton.Show();
        Show();
        int nextFight = getNextFight(0);
        InitFight(nextFight);
        BackgroundTexture.Texture = GD.Load<Texture2D>("res://Sprites/bigForest.png");
        GameState.Instance.kitchenStations.Show();
        GameState.Instance.kitchenStations.Position = new Vector2(53,239);
        GameState.Instance.kitchenStations.Scale = new Vector2(.76f,.76f);
        GameState.Instance.kitchenStations.ZIndex = 1;
        
        
    }

    private void InitFight(int fightNum){
        Enemies.setEnemy(enemy, fightNum);
    }

    public void StopFight(){
        GlobalTimer.Instance.StopTicking();
    }

    public void HandleTick(){
        GameState.Instance.kitchenStations.ActivateEffects();
        enemy.ActivateEffects();
        enemy.CheckResults();
    }

}
