using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class AttackState : FSMState
{
    AI enemyAI;
    float health;
    float elapsedTime;
    float intervalTime;
    float shotElapsed;
    float shotTimer;
    EnemyController enemyController;
    public AttackState(AI enemyTank)
    {
        stateID = FSMStateID.Attacking;
        curRotSpeed = 1.0f;
        curSpeed = 0.0f;
        elapsedTime = 0.0f;
        intervalTime = 5.0f;

        enemyAI = enemyTank;
        health = enemyAI.health;

        enemyController = enemyAI.Turret.GetComponent<EnemyController>();
    }

    public override void EnterStateInit()
    {
        elapsedTime = 0.0f;
        Debug.Log("Entering Attack State");
    }

    public override void Reason()
    {
        Transform tank = enemyAI.gameObject.transform;
        Transform player = enemyAI.Player.transform;

        float dist = Vector3.Distance(tank.position, player.position);

        if (health <= 0)
        {
            enemyAI.PerformTransition(Transition.NoHealth);
            return;
        }

        if (health <= 15)
        {
            enemyAI.PerformTransition(Transition.Hiding);
            Debug.Log("Fleeing");
            return;
        }

        elapsedTime += Time.deltaTime;
        if (elapsedTime > intervalTime)
        {
            elapsedTime = 0.0f;
            enemyAI.PerformTransition(Transition.SawPlayer);
        }

        else if (IsInCurrentRange(tank, player.position, enemyAI.attackRange))
        {
            shotElapsed += Time.deltaTime;
            if(shotElapsed > shotTimer)
            {
                enemyController.attack = true;
                shotElapsed = 0.0f;
            }
        }

        else if(IsInCurrentRange(tank, player.position, enemyAI.chaseRange))
        {
            enemyAI.PerformTransition(Transition.SawPlayer);
        }

        else
        {
            enemyAI.PerformTransition(Transition.LostPlayer);
        }
    }

    public override void Act()
    {

    }
}
