using Godot;

namespace FoodFight; //TODO: make a GameScene interface for all GameScenes to implement

public partial class Kitchen : Node2D, GameScene//, GameScene  //MAKE THIS A FOOD TRUCK SO YOU ARE DRIVING YOUR FOOD TRUCK THROUGH THE GAME!!!! 
{

    Button startButton;
    public KitchenStations PlayerStation;
    Enemy Opponent;

    int fightNumber = 1;

    //ObjectData ovenResource;

    //[Export]
    //public PackedScene TestOven {get; set; }

    public override void _Ready()
    {
        startButton = GetNode<Button>("Button");
        PlayerStation = GetNode<KitchenStations>("KitchenStations1");
        //ovenResource = GD.Load<ObjectData>("res://Resources/Oven.tres");
        Opponent = GetNode<Enemy>("FoodCritic");
    }

    public void _on_button_pressed()
    {
        
        startButton.Visible = false;
        StartFight();
    }

    public void InitializeFight()
    {
        // KitchenObject bakingObject1 = MakeNewOven();
        // KitchenObject bakingObject2 = MakeNewOven();
        // KitchenObject bakingObject3 = MakeNewOven();

        // PlayerStation.SetSlot(bakingObject1, 0);
        // PlayerStation.SetSlot(bakingObject2, 4); //addchild currently being ran in here (bad)
        // PlayerStation.SetSlot(bakingObject3, 6); 
        // do something
    }

    public void SetUp(){
        Show();
        GameState.Instance.Customer = Opponent;
        startButton.Visible = true;
        Opponent.SetUpFight(fightNumber);
    }

    private KitchenObject MakeNewOven(){
        KitchenObject oven = KitchenObject.CreateInstance(1);
        //AddChild(oven);
        return oven;
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
        fightNumber++;
        // if(wonFight){
        //     WinFight();
        // } else {
        //     LoseFight();
        // }
    }

    private void WinFight(){
        GD.Print("Congrats you win :)!");
    }

    public void ResetKitchen(){
        for(int i = 0; i < 10; i++){
            PlayerStation.slots[i].RemoveBakingObject();
        }
        fightNumber = 1;

        //THIS IS TEMPORARY
        Opponent.timeInterval = 1f;
        GD.Print(Opponent.timeInterval);
        Opponent.hungerRate = 20;
    }
    
    private void LoseFight(){
        GD.Print("RIP you lose :(!");
        GameState.Instance.Health--;
        if(GameState.Instance.Health <=0){
            GameState.Instance.GameOver();
        }
    }
}
