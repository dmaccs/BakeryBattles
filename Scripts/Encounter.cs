using Godot;

namespace FoodFight;
public partial class Encounter : Node2D
{
    private Timer timer;
    public float timeInterval = 1f;

    public int hungerRate = 20;
    TextureProgressBar fullness;
    bool IsFighting = false;
    public override void _Ready()
    {
        fullness = GetNode<TextureProgressBar>("Control/Health");
    }
    public void StartFight(){
        IsFighting = true;
        if(timer == null){
            timer = new Timer();
            AddChild(timer);
            timer.OneShot = false;
            // Connect the timeout signal to the TimesUp function
            timer.Timeout += GetHungry;
        }
        // Start the timer
        timer.Start(timeInterval);
        GD.Print("Starting timer: " + timeInterval);
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
        timer.Stop();
    }

    public void HideNode(){
        Visible = false;
    }

    public void SetUpFight(int fightNumber){
        GD.Print("Setting up fight" + fightNumber);
        fullness.MaxValue = 100;
        fullness.Value = 30;
        timeInterval = timeInterval/fightNumber;
        hungerRate = 20 + fightNumber;
    }
    
}
