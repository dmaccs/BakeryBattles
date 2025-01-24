using Godot;

namespace FoodFight;
public partial class KitchenObject : Node2D
{
    [Export]
    public ObjectData objectData;

    private Timer timer;
    private float timeInterval = 5f;

    private string WarCry;

    private Enemy Customer;

    public override void _Ready()
    {
        if (objectData != null && objectData.Texture != null)
        {
            // Initialize sprite
            sprite = GetNode<Sprite2D>("Sprite2D");
            sprite.Texture = objectData.Texture;
            sprite.Position = new Vector2(objectData.Width * 50 / 2, 25);
            Vector2 scale = new Vector2(objectData.Width * 50 / objectData.Texture.GetSize().X, 50 / objectData.Texture.GetSize().Y);
            sprite.Scale = scale;
        }
    }

    public void StartFight(string warCry, Enemy customer){
        timer = new Timer();
        AddChild(timer);
        Customer = customer;
        WarCry = warCry;

        // Set the timer's wait time (X seconds)
        timer.WaitTime = timeInterval;
        
        // Set it to repeat
        timer.OneShot = false;

        // Connect the timeout signal to the TimesUp function
        timer.Timeout += DoFunction;

        // Start the timer
        timer.Start();
    }

    public virtual void DoFunction(){
        GD.Print(WarCry);
        Customer.Eat(3);
    }

    public void StopFight(){
        timer.Stop();
    }

    

    public void SetSlotPosition(int slotPos)
    {
        sprite.Position = new Vector2(objectData.Texture.GetSize().X / 2, 25);
    }
    Sprite2D sprite;

    public KitchenObject(ObjectData newObjectData)
    {
        objectData = newObjectData;
        InitializeBakingObject();
    }

    public KitchenObject(){
        
    }

    public void InitObject(ObjectData newObjectData){
        objectData = newObjectData;
        InitializeBakingObject();
    }

    private void InitializeBakingObject()
    {
        // Initialize sprite
        sprite = GetNode<Sprite2D>("Sprite2D");
        sprite.Texture = objectData.Texture;
        sprite.Position = new Vector2(objectData.Width * 50 / 2, 25);
        Vector2 scale = new Vector2(objectData.Width * 50 / objectData.Texture.GetSize().X, 50 / objectData.Texture.GetSize().Y);
        sprite.Scale = scale;
        //time_interval = x seconds;
    }


}
