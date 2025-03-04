using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public float enemySpeed = 2f;
    //private bool isChasing;
    private int facingDirection = 1;
    public EnemyState enemyState; //track the enemy's current state

    public float attackRange = 2; //tam danh cua enemy
    public float attackCooldown = 2; //toc do delay tung chieu
    private float attackCooldownTimer; //dung bo dem kiem soat attackCD
    public float playerDetectRange = 5; //khoang cach ma trong do, enemy nhin thay player
    public Transform detectionPoint; //diem giua cua enemy's circle of sight.
    public LayerMask playerLayer;

    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    void Update()
    {
        if (enemyState != EnemyState.Knockback)
        {
            CheckForPlayer();
            if (attackCooldownTimer > 0) //neu bo dem hoat dong
            {
                attackCooldownTimer -= Time.deltaTime; //time.deltatime do khoang thoi gian troi qua tu frame cuoi cung
            }
            if (enemyState == EnemyState.Chasing)
            {
                Chase();
            }
            else if (enemyState == EnemyState.Attacking)
            {
                rb.velocity = Vector2.zero;
            }
        }
        
    }

    void Chase()
    {
        //trong unity, transform luon tham chieu den doi tuong duoc gan script, neu muon tham chieu transform den doi tuong khac thi phai khai bao, vi du nhu private Transform player 
        //neu player dang o phia ben phai nhung enemy quay mat sang trai hoac nguoc lai
        if (player.position.x > transform.position.x && facingDirection == -1 ||
                 player.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }
        //vector chi huong tu vi tri hien tai cua enemy den vi tri hien tai cua player
        //.normalized biến vector đó thành vector đơn vị có độ dài bằng 1.
        Vector2 direction = (player.position - transform.position).normalized; //chuan hoa vector ve dang (-1,1)
        rb.velocity = direction * enemySpeed; //dat van toc cho rb, gan no vao direction
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    //neu la OnTriggerEnter thi enemy se nhan dang la player enter vao tam collision va se bat dau di chuyen va tan cong
    //neu la OnTriggerStay thi enemy se nhan dang la player da enter va dang stay trong tam collision va se lien tuc di chuyen (dk la player trong tam)
    private void CheckForPlayer()
    {
        //khi da nhin thay player trong tam nhin cua enemy
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);

        if (hits.Length > 0) 
        {
            player = hits[0].transform; //dat doi tuong player la element dau tien duoc enemy nhin thay
            //kiem tra player co nam trong tam danh va bo dem da tat hay k
            if (Vector2.Distance(transform.position, player.transform.position) <= attackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            //neu player nam ngoai tam danh va k phai o trang thai Attack
            else if (Vector2.Distance(transform.position, player.transform.position) > attackRange && enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        //neu enemy chua nhin thay player
        else
        {
            rb.velocity = Vector2.zero; 
            ChangeState(EnemyState.Idle);
        }

        //if (collision.gameObject.tag == "Player")
        //{
        //    //Nếu player chưa được gán giá trị (null), thì lấy vị trí của Player (collision.transform) để lưu vào biến player.
        //    //Cách này giúp Enemy chỉ cần tìm Player một lần duy nhất, tránh việc liên tục cập nhật Player nếu có nhiều va chạm.
        //    if (player == null)
        //    {
        //        player = collision.transform;
        //    }
            //isChasing = true;
    }
    
    //------------------------BỎ ĐƯỢC ĐOẠN DƯỚI NÀY----------------------
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        rb.velocity = Vector2.zero;
    //        ChangeState(EnemyState.Idle);
    //        //isChasing = false;
    //    }
    //}

    //khoi tao ham ChangeState voi kieu EnemyState, gia tri truyen vao newState
    //ham ChangeState chuyen trang thai tu A -> B, trang thai khoi dau la Idle
    public void ChangeState(EnemyState newState)
    {
        //neu dang o trang thai Idle, se tat animation Idle de ChangeState sang trang thai moi
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", false);
        }
        //neu dang o trang thai Walking, se tat animation Walking de ChangeState sang trang thai moi
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", false);
        }
        //neu dang o trang thai Attacking, se tat animation Attacking de ChangeState sang trang thai moi
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", false);
        }

        //luu trang thai moi vao bien enemyState
        enemyState = newState;


        //bat anim tuong ung voi trang thai moi
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", true);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", true);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);
    }
}

//Goi ham nay o ngoai bracket
//1. cho phep ket noi voi cac script khac
//2. OOP: dong goi (tach rieng logic so voi phan con lai cua class)
public enum EnemyState 
{
    Idle,
    Chasing,
    Attacking,
    Knockback
}

