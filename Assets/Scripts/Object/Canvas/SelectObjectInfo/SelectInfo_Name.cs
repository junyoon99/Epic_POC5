using TMPro;
using UnityEngine;

public class SelectInfo_Name : MonoBehaviour
{
    void Awake()
    {
        transform.root.GetComponent<SelectObjectInfo>().NameUpdate += TextUpdate;
    }

    void TextUpdate(string name) 
    {
        GetComponent<TextMeshProUGUI>().text = name;
    }
}
