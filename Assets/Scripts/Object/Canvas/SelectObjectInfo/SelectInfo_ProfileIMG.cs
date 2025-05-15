using UnityEngine;
using UnityEngine.UI;

public class SelectInfo_ProfileIMG : MonoBehaviour
{
    Image profileIMG;
    private void Awake()
    {
        profileIMG = GetComponent<Image>();
        transform.root.GetComponent<SelectObjectInfo>().ProfileIMGUpdate += ProfileIMGUpdate;
    }

    void ProfileIMGUpdate(string path) 
    {
        profileIMG.sprite = Resources.Load<Sprite>(path);
    }
}
