using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
 
    [Header("Player Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float doubleJump = 4f;

    [Header("Player Animation")]
    public Ease ease = Ease.OutBack;
    public float scaleSize = 2.0f;
    public float scaleTime = 1f;
    public Animator animator;




    //privadas
    private Rigidbody2D rigidbody2D;
    private EnemyBase enemy;
    private bool isGrounded = true;
    private Tween moveTween;
    private HealthBase health;
    private bool facingRight = true;



    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        isGrounded = true;
        health = GetComponent<HealthBase>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = 0f;


        if (animator != null)
        {
            animator.SetFloat("Run", Mathf.Abs(moveX));
            animator.SetBool("isGrounded", isGrounded);

            if(Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Attack");
            }
        }

        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }


        if (moveX != 0)
        {
            float targetX = transform.position.x + moveX * moveSpeed * Time.deltaTime; 

            moveTween?.Kill();
            moveTween = transform.DOMoveX(targetX, 0.5f).SetEase(ease);
        }


        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
            isGrounded = false;
            transform.DOScaleY(scaleSize, scaleTime).SetLoops(2, LoopType.Yoyo).SetEase(ease);

        }

        if(!isGrounded && Input.GetKey(KeyCode.W))
        {
            moveY = doubleJump;
        }


        Vector2 direction = new Vector3(moveX, moveY, 0).normalized;

        Move(direction);
    }

    public void Move(Vector2 direction)
    {
        rigidbody2D.velocity = new Vector2(direction.x * moveSpeed, rigidbody2D.velocity.y);

        if (direction.y > 0)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        enemy = collision.gameObject.GetComponent<EnemyBase>();
        if (enemy != null && health != null) 
        {
            health.TakeDamage(enemy.damage);
        }

        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

   
    
        
    
}
