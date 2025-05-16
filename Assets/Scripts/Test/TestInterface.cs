using System;
using UnityEngine;

public interface TestInterface
{
    float Attack { get; set; }
    Vector2 AttackRange { get; set; }
    Action DamageAction { get; set; }
}
