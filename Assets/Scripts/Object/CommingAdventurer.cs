using UnityEngine;
using UnityEngine.UI;

public class CommingAdventurer : MonoBehaviour
{
    Image fill;
    public float CommingTime;
    float elapsedTime;
    public int number;
    void Start()
    {
        fill = transform.GetChild(0).GetChild(1).GetComponent<Image>();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= CommingTime)
        {
            GameManager.Instance.SpawnAdventurer(transform.position, number);
            Destroy(gameObject);
        }

        UpdateFill((CommingTime - elapsedTime) / CommingTime);
    }

    public void UpdateFill(float value)
    {
        fill.fillAmount = value;
    }
}
