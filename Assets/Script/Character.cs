using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private float hp;
    public bool isDead => hp <= 0;
    private string currentAnim;

    public void Start()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
        hp = 100;
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
                OnDeath();

            }
        }
    }


}
