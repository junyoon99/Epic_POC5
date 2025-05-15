using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthBar : MonoBehaviour
{
    Image fill;
    CanSelectObject parent;

    private void Awake()
    {
        fill = GetComponent<Image>();
        parent = transform.root.GetComponent<CanSelectObject>();
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
