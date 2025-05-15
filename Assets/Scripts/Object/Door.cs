using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Room currentRoom { get; private set; }
    public List<Door> connectedDoor { get; private set; } = new List<Door>();

    public int doorNumber;

    private void OnEnable()
    {
        RoomManager.Instance.AllDoors.Add(this);
    }

    private void OnDisable()
    {
        if (RoomManager.Instance != null) 
        {
            RoomManager.Instance.AllDoors.Remove(this);
        }
    }

    // Ήζ ΐΤ°Ά
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Room>(out Room currentRoom)) 
        {
            currentRoom.inDoors.Add(this);
            this.currentRoom = currentRoom;
        }
    }

    // Επ°Ά
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Room>(out Room beforeRoom))
        {
            beforeRoom.inDoors.Remove(this);
        }
    }

    public void DoorEnter(Transform user, int index) 
    {
        user.position = connectedDoor[index].transform.position;
    }

    public List<Door> GetConnectedDoor() 
    {
        return connectedDoor;
    }

    public void SetConnectDoor(Door door) 
    {
        connectedDoor.Add(door);
    }
}
