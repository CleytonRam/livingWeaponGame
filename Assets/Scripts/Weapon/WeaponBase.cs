using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Weapon Stats")]
    public float damage = 10f;
    public float attackRange = 1f;
    public float attackRate = 1f;

    protected float lastAttackTime;

    public virtual void Attack()
    {
        if (Time.time >= lastAttackTime + attackRate)
        {
            lastAttackTime = Time.time;
           // PerformAttack();  
        }
    }
}
