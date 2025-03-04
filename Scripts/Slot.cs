using Godot;

namespace FoodFight;

public partial class Slot : Node2D
{
    [Export]
    public KitchenObject bakingObject;
    public bool spaceUsed = false;
    
    public override void _Ready(){
        if(bakingObject != null){
            spaceUsed = true;
            bakingObject.Position = Position;
        }
    }
    public bool AddBakingObject(KitchenObject newBakingObject)
    {
        if (bakingObject == null)  // Only add if slot is empty
        {
            bakingObject = newBakingObject;
            //newBakingObject.Position = Position;  // Position it in the slot (optional adjustment)
            // DON'T DO THIS CUZ IT RUNS MULTIPLE TIMES CURRENTLY AddChild(newBakingObject);  // Add the BakingObject as a child of this slot
            spaceUsed = true;
            bakingObject.ZIndex = 1;
            return true;
        }
        return false;
    }

    public void RemoveBakingObject(){
        if(bakingObject != null){
            bakingObject.QueueFree();
        }
        bakingObject = null;
        spaceUsed = false;
    }
}
