using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace FoodFight;
public partial class Enemy : Node2D
{
    public int hungerRate = 20;
    public int maxFullness = 100;
    TextureProgressBar fullness;
    bool IsFighting = false;

    List<EnemyAbility> Abilities = new List<EnemyAbility>();
    public override void _Ready()
    {
        GD.Print("ON ReADY!");
        fullness = GetNode<TextureProgressBar>("Control/Health");
    }

    public Enemy(int enemyID){
        Enemies.setEnemy(this,enemyID);
    }
    public Enemy(){

    }

    private void GetHungry(){
        fullness.Value -= hungerRate;
        if(fullness.Value <= 0){
            GameState.Instance.FightOver(false); // lose fight because became fully hungry
        }
    }

    public void Eat(int satiation){
        fullness.Value += satiation;
        if(IsFighting && fullness.Value == fullness.MaxValue){
            IsFighting = false;
            GameState.Instance.FightOver(true); //win fight because satiated the hunger
        }
    }

    public void StopFight(){
        IsFighting = false;
    }

    public void HideNode(){
        Visible = false;
    }

    public void SetUpFight(int fightNumber){
        GD.Print("Setting up fight" + fightNumber);
        fullness.MaxValue = 100;
        fullness.Value = 30;
    }
    
}
