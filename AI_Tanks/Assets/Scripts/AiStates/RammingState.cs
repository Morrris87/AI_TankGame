using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class RammingState : FSMState
{
    AI enemyAI;
    float health;

    EnemyController enemyController;
    public RammingState(AI enemyTank)
    {
        stateID = FSMStateID.Ramming;
        curRotSpeed = 2.0f;
        curSpeed = 7.0f;

        enemyAI = enemyTank;
        health = enemyAI.health;

        enemyController = enemyAI.Turret.GetComponent<EnemyController>();
        enemyAI.navAgent.speed = curSpeed;
    }

    public override void EnterStateInit()
    {
        Debug.Log("Entering Ramming State");
    }

    public override void Reason()
    {
        Transform tank = enemyAI.gameObject.transform;
        Transform player = enemyAI.Player.transform;

        if (health <= 0)
        {
            enemyAI.PerformTransition(Transition.NoHealth);
            return;
        }

        if (health <= 15)
        {
            enemyAI.PerformTransition(Transition.Hiding);
            Debug.Log("Hiding");
            return;
        }

        if (IsInCurrentRange(tank, player.position, enemyAI.chaseRange) && !(IsInCurrentRange(tank, player.position, enemyAI.rammingRange)))
        {
            enemyAI.PerformTransition(Transition.SawPlayer);
            Debug.Log("Chasing");
            return;
        }

        if (IsInCurrentRange(tank, player.position, enemyAI.attackRange) && !(IsInCurrentRange(tank, player.position, enemyAI.rammingRange)))
        {
            enemyAI.PerformTransition(Transition.ReachPlayer);
            Debug.Log("Attacking");
            return;
        }

        if (!(IsInCurrentRange(tank, player.position, enemyAI.chaseRange)))
        {
            enemyAI.PerformTransition(Transition.LostPlayer);
            Debug.Log("Wandering");
        }
    }

    public override void Act()
    {
        Transform tank = enemyAI.gameObject.transform;
        Transform player = enemyAI.Player.transform;

        //setDestPos

        Quaternion targetRotation = Quaternion.LookRotation(player.position - tank.position);

        tank.rotation = Quaternion.Slerp(tank.rotation,
                targetRotation, Time.deltaTime * curRotSpeed);

        enemyAI.navAgent.SetDestination(player.position);
    }
}
