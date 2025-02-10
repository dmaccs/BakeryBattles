using Godot;

namespace FoodFight;
public static class EventRewards{

public static void GetRewards(int RewardsID){ //TODO: Put functionality for many rewards into here
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
        default:
            GameState.Instance.FinishedEncounter();
            break;
    }

}
}
