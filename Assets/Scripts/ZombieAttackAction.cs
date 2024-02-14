using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "A.I/Actions/Zombie Attack Action")]
public class ZombieAttackAction : ScriptableObject
{
    [Header("Attack Animation")]
    public string attackAnimation;

    [Header("Attack Cooldown")]
    public float attackCooldown = 5f;

    [Header("Attack Angles and Distances")]
    public float maxiumAttackAngle = 20f;
    public float miniumAttackAngle = -20f;
    public float miniumAttackDistance = 1f;
    public float maxiumAttackDistance = 3.5f;
}
