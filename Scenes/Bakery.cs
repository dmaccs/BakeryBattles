using Godot;

namespace FoodFight;

public partial class Bakery : Node2D
{

    Button startButton;
    BakeryStations PlayerStation;
    BakeryStations EnemyStation;

    Fighter Player;

    Fighter Opponent;

    ObjectData ovenResource;

    [Export]
    public PackedScene TestOven {get; set; }

    public override void _Ready()
    {
        startButton = GetNode<Button>("Button");
        PlayerStation = GetNode<BakeryStations>("BakeryStations1");
        EnemyStation = GetNode<BakeryStations>("BakeryStations2");
        ovenResource = GD.Load<ObjectData>("res://Resources/Oven.tres");
        Player = GetNode<Fighter>("Player");
        Opponent = GetNode<Fighter>("Fighter2");
    }

    public void _on_button_pressed()
    {
        startButton.Visible = false;
        InitializeFight();
        StartFight();
    }

    public void InitializeFight()
    {
        BakingObject bakingObject1 = MakeNewOven();
        BakingObject bakingObject2 = MakeNewOven();
        BakingObject bakingObject3 = MakeNewOven();
        BakingObject bakingObject4 = MakeNewOven();
        BakingObject bakingObject5 = MakeNewOven();

        PlayerStation.SetSlot(bakingObject1, 0);
        PlayerStation.SetSlot(bakingObject2, 4);
        PlayerStation.SetSlot(bakingObject3, 6);  
        EnemyStation.SetSlot(bakingObject4,1);
        EnemyStation.SetSlot(bakingObject5,8);      
        // do something
    }

    private BakingObject MakeNewOven(){
        BakingObject theOven = TestOven.Instantiate<BakingObject>();
        theOven.InitObject(ovenResource);
        return theOven;
    }

    public void StartFight()
    {
        for(int i = 0; i < 10; i++){ //start player items
            if(PlayerStation.slots[i].spaceUsed){
                if(PlayerStation.slots[i].bakingObject != null){
                    PlayerStation.slots[i].bakingObject.StartFight("bam!",Player);
                    i+= PlayerStation.slots[i].bakingObject.objectData.Width - 1;
                }
            }
        }
        for(int i = 0; i < 10; i++){ //start player items
            if(EnemyStation.slots[i].spaceUsed){
                if(EnemyStation.slots[i].bakingObject != null){
                    EnemyStation.slots[i].bakingObject.StartFight("bang!", Opponent);
                    i+= EnemyStation.slots[i].bakingObject.objectData.Width - 1;
                }
            }
        }

        Player.StartFight(Opponent);
        Opponent.StartFight(Player);        

        
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
        for(int i = 0; i < 10; i++){ //start player items
            if(EnemyStation.slots[i].spaceUsed){
                if(EnemyStation.slots[i].bakingObject != null){
                    EnemyStation.slots[i].bakingObject.StopFight();
                    i+= EnemyStation.slots[i].bakingObject.objectData.Width - 1;
                }
            }
        }

        Player.StopFight();
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
