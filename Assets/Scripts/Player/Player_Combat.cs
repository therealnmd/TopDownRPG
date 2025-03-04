using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayer;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Attack()
    {
        anim.SetBool("isAttacking", true);
    }

    public void DealDamage()
    {
        //tao 1 list chua toan bo cac object nam trong tam cua player
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, StatsManager.Instance.weaponRange, enemyLayer);
        //overlapcircleall is an invisible detecion circle, takes in 3 params:
        //origin point, radius of the circle, layer it is looking for
        //neu ma do dai cua cai list hits nhieu hon 0, tuc la co enemy trong tam danh cua player
        if (enemies.Length > 0)
        {
            enemies[0].GetComponent<Enemy_Health>().ChangeHealth(-StatsManager.Instance.damage);
            enemies[0].GetComponent<Enemy_KnockBack>().KnockBack(transform, StatsManager.Instance.knockBackForce, StatsManager.Instance.knockBackTime, StatsManager.Instance.stunTime);
        }
    }

    public void FinishAttacking()
    {
        anim.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, StatsManager.Instance.weaponRange);
    }
}
