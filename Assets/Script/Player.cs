using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpForce = 350;
    [SerializeField] private float speed = 250;

    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttack = false;
    private float horizontal;
    private string currentAnim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
            rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }
        else if (isGrounded)
        {
            ChageAnim("idle");
            rb.velocity = Vector2.zero;
        }
    }
    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.7f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    private void Attack()
    {
        isAttack = true;
        ChageAnim("attack");
        Invoke(nameof(ResetIdle), 0.5f);
    }
    private void Throw()
    {
        isAttack = true;
        ChageAnim("throw");
        Invoke(nameof(ResetIdle), 0.5f);
    }
    private void Jump()
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

    // doi Anim 
    private void ChageAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }


    }
}
