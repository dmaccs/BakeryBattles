using Godot;

namespace FoodFight;
public partial class Enemy : Node2D
{
    private Timer timer;
    private float timeInterval = 3f;

    public int hungerRate = 5;
    TextureProgressBar fullness;
    public override void _Ready()
    {
        fullness = GetNode<TextureProgressBar>("Control/Health");
    }
    public void StartFight(){

        timer = new Timer();
        AddChild(timer);
        timer.OneShot = false;

        // Connect the timeout signal to the TimesUp function
        timer.Timeout += GetHungry;

        // Start the timer
        timer.Start();
    }

    private void GetHungry(){
        fullness.Value -= hungerRate;
        if(fullness.Value <= 0){
            GameState.Instance.FightOver(false); // lose fight because became fully hungry
        }
    }

    public void Eat(int satiation){
        fullness.Value += satiation;
        if(fullness.Value == fullness.MaxValue){
            GameState.Instance.FightOver(true); //win fight because satiated the hunger
        }
    }

    public void StopFight(){
        timer.Stop();
    }
    
}
