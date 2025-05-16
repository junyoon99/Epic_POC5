using UnityEngine;
using UnityEngine.EventSystems;
public class ClosePanel : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
           transform.root.GetComponent<Canvas>().enabled = false;
        }
    }
}
