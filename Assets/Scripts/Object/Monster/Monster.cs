using UnityEngine;

public class Monster : CanMoveObjects
{
    void Start()
    {
        Init();
        moveSpeed = 4f;
        AttackPower = 1f;
        MaxHealth = 500f;
        CurrentHealth = MaxHealth;
        attackRange = new Vector2(2, 1);
    }
    
    void Update()
    {
    }

    private void FixedUpdate()
    {
        MoveAI();
    }

    public override void TakeDamage(CanSelectObject attackObject)
    {
        base.TakeDamage(attackObject);
        // 아무것도 안하고있는데 누가 때리면 나도 때림
        if (targetPosition == null && targetObject == null && targetCore == null)
        {
            setTargetObject(attackObject.gameObject);
        }
    }
}
