using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    GameObject floorLayer;
    GameObject lastFloorLayer;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        floorLayer = Resources.Load<GameObject>("FloorLayer");
        lastFloorLayer = GameObject.Find("FloorLayer");
    }

    public void SpawnNewFloorLayer() 
    {
        GameObject SpawnedFloor = Instantiate(floorLayer, lastFloorLayer.transform.position + new Vector3(0,5,0), Quaternion.identity);
        lastFloorLayer = SpawnedFloor;
        RoomManager.Instance.UpdateDoor();
    }
}
