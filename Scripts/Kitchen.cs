using Godot;

namespace FoodFight;

public partial class Kitchen : Node2D, GameScene // TODO: MAKE THIS A FOOD TRUCK SO YOU ARE DRIVING YOUR FOOD TRUCK THROUGH THE GAME!!!! 
{

    Button startButton;
    //public KitchenStations PlayerStation;
    Enemy Opponent;

    int fightNumber = 1;

    public override void _Ready()
    {
        startButton = GetNode<Button>("Button");
        //PlayerStation = GetNode<KitchenStations>("KitchenStations1");
    }

    public void _on_button_pressed()
    {
        GD.Print("Going back");
    }

    public void InitializeFight()
    {

    }

    public void SetUp(){
        Show();
        startButton.Visible = true;
    }

    public void ResetKitchen(){
        for(int i = 0; i < 10; i++){
            //PlayerStation.slots[i].RemoveBakingObject();
        }
    }
}
