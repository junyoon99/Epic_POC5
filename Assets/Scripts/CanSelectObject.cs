using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class CanSelectObject : MonoBehaviour
{
    public Room currentRoom;
    public GameObject targetObject { get; protected set; }
    protected Vector2? targetPosition;
    protected GameObject targetCore;
    public List<Tuple<Door, int>> Path = new List<Tuple<Door, int>>();

    public float MaxHealth { get; protected set; }
    public float CurrentHealth { get; protected set; }
    public float AttackPower { get; protected set; }
    protected Vector2 attackRange;

    public Action NoticeChangeRoom;
    public Action HealthUpdate;
    public Action ObjectisDead;

    protected GameObject ShowTextPrefab;

    virtual protected void Init() 
    {
        ShowTextPrefab = Resources.Load<GameObject>("ShowText");
        HealthBar healthbar = Instantiate(Resources.Load<Canvas>("Canvas/IngameState/ShowIngameState"), transform).GetComponentInChildren<HealthBar>();
        healthbar.parent = this;
        healthbar.Init();
        ObjectisDead += Dead;
    }

    virtual public void TakeDamage(CanSelectObject attackObject)
    {
        CurrentHealth -= attackObject.AttackPower;
        GameObject SpawnedDamage = Instantiate(ShowTextPrefab, transform.position, Quaternion.identity);
        ShowText textScript = SpawnedDamage.GetComponent<ShowText>();
        textScript.UpdateText("-" + attackObject.AttackPower.ToString());
        textScript.fontSize(10, attackObject.AttackPower);
        HealthUpdate?.Invoke();
        if (UIManager.Instance.SelectObjectInfo.CurrentObject == this)
        {
            UIManager.Instance.OverInfoState(this);
        }
        if (CurrentHealth <= 0) 
        {
            CurrentHealth = 0;
            attackObject.GetComponent<CanSelectObject>().Resettarget();
            attackObject.targetObject = null;
            NoticeChangeRoom = null;
            ObjectisDead.Invoke();
        }
    }

    virtual public void Dead() 
    {
        Resettarget();
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }

    virtual public void Heal(float value)
    {
        CurrentHealth += value;
        CurrentHealth = Mathf.Clamp(CurrentHealth, -999, MaxHealth);
        GameObject SpawnedText = Instantiate(ShowTextPrefab, transform.position, Quaternion.identity);
        ShowText showTextScript = SpawnedText.GetComponent<ShowText>();
        showTextScript.UpdateText("+" + value);
        showTextScript.fontSize(10, value);
        HealthUpdate?.Invoke();
        if (UIManager.Instance.SelectObjectInfo.CurrentObject == this) 
        {
            UIManager.Instance.OverInfoState(this);
        }
    }

    public void setTargetObject(GameObject target)
    {
        Resettarget();
        targetObject = target;
        target.GetComponent<CanSelectObject>().NoticeChangeRoom += TargetChangedRoom;
    }

    void TargetChangedRoom()
    {
        if (targetObject)
        {
            Path = RoomManager.Instance.FindPath(this, targetObject.GetComponent<CanSelectObject>().currentRoom);
        }
    }

    virtual protected void Resettarget()
    {
        targetPosition = null;

        if (targetObject) 
        {
            targetObject.GetComponent<CanSelectObject>().NoticeChangeRoom -= TargetChangedRoom;
        }
        targetObject = null;
        targetCore = null;
    }

    virtual public void InfoOverCanvas() 
    {
        
    }
}
