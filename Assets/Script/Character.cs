using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] protected HeathBar heathBar;
    [SerializeField] protected CombatText combatTextPrefab;
    private float hp;
    private float originHP = 100;
    public bool isDead => hp <= 0;
    private string currentAnim;

    private void Start()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
        hp = originHP;
        heathBar.OnInit(100, transform);
    }
    public virtual void OnDespawn()
    {

    }
    protected virtual void OnDeath()
    {
        ChageAnim("dead");
        Invoke(nameof(OnDespawn), 2f);
    }
    protected void ChageAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
    public void OnHit(float damage)
    {
        if (!isDead)
        {
            hp -= damage;

            if (isDead)
            {
                hp = 0;
                OnDeath();
            }
            heathBar.SetNewHP(hp);
            Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        }
    }


}
