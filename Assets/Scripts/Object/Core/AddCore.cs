using UnityEngine;

public class AddCore : MonoBehaviour
{
    public Room GetCurrentRoom() 
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        foreach(Collider2D col in cols)
        {
            Debug.Log(col.gameObject.name);
            if (col.TryGetComponent<Room>(out Room room))
            {
                return room;
            }
        }
        return null;
    }
}
