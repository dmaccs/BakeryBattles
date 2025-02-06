using Godot;

namespace FoodFight;
public class EventRewards{

public void GetRewards(int RewardsID){ //TODO: Put functionality for many rewards into here
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
