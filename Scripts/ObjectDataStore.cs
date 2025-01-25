using System.Collections.Generic;
using Godot;

namespace FoodFight
{
    public static class ObjectDataStore
    {
        private static Dictionary<int, ObjectData> objectDataDictionary = new Dictionary<int, ObjectData>
        {
            { 0, new Oven(0,"Oven", 2, LoadTexture("res://Sprites/Oven.png"), "Appliance", "A kitchen oven.") },
            { 1, new Blender(1,"Blender", 1, LoadTexture("res://Sprites/Blender.png"), "Appliance", "A kitchen blender.") },
            { 2, new Toaster(2,"Toaster", 1, LoadTexture("res://Sprites/Toaster.png"), "Appliance", "A kitchen toaster.") },
            // Add more objects here
        };

        public static ObjectData GetObjectData(int id)
        {
            if (objectDataDictionary.TryGetValue(id, out var objectData))
            {
                return objectData;
            }
            return null;
        }

        private static Texture2D LoadTexture(string path)
        {
            return (Texture2D)ResourceLoader.Load(path);
        }
    }
}