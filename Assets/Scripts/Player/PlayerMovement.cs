using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool isKnockBack;

    public Player_Combat playerCombat;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            playerCombat.Attack(); 
        }
    }

    // Update is called 50x per frame
    void FixedUpdate()
    {
        if (isKnockBack == false)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));

            //neu di chuyen sang phai va mat dang huong ve phia trai hoac nguoc lai
            if (horizontal > 0 && transform.localScale.x < 0 ||
                horizontal < 0 && transform.localScale.x > 0)
            {
                Flip();
            }

            rb.velocity = new Vector2(horizontal, vertical) * StatsManager.Instance.moveSpeed;
        }
        
    }

    void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void KnockBack(Transform enemy, float knockBackForce, float stunTime)
    {
        isKnockBack = true; 
        Vector2 direction = (transform.position - enemy.position).normalized; //dat vector gioi han (-1, 1)
        rb.velocity = direction * knockBackForce;
        StartCoroutine(KnockBackCounter(stunTime));
    }

    //Coroutines: hoat dong nhu 1 method, nhung co the bi dung` (dung` de? timer)
    IEnumerator KnockBackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        isKnockBack = false;
    }

    

}
