using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{

    float time;
    public void OnEnter(Enemy enemy)
    {
        if (enemy.Target != null)
        {
            // doi huong enemy toi huong cua player
            enemy.ChageDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            enemy.StopMoving();
            enemy.Attack();
        }
        time = 0;
    }

    public void OnExecute(Enemy enemy)
    {
        time += Time.deltaTime;
        if (time >= 1.5f)
        {
            enemy.ChageState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {
    }
}
