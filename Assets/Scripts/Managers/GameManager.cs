using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    static GameManager _instance 
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindAnyObjectByType(typeof(GameManager)) as GameManager;
                if (Instance == null)
                {
                    Instance = _instance;
                }
            }
            return Instance;
        }
    }

    GameObject floorLayer;
    GameObject lastFloorLayer;
    public List<GameObject> TimeCores = new List<GameObject>();

    GameObject[] AllMonsters;
    GameObject[] AllAdventurer;
    GameObject CommingAdventurer;

    int wave = 0;

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
        AllMonsters = Resources.LoadAll<GameObject>("Monster");
        AllAdventurer = Resources.LoadAll<GameObject>("Adventurer");
        CommingAdventurer = Resources.Load<GameObject>("CommingAdventurer");
    }

    public void SpawnNewFloorLayer() 
    {
        GameObject SpawnedFloor = Instantiate(floorLayer, lastFloorLayer.transform.position + new Vector3(0,5,0), Quaternion.identity);
        lastFloorLayer = SpawnedFloor;
        RoomManager.Instance.UpdateDoor();
    }

    public void TimeStop() 
    {
        if (Time.timeScale != 0 && TimeCores.Count > 0) 
        {
            Time.timeScale = 0;
            return;
        }
        else if (TimeCores.Count == 0)
        {
            ShowText spawnedText = Instantiate(Resources.Load<GameObject>("ShowText")).GetComponent<ShowText>();
            spawnedText.UpdateText("Need TimeCore");
            spawnedText.fontSize(10, 0);
            spawnedText.GetComponent<TextMeshPro>().color = Color.red;
            return;
        }

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            return;
        }
    }

    public void SpawnRandomMonster() 
    {
        Instantiate(AllMonsters[Random.Range(0, AllMonsters.Count())] , new Vector3(0, 10, 0), Quaternion.identity);
    }

    public void SpawnWave() 
    {
        wave++;
        int count = wave;
        while (count > 0) 
        {
            int SpawnNumber = Random.Range(1, count);
            count -= SpawnNumber;
            Instantiate(CommingAdventurer, RoomManager.Instance.AllDoors[Random.Range(0, RoomManager.Instance.AllDoors.Count)].currentRoom.transform.position, Quaternion.identity).GetComponent<CommingAdventurer>().number = SpawnNumber;
        }
    }

    public void SpawnAdventurer(Vector2 spawnPosition, int count)
    {
        StartCoroutine(SpawnDelay(0.5f, spawnPosition, count));
    }

    IEnumerator SpawnDelay(float delay, Vector2 spawnPosition, int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(delay);
            Instantiate(AllAdventurer[Random.Range(0, AllAdventurer.Count())], spawnPosition, Quaternion.identity);
        }
    }
}
