using System;
using System.Collections.Generic;
using System.IO;
using Godot;

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
            { 8, "res://Scenes/encounter.tscn"},
            { 9, "res://Scenes/kitchen_stations.tscn"}
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

        public static Dictionary<string, int> GetSceneEnum()
        {
            var sceneEnum = new Dictionary<string, int>();
            foreach (var kvp in ScenePathDict)
            {
                string sceneName = Path.GetFileNameWithoutExtension(kvp.Value).Replace(" ", "");
                sceneEnum[sceneName] = kvp.Key;
            }
            return sceneEnum;
            
        }
    }
    //make a function that loops from 1-100 and prints hello x times for each iteration where x is the current iteratino number 
}