using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace FoodFight;
public partial class Enemy : Node2D
{
    public int hungerRate = 20;
    public int maxFullness = 100;
    public TextureProgressBar fullness;
    double actualFullness = 0;
    bool IsFighting = false;

    private double startingFullness = 30;

    List<EnemyAbility> Abilities = new List<EnemyAbility>();
    public override void _Ready()
    {
        GD.Print("ON ReADY!");
        fullness = GetNode<TextureProgressBar>("Control/Health");
    }

    public Enemy(int enemyID){
        Enemies.setEnemy(this,enemyID);
        fullness.MaxValue = maxFullness;
    }
    public Enemy(){

    }

    private void GetHungry(){
        fullness.Value -= hungerRate/10;
        actualFullness -= hungerRate/10;
    }

    public void Eat(int satiation){
        fullness.Value += satiation;
        actualFullness += satiation;
    }

    public void HideNode(){
        Visible = false;
    }

    public void ActivateEffects(){
        GetHungry();
    }

    public void CheckResults(){ //TODO: Pool all of the abilites into a single class so instead of directly affecting the enemy and calling Eat over and over we calculate 
                                // The total change in the health bar as well as change in statuses so we only need to do one change to the enemy and then check results
        if(IsFighting && actualFullness >= fullness.MaxValue){
            fullness.Value = fullness.MaxValue;
            IsFighting = false;
            GameState.Instance.FightOver(true); //win fight because satiated the hunger
        } else if(fullness.Value <= 0){
            IsFighting = false;
            GameState.Instance.FightOver(false); // lose fight because became fully hungry
        }
        if(!IsFighting){
            GD.Print("Stopped fighting");
        }
    }

    public void StartFight(){
        IsFighting = true;
        actualFullness = fullness.Value;
    }

    public void ResetValues(){
        actualFullness = fullness.Value = startingFullness;
    }
    
}
