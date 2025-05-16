using UnityEngine;

public class CoreList : MonoBehaviour
{
    public void CoreListUpdate() 
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        GameObject[] Cores = Resources.LoadAll<GameObject>("Core");
        GameObject CoreElement = Resources.Load<GameObject>("Canvas/InstallCore/CoreListElement");
        foreach (GameObject core in Cores)
        {
            CoreListElement coreListElement = Instantiate(CoreElement, transform).GetComponent<CoreListElement>();
            coreListElement.Init(core.GetComponent<Sprite>(), core.name, core.GetComponent<CoreFacilities>().Price, core);
        }
    }
}
