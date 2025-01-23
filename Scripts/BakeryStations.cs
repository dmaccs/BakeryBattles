using Godot;
using System.Collections.Generic;

namespace FoodFight;
public partial class BakeryStations : Node2D
{

public List<Slot> slots = new(10);

    public override void _Ready(){
        for (int i = 1; i <= 10; i++)
        {
            Slot slot = GetNode<Slot>($"Slot{i}");  // Get slot by its name (slot1, slot2, etc.)
            slots.Add(slot);
        }
        for(int i = 0; i < slots.Count; i++){
            if(slots[i].spaceUsed){
                BakingObject currBakingObject = slots[i].bakingObject;
                int objectWidth = currBakingObject.objectData.Width;
                if(objectWidth > 3){
                    GD.Print("Width is too large");
                } else {
                    if(objectWidth == 3){
                        if(i >= 8){
                            GD.Print("can't place this close to end");
                            slots[i].RemoveBakingObject();
                        } else {
                            slots[i+1].RemoveBakingObject();
                            slots[i+2].RemoveBakingObject();
                        }
                    }
                    if(objectWidth == 2){
                        if(i >= 9){
                            GD.Print("can't place this close to end");
                            slots[i].RemoveBakingObject();
                        } else {
                            slots[i+1].RemoveBakingObject();
                        }
                    }
                }
            }
        }
    }

    public void SetSlot(BakingObject setObject, int slot){
        if(setObject == null){
            return;
        }
        if(setObject.objectData.Width + slot - 1 > 9){
            return;
        }
        if(slot > 9 || slot < 0){
            return;
        }
        //GD.Print("Why am i here");
        bool success = true;
        
        for(int i = 0; i < setObject.objectData.Width; i++){
            //GD.Print("current i: " + i);
            //GD.Print(setObject.objectData.Width);
            success &= slots[slot + i].AddBakingObject(setObject);
        }
        if(!success){
           for(int i = 0; i < setObject.objectData.Width; i++){
                slots[slot + i].RemoveBakingObject();
                //return false maybe;
            }
            return; 
        }
        slots[slot].AddChild(setObject); // this is a hack for getting the og thing to run
        //GD.Print("slot position = " + slots[slot].Position.ToString());
        //setObject.Position = slots[slot].Position;
        return;
        
    }

}
