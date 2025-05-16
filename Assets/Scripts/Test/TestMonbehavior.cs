using System;
using UnityEngine;

public class TestMonbehavior : MonoBehaviour, TestInterface
{
    public float Attack { get; set; }
    public Vector2 AttackRange { get; set; }
    public Action DamageAction { get; set; }

    private void onenable()
    {
        Attack = 10f;
        AttackRange = new Vector2(1f, 1f);
    }

    private void OnEnable()
    {
        DamageAction += Damage;
    }

    private void OnDisable()
    {
        DamageAction -= Damage;
    }

    public void Damage() 
    {
        Debug.Log("Take Damage!");
    }
}
