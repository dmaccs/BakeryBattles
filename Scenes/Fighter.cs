using Godot;

namespace FoodFight;
public partial class Fighter : Node2D
{
    private Timer timer;
    private float timeInterval = 3f;

    public int attackDamage{get; set;} = 5;

    private Fighter Opponent;

    TextureProgressBar health;
    public override void _Ready()
    {
        health = GetNode<TextureProgressBar>("Control/Health");
    }
    public void StartFight(Fighter opponent){

        Opponent = opponent;
        timer = new Timer();
        AddChild(timer);
        timer.OneShot = false;

        // Connect the timeout signal to the TimesUp function
        timer.Timeout += Attack;

        // Start the timer
        timer.Start();
    }

    private void Attack(){
        Opponent.GetHit(attackDamage);
    }

    public void GetHit(int damage){
        health.Value -= damage;
        if(health.Value <= 0){
            health.Value = 0;
            GameState.Instance.FightOver(this);
            
        }
    }

    public void StopFight(){
        timer.Stop();
    }
    
}
