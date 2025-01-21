using Godot;
using System;

public partial class Bakery : Node2D
{

    Button startButton;
    BakeryStations PlayerStation;
    BakeryStations EnemyStation;

    ObjectData ovenResource;

    [Export]
    public PackedScene TestOven {get; set; }

    public override void _Ready()
    {
        startButton = GetNode<Button>("Button");
        PlayerStation = GetNode<BakeryStations>("BakeryStations1");
        EnemyStation = GetNode<BakeryStations>("BakeryStations2");
        ovenResource = GD.Load<ObjectData>("res://Resources/Oven.tres");
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
        // AddChild(bakingObject1);
        // AddChild(bakingObject2);
        // AddChild(bakingObject3);
        // AddChild(bakingObject4);
        // AddChild(bakingObject5);
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
        // do something else
    }
}
