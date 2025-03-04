using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    public int damage = 1;
    public Transform attackPoint;
    public float weaponRange;
    public float knockBackForce;
    public float stunTime;
    public LayerMask playerLayer;

    public void Attack()
    {
        //tao 1 list chua toan bo cac object nam trong tam cua enemy
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);
        //overlapcircleall is an invisible detecion circle, takes in 3 params:
        //origin point, radius of the circle, layer it is looking for

        //neu ma do dai cua cai list hits nhieu hon 0, tuc la co player trong tam danh cua enemy
        if (hits.Length > 0) 
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-damage); //hits[0] la element player
            hits[0].GetComponent<PlayerMovement>().KnockBack(transform, knockBackForce, stunTime);
        }
    }
}
