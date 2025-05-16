using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CoreListElement : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(transform.root.GetComponent<InstallCoreCanvas>().calledRoom);
        print(core);
        transform.root.GetComponent<InstallCoreCanvas>().calledRoom.InstallCore(core);
        print(price);
    }

    Image profileIMG;
    TextMeshProUGUI coreName;
    TextMeshProUGUI corePrice;
    GameObject core;
    int price;

    public void Init(Sprite IMG, string name, int price, GameObject core)
    {
        profileIMG = transform.GetChild(0).GetComponent<Image>();
        coreName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        corePrice = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();

        profileIMG.sprite = IMG;
        coreName.text = name;
        corePrice.text = price.ToString();
        this.price = price;
        this.core = core;
    }
}
