using UnityEngine;

public class HealingCore : CoreFacilities
{
    float healAmount = 10;
    private void Awake()
    {
        Init();
        MaxHealth = 3000;
        CurrentHealth = MaxHealth;
    }

    public override void CoreFunction(CanMoveObjects user)
    {
        base.CoreFunction(user);
        user.Heal(healAmount);
    }

    private void OnDisable()
    {
        if (currentRoom != null)
        {
            currentRoom.Core = null;
            currentRoom.CheckCoreInCurrentRoom();
        }
    }
}
