using System.Collections.Generic;
using Godot;

namespace FoodFight;
public static class Enemies{

public static void setEnemy(Enemy enemy, int EnemyID){ //TODO: Put functionality for many rewards into here
    GD.Print(EnemyID);
    switch (EnemyID){
        case 0:
            enemy.hungerRate = 20;
            enemy.maxFullness = 100;
            break;
        case 1:
            enemy.hungerRate = 30;
            enemy.maxFullness = 150;
            break;
        case 2:
            enemy.hungerRate = 40;
            enemy.maxFullness = 200;
            break;
        default:
            break;
    }
    GD.Print("Set up enemy with " + enemy.hungerRate + " hunger rate and " + enemy.maxFullness + " max fullness");

}
}
