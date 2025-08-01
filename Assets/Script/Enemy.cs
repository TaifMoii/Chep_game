using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject attackArea;

    private IState currentState;
    private bool isRight = true;
    private Character target;
    public Character Target => target;
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }


    public override void OnInit()
    {
        base.OnInit();
        ChageState(new IdleState());
        DeActiveAttack();
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(heathBar.gameObject);
        Destroy(gameObject);
    }
    protected override void OnDeath()
    {
        ChageState(null);
        base.OnDeath();
    }
    public void ChageState(IState state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public void Moving()
    {
        ChageAnim("run");
        rb.velocity = transform.right * moveSpeed;
    }
    public void StopMoving()
    {
        ChageAnim("idle");
        rb.velocity = Vector2.zero;
    }
    public void Attack()
    {
        ChageAnim("attack");
        Invoke(nameof(ActiveAttack), 0.4f);
        Invoke(nameof(DeActiveAttack), 0.5f);
    }
    public bool IsTargetInRange()
    {

        if (target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsTargetInRange())
        {

        }
        else
        {
            if (collision.tag == "EnemyWall")
            {
                ChageDirection(!isRight);
            }
        }
    }

    public void ChageDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }

    private void ActiveAttack()
    {
        attackArea.SetActive(true);

    }
    private void DeActiveAttack()
    {
        attackArea.SetActive(false);

    }
    internal void SetTarget(Character character)
    {


        this.target = character;
        if (IsTargetInRange())
        {
            ChageState(new AttackState());
        }
        else if (Target != null)
        {
            ChageState(new PatrolState());
        }
        else
        {
            ChageState(new IdleState());
        }


        DeActiveAttack();

    }
}
