using UnityEngine;

public class InstallCoreCanvas : MonoBehaviour
{
    public Room calledRoom;
    void Start()
    {
        UIManager.Instance.InstallCore = this;
        GetComponent<Canvas>().enabled = false;
    }

    public void OpenCanvas(Room calledRoom)
    {
        GetComponentInChildren<CoreList>().CoreListUpdate();
        GetComponent<Canvas>().enabled = true;
        this.calledRoom = calledRoom;
    }

    public void CloseCanvas()
    {
        GetComponent<Canvas>().enabled = false;
        calledRoom = null;
    }
}
