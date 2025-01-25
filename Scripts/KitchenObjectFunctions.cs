using Godot;

namespace FoodFight
{
    public partial class Oven : ObjectData
    {
        public Oven(int id, string itemName, int width, Texture2D texture, string itemType, string description)
            : base(id,itemName, width, texture, itemType, description) { }

        public override void Use()
        {
            GD.Print("Using the oven to bake something.");
            // Add specific functionality for the oven here
            }
        }

    public partial class Blender : ObjectData
    {
        public Blender(int id, string itemName, int width, Texture2D texture, string itemType, string description)
        : base(id, itemName, width, texture, itemType, description) { }
        public override void Use()
        {
            GD.Print("Using the blender to blend something.");
            GameState.Instance.Customer.Eat(3);
        }
    }

    public partial class Toaster : ObjectData
    {
        public Toaster(int id, string itemName, int width, Texture2D texture, string itemType, string description)
        : base(id, itemName, width, texture, itemType, description) { }
        public override void Use()
        {
            GD.Print("Using the toaster to toast something.");
        }
    }
}