using Godot;

public partial class BakingObject : Node2D
{
    [Export]
    public ObjectData objectData;

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

    public void SetSlotPosition(int slotPos)
    {
        sprite.Position = new Vector2(objectData.Texture.GetSize().X / 2, 25);
    }
    Sprite2D sprite;

    public BakingObject(ObjectData newObjectData)
    {
        objectData = newObjectData;
        InitializeBakingObject();
    }

    public BakingObject(){
        
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
    }


}
