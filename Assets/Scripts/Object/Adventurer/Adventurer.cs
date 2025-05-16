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
        AttackPower = 2f;
        MaxHealth = 555f;
        CurrentHealth = MaxHealth;
        attackRange = new Vector2(0.2f, 1);
    }

    void Update()
    {
        TestPathCopy();
        FindObject();
    }

    private void FixedUpdate()
    {
        MoveAI();
    }

    void TestPathCopy() 
    {
        TestPath.Clear();
        if (Path == null) return;
        foreach (Tuple<Door, int> tuple in Path)
        {
            TestPath.Add(tuple.Item1);
        }
    }

    void FindObject() 
    {
        // 타겟 오브젝트가 없으면 찾기
        if ((targetObject == null && currentRoom != null) || Path == null)
        {
            // 코어 시설 먼저 찾아보기
            CoreFacilities findCore = FindAnyObjectByType<CoreFacilities>();
            if (findCore != null)
            {
                setTargetObject(findCore.gameObject);
                Path = RoomManager.Instance.FindPath(this, targetObject.GetComponent<CanSelectObject>().currentRoom);
            }
            else
            {
                // 맵에 코어 시설이 없으면 몬스터 대상찾기
                Monster findMonster = FindAnyObjectByType<Monster>();
                if (findMonster != null)
                {
                    setTargetObject(findMonster.gameObject);
                    Path = RoomManager.Instance.FindPath(this, targetObject.GetComponent<CanSelectObject>().currentRoom);
                }
            }
        }
    }

    public override void TakeDamage(CanSelectObject attackObject)
    {
        base.TakeDamage(attackObject);
        if (targetObject == null || (targetObject != null && targetObject.GetComponent<CoreFacilities>()))
        {
            print("날 때려?");
            setTargetObject(attackObject.gameObject);
        }
    }
}
