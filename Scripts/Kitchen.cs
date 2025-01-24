using Godot;

namespace FoodFight;

public partial class Kitchen : Node2D
{

    Button startButton;
    KitchenStations PlayerStation;
    Enemy Opponent;

    ObjectData ovenResource;

    [Export]
    public PackedScene TestOven {get; set; }

    public override void _Ready()
    {
        startButton = GetNode<Button>("Button");
        PlayerStation = GetNode<KitchenStations>("KitchenStations1");
        ovenResource = GD.Load<ObjectData>("res://Resources/Oven.tres");
        Opponent = GetNode<Enemy>("FoodCritic");
    }

    public void _on_button_pressed()
    {
        startButton.Visible = false;
        InitializeFight();
        StartFight();
    }

    public void InitializeFight()
    {
        KitchenObject bakingObject1 = MakeNewOven();
        KitchenObject bakingObject2 = MakeNewOven();
        KitchenObject bakingObject3 = MakeNewOven();

        PlayerStation.SetSlot(bakingObject1, 0);
        PlayerStation.SetSlot(bakingObject2, 4);
        PlayerStation.SetSlot(bakingObject3, 6);    
        // do something
    }

    private KitchenObject MakeNewOven(){
        KitchenObject theOven = TestOven.Instantiate<KitchenObject>();
        theOven.InitObject(ovenResource);
        return theOven;
    }

    public void StartFight()
    {
        for(int i = 0; i < 10; i++){ //start player items
            if(PlayerStation.slots[i].spaceUsed){
                if(PlayerStation.slots[i].bakingObject != null){
                    PlayerStation.slots[i].bakingObject.StartFight("bam!",Opponent);
                    i+= PlayerStation.slots[i].bakingObject.objectData.Width - 1;
                }
            }
        }

        Opponent.StartFight();        

        
    }

    public void StopFight(bool wonFight){
        for(int i = 0; i < 10; i++){ //start player items
            if(PlayerStation.slots[i].spaceUsed){
                if(PlayerStation.slots[i].bakingObject != null){
                    PlayerStation.slots[i].bakingObject.StopFight();
                    i+= PlayerStation.slots[i].bakingObject.objectData.Width - 1;
                }
            }
        }

        Opponent.StopFight();
        if(wonFight){
            WinFight();
        } else {
            LoseFight();
        }
    }

    private void WinFight(){
        GD.Print("Congrats you win :)!");
    }
    
    private void LoseFight(){
        GD.Print("RIP you lose :(!");
        GameState.Instance.Health--;
        if(GameState.Instance.Health <=0){
            GameState.Instance.GameOver();
        }
    }
}
