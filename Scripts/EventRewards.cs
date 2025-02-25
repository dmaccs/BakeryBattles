using System;
using Godot;

namespace FoodFight;
public static class EventRewards{

public static void GetRewards(int RewardsID){ //TODO: Put functionality for many rewards into here
    GameState gameState = GameState.Instance;
    switch (RewardsID){
        case 0:
            GameState.Instance.FinishedEncounter();
            break;
        case 1:
            GameState.Instance.ChangeCoins(50);
            break;
        case 2:
            GD.Print("WHOOOPS!");
            break;
        case 3:
            GD.Print("50% chance combat 50% chance they give you something");
            if(GameState.Instance.RandomNumberGenerator.Next() % 2 == 0){
                GameState.Instance.ChangeCoins(30);
            } else {
                GameState.Instance.LoadEncounter(8); //8 is encounter TODO: Turn scenes into a public Enum so i can write ENCOUNTER
            }
            break;
        case 4:
            GD.Print("You ready your appliances...");
            if(GameState.Instance.RandomNumberGenerator.Next() % 2 == 0){
                GD.Print("but nothing can be seen");
            } else {
                GD.Print("And a monster jumps out!"); //TODO: start fight with some advantage
                GameState.Instance.LoadEncounter(8); //8 is encounter TODO: Turn scenes into a public Enum so i can write ENCOUNTER
            }
            break;
        case 5:
            GD.Print("You put money in and ... ");
            if(GameState.Instance.RandomNumberGenerator.Next() % 2 == 0){
                gameState.ChangeCoins(-18);
                GD.Print(" the machine seems to jam");
            } else {
                GD.Print("You find a random candy!");
                //TODO: Do something ehre
            }
            break;
        case 6:
            GD.Print("Upgraded applicance!");
            break;
        case 7:
            GD.Print("your car is shiny!"); //cost few 
            break;
        case 8:
            GD.Print("Your car is cleaned and your replenish a heart"); //cost med
            gameState.Health= Math.Min(5,gameState.Health + 1);
            break;
        case 9:
            GD.Print("with a perfectly clean car you are ready for travels! gain a permannet heart"); // cost a lot
            gameState.Health += 1; //TODO: Set a max health that can change and increase it here
            break;
        case 10:
            GD.Print("Without being payed the eerie looking carwashers can't hold back their hunger!");
            gameState.LoadEncounter(8);// start with disadvantage // car wash
            break;
        case 11:
            GD.Print("You lose appliance 1");
            break;
        case 12:
            GD.Print("You lose appliance 2");
            break;
        case 13:
            if(gameState.RandomNumberGenerator.Next() % 2 == 0){
                GD.Print("you barely make it up the hill... that was lucky");
            } else {
                GD.Print("Your engine blows! You will need to find another way around this...");
                gameState.Health--;
            }
            break;
        case 14:
        case 15:
        default:
            GameState.Instance.FinishedEncounter();
            break;
    }

}
}
