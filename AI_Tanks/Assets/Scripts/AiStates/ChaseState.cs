﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class ChaseState : FSMState
{
    AI enemyAI;
    float health;

    EnemyController enemyController;
    public ChaseState(AI enemyTank)
    {
        stateID = FSMStateID.Chasing;
        curRotSpeed = 2.0f;
        curSpeed = 3.0f;

        enemyAI = enemyTank;
        health = enemyAI.health;

        enemyController = enemyAI.Turret.GetComponent<EnemyController>();
        enemyAI.navAgent.speed = curSpeed;
    }

    public override void EnterStateInit()
    {
        Debug.Log("Entering Chase State");
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
            Debug.Log("Wandering");
            return;
        }
        else if(IsInCurrentRange(tank, player.position, enemyAI.attackRange))
        {
            enemyAI.PerformTransition(Transition.ReachPlayer);
            Debug.Log("Attack");
            return;
        }
        else if(!(IsInCurrentRange(tank, player.position, enemyAI.chaseRange)))
        {
            enemyAI.PerformTransition(Transition.LostPlayer);
            Debug.Log("Lost Player");
            return;
        }
    }

    public override void Act()
    {
        Transform tank = enemyAI.gameObject.transform;
        Transform player = enemyAI.Player.transform;

        Quaternion targetRotation = Quaternion.LookRotation(player.position - tank.position);

        tank.rotation = Quaternion.Slerp(tank.rotation,
                targetRotation, Time.deltaTime * curRotSpeed);

        enemyAI.navAgent.SetDestination(destPos);
    }
}
