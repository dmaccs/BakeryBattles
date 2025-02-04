using System.Collections.Generic;


namespace FoodFight
{
    public static class ScenePaths
    {
        private static Dictionary<int, string> ScenePathDict = new Dictionary<int, string>
        {
            {-1, "res://Scenes/TitleScreen.tscn"},
            { 0, "res://Scenes/first_stop.tscn"},
            { 1, "res://Scenes/next_stop.tscn"},
            { 2, "res://Scenes/initial_encounter.tscn"},
            { 3, "res://Scenes/game_over.tscn"},
            { 4, "res://Scenes/BattleTransition.tscn"},
            { 5, "res://Scenes/kitchen.tscn"},
            { 6, "res://Scenes/shop.tscn"},
            { 7, "res://Scenes/event_scene.tscn"},
            // Add more scenes here
        };

        public static string GetScenePath(int id)
        {
            if (ScenePathDict.TryGetValue(id, out string ScenePath))
            {
                return ScenePath;
            }
            return "";
        }
    }
}