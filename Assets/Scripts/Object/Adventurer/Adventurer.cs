using System;
using System.Collections.Generic;
using UnityEngine;

public class Adventurer : CanMoveObjects
{
    public List<Door> TestPath;
    void Start()
    {
        Init();
        moveSpeed = 3f;
        AttackPower = 1f;
        MaxHealth = 555f;
        CurrentHealth = MaxHealth;
        attackRange = new Vector2(0.2f, 1);
    }

    void Update()
    {
        TestPathCopy();
        FindCore();
    }

    private void FixedUpdate()
    {
        MoveAI();
    }

    void TestPathCopy() 
    {
        TestPath.Clear();
        foreach (Tuple<Door, int> tuple in Path) 
        {
            TestPath.Add(tuple.Item1);
        }
    }

    void FindCore() 
    {
        if (targetObject == null) 
        {
            setTargetObject(FindAnyObjectByType<CoreFacilities>().gameObject);
            Path = RoomManager.Instance.FindPath(this, targetObject.GetComponent<CanSelectObject>().currentRoom);
        }
    }

    public override void TakeDamage(CanSelectObject attackObject)
    {
        base.TakeDamage(attackObject);
        if (targetObject == null || targetObject.GetComponent<CoreFacilities>()) 
        {
            setTargetObject(attackObject.gameObject);
        }
    }
}
