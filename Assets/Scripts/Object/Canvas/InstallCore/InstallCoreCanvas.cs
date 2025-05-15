using UnityEngine;

public class InstallCoreCanvas : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.InstallCore = this;
        GetComponent<Canvas>().enabled = false;
    }
}
