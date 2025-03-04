using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_KnockBack : MonoBehaviour
{
    private Rigidbody2D rb;
    private Enemy_Movement enemyMovement;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<Enemy_Movement>();
    }


    public void KnockBack(Transform playerTransform, float knockBackForce, float knockBackTime, float stunTime)
    {
        enemyMovement.ChangeState(EnemyState.Knockback);
        StartCoroutine(StunTimer(knockBackTime, stunTime));
        Vector2 direction = (transform.position - playerTransform.position).normalized;
        rb.velocity = direction * knockBackForce;
    }
    
    IEnumerator StunTimer(float knockBackTime, float stunTime)
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        enemyMovement?.ChangeState(EnemyState.Idle);
    }
}
