using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectInfo_HealthBar : MonoBehaviour
{
    Transform Fill;
    Transform Text;
    private void Awake()
    {
        Fill = transform.GetChild(1);
        Text = transform.GetChild(2);

        transform.root.GetComponent<SelectObjectInfo>().HealthBarUpdate += HealthBarUpdate;
    }

    void HealthBarUpdate(float currentHealth, float maxHealth)
    {
        Fill.GetComponent<Image>().fillAmount = currentHealth / maxHealth;
        Text.GetComponent<TextMeshProUGUI>().text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
