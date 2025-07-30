using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpForce = 350;
    [SerializeField] private float speed = 250;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;



    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttack = false;
    private float horizontal;
    private int coin = 0;
    private Vector3 savePoint;

    void Awake()
    {
        SavePoint();
        coin = PlayerPrefs.GetInt("coin", 0);
    }


    void Update()

    {
        if (isDead)
        {
            return;
        }
        isGrounded = CheckGrounded();

        //Di chuyen
        //-1 ->0->1
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (isGrounded)
        {
            if (isJumping)
            {
                return;
            }
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
            {
                Jump();
            }

            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChageAnim("run");
            }
            //Attack
            if ((Input.GetKeyDown(KeyCode.J)) && isGrounded)
            {
                Attack();
            }
            //Throw
            if ((Input.GetKeyDown(KeyCode.K)) && isGrounded)
            {
                Throw();
            }

        }
        //check falling
        if (!isGrounded && rb.velocity.y < 0)
        {
            ChageAnim("fall");
            isJumping = false;
        }


        /* vertical= Input.GetAxisRaw("Vertical"); */
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }
        else if (isGrounded)
        {
            ChageAnim("idle");
            rb.velocity = Vector2.zero;
        }
    }

    public override void OnInit()
    {
        DeActiveAttack();
        base.OnInit();
        isAttack = false;
        transform.position = savePoint;
        ChageAnim("idle");
        UIManager.instance.SetCoin(coin);

    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }
    protected override void OnDeath()
    {
        base.OnDeath();
    }
    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public void Attack()
    {
        isAttack = true;
        ChageAnim("attack");
        Invoke(nameof(ResetIdle), 0.65f);
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);


    }
    public void Throw()
    {
        isAttack = true;
        ChageAnim("throw");
        Invoke(nameof(ResetIdle), 0.65f);

        Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);

    }
    public void Jump()
    {
        isJumping = true;
        ChageAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
    }
    private void ResetIdle()
    {
        ChageAnim("ilde");
        isAttack = false;

    }
    private void ActiveAttack()
    {
        attackArea.SetActive(true);

    }
    private void DeActiveAttack()
    {
        attackArea.SetActive(false);

    }

    public void OnTriggerEnter2D(Collider2D collision)

    {
        if (collision.tag == "Coin")
        {
            coin++;
            PlayerPrefs.SetInt("coin", coin);
            UIManager.instance.SetCoin(coin);

            Destroy(collision.gameObject);
        }
        if (collision.tag == "DeadZone")
        {
            ChageAnim("dead");
            Invoke(nameof(OnInit), 1f);
        }
    }

    internal void SavePoint()
    {
        savePoint = transform.position;
    }
    public void SetMove(float horizontal)
    {
        this.horizontal = horizontal;
    }


}
