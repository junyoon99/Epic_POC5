using System;
using UnityEngine;

public class TestBrain : MonoBehaviour
{
    TestMonbehavior testMonbehavior;
    TestCommanMove commanMove;
    TestMonsterAI monsterAI;
    TestCanSelectObject canSelectObject;
    private void Awake()
    {
        testMonbehavior = GetComponent<TestMonbehavior>();
        commanMove = GetComponent<TestCommanMove>();
        monsterAI = GetComponent<TestMonsterAI>();
        canSelectObject = GetComponent<TestCanSelectObject>();

        testMonbehavior.Attack = 15f;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (canSelectObject.isSelected)
        {
            commanMove.Move();
        }
        else 
        {
            monsterAI.Move();
        }
    }

    void TakeDamage() 
    {
        testMonbehavior.DamageAction?.Invoke();
    }
}
