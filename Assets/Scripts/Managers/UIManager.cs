using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    static UIManager _instance;

    public SelectObjectInfo SelectObjectInfo;
    public InstallCoreCanvas InstallCore;

    public static UIManager Instance 
    {
        get 
        {
            if (!_instance)
            {
                _instance = FindFirstObjectByType(typeof(UIManager)) as UIManager;
                if (!_instance) 
                {
                    Debug.Log("UIManager ¾øÀ½!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else 
        {
            Destroy( this );
        }
    }

    public void OverInfoState(CanSelectObject targetObject)
    {
        SelectObjectInfo.CurrentObject = targetObject;
        SelectObjectInfo.ProfileIMGUpdate.Invoke("Canvas/Images/Default");
        Instance.SelectObjectInfo.HealthBarUpdate.Invoke(targetObject.CurrentHealth, targetObject.MaxHealth);
        Instance.SelectObjectInfo.NameUpdate.Invoke(targetObject.name);

        Dictionary<string, string> Stats = new Dictionary<string, string>();
        Stats[nameof(targetObject.AttackPower)] = targetObject.AttackPower.ToString();
        Stats[nameof(targetObject.targetObject)] = targetObject.targetObject != null ? targetObject.targetObject.ToString() : "null";
        if (targetObject.TryGetComponent<CanMoveObjects>(out CanMoveObjects moveObject))
        {
            Stats[nameof(moveObject.moveSpeed)] = moveObject.moveSpeed.ToString();
        }

        SelectObjectInfo.StateUpdate.Invoke(Stats);
        SelectObjectInfo.GetComponent<Canvas>().enabled = true;
    }
}
