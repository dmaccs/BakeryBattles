using Godot;
using System;

namespace FoodFight;
public partial class Encounter : Node2D, GameScene
{
    private Enemy enemy;
    public override void _Ready()
    {
        enemy = GetNode<Enemy>("Enemy");
        
    }

    public void _on_button_pressed(){
        GD.Print("FIGHT START!");
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
    
        Show();
        int nextFight = getNextFight(1);
        InitFight(1);
    }

    private void InitFight(int fightNum){
        Enemies.setEnemy(enemy, fightNum);
    }

    private void HandleTick(){
        GD.Print("We be ticking");
    }

}
