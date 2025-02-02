using Godot;

namespace FoodFight;

public partial class TitleScreen : Node2D, GameScene
{
    public override void _Ready()
    {  
    }

    public void _on_settings_pressed(){
        GD.Print("Open Settings");
    }

    public void _on_play_pressed(){
        GameState.Instance.ResetGame();
        GameState.Instance.StartGame();
        Hide();
    }

    public void SetUp()
    {
       GD.Print("setting up title screen");
       Show();
    }
}
