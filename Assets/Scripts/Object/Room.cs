using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Door> inDoors;
    public CoreFacilities Core;
    private TextMeshPro tmp;

    private void Awake()
    {
        if (GetComponentInChildren<TextMeshPro>())
        {
            tmp = GetComponentInChildren<TextMeshPro>();
        }
    }
    // πÊ ¿‘∞∂
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CanSelectObject>(out CanSelectObject moveObject))
        {
            moveObject.currentRoom = this;
            moveObject.NoticeChangeRoom?.Invoke();
            
        }
        if (collision.TryGetComponent<CoreFacilities>(out CoreFacilities Core))
        {
            this.Core = Core;
            CheckCoreInCurrentRoom();
        }
    }

    public void CheckCoreInCurrentRoom()
    {
        if (Core != null)
        {
            tmp.enabled = false;
        }
        else
        {
            tmp.enabled = true;
        }
    }
}
