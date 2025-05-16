using UnityEngine;

public class TimeCore : CoreFacilities
{
    private void Awake()
    {
        Init();
        MaxHealth = 2000;
        CurrentHealth = MaxHealth;
    }
    private void OnEnable()
    {
        GameManager.Instance.TimeCores.Add(gameObject);
    }

    private void OnDisable()
    {
        if (currentRoom != null)
        {
            currentRoom.Core = null;
            currentRoom.CheckCoreInCurrentRoom();
        }
        GameManager.Instance.TimeCores.Remove(gameObject);
    }
}
