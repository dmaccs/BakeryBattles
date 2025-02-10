namespace FoodFight;
public abstract class EnemyAbility{
    public string AbilityName = "default name";
    protected Enemy Owner;

    int abilityCount = 0;

    public void SetOwner(Enemy owner){
        Owner = owner;
    }

    public void AbilityCount(int count){
        abilityCount = count;
    }

    public abstract void Activate();
}

public class SlowStart : EnemyAbility
{
    int remaining = 20;
    bool activated = false;
    public override void Activate()
    {
        remaining--;
        if(remaining <=0){
            if(!activated){
                Owner.hungerRate *= 2;
            }
            activated = true;
            
        }
    }
}