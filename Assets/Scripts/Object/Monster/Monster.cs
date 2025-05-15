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
        // �ƹ��͵� ���ϰ��ִµ� ���� ������ ���� ����
        if (targetPosition == null && targetObject == null && targetCore == null)
        {
            setTargetObject(attackObject.gameObject);
        }
    }
}
