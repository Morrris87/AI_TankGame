using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class FleeState : FSMState
{
    AI enemyAI;
    float health;

    float elapsedTime;
    float intervalTime;

    EnemyController enemyController;
    public FleeState(AI enemyTank)
    {
        stateID = FSMStateID.Fleeing;
        curRotSpeed = 2.0f;
        curSpeed = 5.0f;

        enemyAI = enemyTank;
        health = enemyAI.health;

        enemyController = enemyAI.Turret.GetComponent<EnemyController>();

        elapsedTime = 0.0f;
        intervalTime = 1.0f;
        enemyAI.navAgent.speed = curSpeed;
    }

    public override void EnterStateInit()
    {
        Debug.Log("Entering Flee State");
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

        if (health > 15)
        {
            enemyAI.PerformTransition(Transition.LostPlayer);
            Debug.Log("Wandering");
            return;
        }

        elapsedTime += Time.deltaTime;
        if(elapsedTime >= intervalTime)
        {
            health += 5;
            elapsedTime = 0.0f;
        }
    }

    public override void Act()
    {
        Transform tank = enemyAI.gameObject.transform;
        Transform player = enemyAI.Player.transform;
        float dist = 0;

        for (int i = 0; i < enemyAI.waypoints.Length/*-1*/; i++)
        {
            float temp = Vector3.Distance(tank.position, enemyAI.waypoints[i].transform.position);
            if (temp > dist)
            {
                dist = temp;
                destPos = enemyAI.waypoints[i].transform.position;
            }
        }

        Quaternion targetRotation = Quaternion.LookRotation(destPos - tank.position);

        tank.rotation = Quaternion.Slerp(tank.rotation, targetRotation, Time.deltaTime * curRotSpeed);

        enemyAI.navAgent.SetDestination(destPos);
    }
}
