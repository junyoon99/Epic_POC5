using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CanMoveObjects : CanSelectObject
{

    protected Rigidbody2D rb2d;
    protected Collider2D col;
    public float moveSpeed { get; protected set; }
    protected bool isArrived;

    override protected void Init()
    {
        base.Init();
        rb2d = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    protected override void Resettarget()
    {
        base.Resettarget();
        isArrived = false;
    }

    public void TestInspecter(List<Door> TestPath)
    {
        foreach (Tuple<Door, int> d in Path)
        {
            TestPath.Add(d.Item1);
        }
    }

    protected void MoveAI()
    {
        // 가야 할 문이 있으면 제일 우선순위
        if (Path != null && Path.Count > 0)
        {
            MoveFollowPath();
        }
        // 가야 할 문이 더 없고 사용할 코어가 있으면
        else if (targetCore != null)
        {
            TryUseCore();
        }
        // 공격대상이 있으면
        else if (targetObject != null)
        {
            TryAttack(targetObject);
        }
        // 목표 포지션이 있으면
        else if (targetPosition != null)
        {
            MoveTo((Vector2)targetPosition);
        }
    }
    protected void MoveFollowPath()
    {
        // 거쳐야 할 문이 있으면 그 문으로 이동
        MoveTo(Path[0].Item1.transform.position);
    }

    protected void MoveTo(Vector2 goalPosition)
    {
        float directionX = Mathf.Sign(goalPosition.x - rb2d.position.x);
        float moveStep = directionX * moveSpeed;
        float remainDistance = Mathf.Abs(goalPosition.x - rb2d.position.x);

        if (remainDistance > 0.1f)
        {
            if (MathF.Abs(rb2d.linearVelocityX) < MathF.Abs(moveStep))
            {
                rb2d.linearVelocityX = moveStep;
            }
        }
        else
        {
            rb2d.linearVelocityX = 0;
            isArrived = true;

            if (Path != null && Path.Count > 0)
            {
                TryEnterDoor(Path[0].Item2);
                Path.RemoveAt(0);
            }
            else
            {
                targetPosition = null;
            }
        }

    }

    protected void TryAttack(GameObject target)
    {
        Collider2D[] inAttackRangeObjects = Physics2D.OverlapAreaAll((Vector2)transform.position - attackRange * 0.5f, (Vector2)transform.position + attackRange * 0.5f);
        bool findEnemmy = false;
        foreach (Collider2D col in inAttackRangeObjects)
        {
            if (col.gameObject == targetObject && currentRoom == targetObject.GetComponent<CanSelectObject>().currentRoom)
            {
                findEnemmy = true;
                break;
            }
        }

        if (findEnemmy)
        {
            Attack();
        }
        else
        {
            MoveTo(targetObject.transform.position);
        }
    }

    protected void TryUseCore()
    {
        if (isArrived)
        {
            UseCore();
        }
        else
        {
            MoveTo(targetCore.transform.position);
        }
    }

    protected void UseCore()
    {
        targetCore.GetComponent<CoreFacilities>().CoreFunction(this);
    }

    protected void Attack()
    {
        // 시체매너
        if (targetObject.GetComponent<CanSelectObject>().CurrentHealth > 0) 
        {
            targetObject.GetComponent<CanSelectObject>().TakeDamage(this);
        }
    }

    public void setTargetPos(Vector2 position, Room room)
    {
        Resettarget();
        targetPosition = position;
    }

    public void setTargetCore(GameObject targetCore)
    {
        Resettarget();
        this.targetCore = targetCore;
    }

    protected void TryEnterDoor(int index)
    {
        Collider2D[] collisions = Physics2D.OverlapAreaAll(col.bounds.min, col.bounds.max);
        foreach (Collider2D collision in collisions)
        {
            if (collision.TryGetComponent<Door>(out Door door))
            {
                door.DoorEnter(transform, index);
                return;
            }
        }
        Debug.Log("문 없음!");
    }
}
