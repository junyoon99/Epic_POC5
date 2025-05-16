using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image fill;
    public CanSelectObject parent;
    public void Init()
    {
        fill = GetComponent<Image>();
        parent.HealthUpdate += HealthUpdate;
    }

    private void Start()
    {
       
    }

    void HealthUpdate() 
    {
        fill.fillAmount = parent.CurrentHealth/parent.MaxHealth;
    }
}
