using FoodFight;
using Godot;
using System;

namespace FoodFight;
public partial class GameUi : Control
{
    private Label cointCount;
    private RunTimer runTimer;
    public override void _Ready()
    {
        cointCount = GetNode<Label>("CoinCount");
        runTimer = GetNode<RunTimer>("RunTimer");
    }

    public void UpdateCoins(){
        cointCount.Text = GameState.Instance.coins.ToString("D3"); 
    }

    public void AddCoins(int coinChange){
        
    }

    public void SetHealth(int health){
        //TODO
    }

    public void ChangeHealth(){
        //TODO
    }

    public void StartRun(){
        runTimer.StartRun();
    }

    public void GameOver(){
        runTimer.StopRun();
    }

    public void _on_button_pressed(){
        GD.Print("presseed the buttooon");
        //swtich scene to kitchen so you can move stuff around
    }
}
