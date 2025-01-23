using Godot;

namespace FoodFight;
public partial class ObjectData : Resource
{
    [Export]
    public string ItemName { get; set; }
    [Export]
    public int Width { get; set; }
    [Export]
    public Texture2D Texture { get; set; }
    [Export]
    public string ItemType { get; set; }
    [Export]
    public string Description { get; set; }

    public ObjectData(string itemName, int width, Texture2D texture, string itemType, string description)
    {
        ItemName = itemName;
        Width = width;
        Texture = texture;
        ItemType = itemType;
        Description = description;
    }

    public ObjectData(){
        
    }
}