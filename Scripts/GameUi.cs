using FoodFight;
using Godot;
using System;

namespace FoodFight;
public partial class GameUi : Control
{
    private Label cointCount;
    public override void _Ready()
    {
        cointCount = GetNode<Label>("CoinCount");
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
}
