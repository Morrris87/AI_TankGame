using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class WanderState : FSMState
{
    AI enemyAI;
    float health;

    float elapsedTime;
    float intervalTime;

    int randNum;

    EnemyController enemyController;
    public WanderState(AI enemyTank)
    {
        stateID = FSMStateID.Patrolling;
        curRotSpeed = 2.0f;
        curSpeed = 3.0f;

        enemyAI = enemyTank;
        health = enemyAI.health;

        enemyController = enemyAI.Turret.GetComponent<EnemyController>();

        randNum = Random.Range(0, 7);

        elapsedTime = 0.0f;
        intervalTime = 1.0f;
        enemyAI.navAgent.speed = 3;
    }

    public override void EnterStateInit()
    {
        Debug.Log("Entering Wander State");
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
            Debug.Log("Hiding");
            return;
        }

        if (IsInCurrentRange(tank, player.position, enemyAI.chaseRange))
        {
            enemyAI.PerformTransition(Transition.SawPlayer);
            Debug.Log("Chasing");
            return;
        }
    }

    public override void Act()
    {
        Transform tank = enemyAI.gameObject.transform;
        Transform player = enemyAI.Player.transform;

        float dist = Vector3.Distance(tank.position, enemyAI.waypoints[randNum].transform.position);

        if (dist < 0.5f)
        {
            int temp = randNum;
            while (temp == randNum)
            {
                randNum = Random.Range(0, 7);
            }
        }
        else
        {
            destPos = enemyAI.waypoints[randNum].transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(destPos - tank.position);

            tank.rotation = Quaternion.Slerp(tank.rotation,
                    targetRotation, Time.deltaTime * curRotSpeed);

            enemyAI.navAgent.SetDestination(destPos);
        }
    }
}
