using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectInfo_ShowStateArea : MonoBehaviour
{
    GameObject statePrefab;
    private void Awake()
    {
        transform.root.GetComponent<SelectObjectInfo>().StateUpdate += UpdateState;
        statePrefab = Resources.Load<GameObject>("Canvas/SelectObjectInfo/State");
    }

    void UpdateState(Dictionary<string, string> stats) 
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        foreach (KeyValuePair<string, string> kvp in stats) 
        {
            GameObject SpawnedObject = Instantiate(statePrefab, transform);
            SpawnedObject.GetComponent<TextMeshProUGUI>().text = kvp.Key + " : " + kvp.Value;
        }
    }
}
