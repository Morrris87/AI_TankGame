using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class RammingState : FSMState
{
    AI enemyAI;
    float health;
    float elapsedTime;
    float intervalTime;
    Rigidbody r;

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
        elapsedTime = 0;
        intervalTime = 2.5f;
        r = enemyAI.GetComponent<Rigidbody>();
    }

    public override void EnterStateInit()
    {
        if (r.isKinematic)
        {
            enemyAI.navAgent.isStopped = true;
            r.isKinematic = false;
        }
        Debug.Log("Entering Ramming State");
    }

    public override void Reason()
    {
        Transform tank = enemyAI.gameObject.transform;
        Transform player = enemyAI.Player.transform;

        if (tank.position.y < 399)
        {
            GameObject.Destroy(enemyAI);
        }

        if (health <= 0)
        {
            enemyAI.PerformTransition(Transition.NoHealth);
            enemyAI.navAgent.isStopped = false;
            return;
        }

        if (health <= 15)
        {
            enemyAI.PerformTransition(Transition.Hiding);
            enemyAI.navAgent.isStopped = false;
            Debug.Log("Hiding");
            return;
        }

        elapsedTime += Time.deltaTime;
        if (elapsedTime > intervalTime)
        {
            elapsedTime = 0.0f;
            enemyAI.PerformTransition(Transition.ReachPlayer);
            enemyAI.navAgent.isStopped = false;
            return;
        }

        if (IsInCurrentRange(tank, player.position, enemyAI.chaseRange) && !(IsInCurrentRange(tank, player.position, enemyAI.rammingRange)))
        {
            enemyAI.PerformTransition(Transition.SawPlayer);
            Debug.Log("Chasing");
            enemyAI.navAgent.isStopped = false;
            return;
        }

        if (!(IsInCurrentRange(tank, player.position, enemyAI.chaseRange)))
        {
            enemyAI.PerformTransition(Transition.LostPlayer);
            enemyAI.navAgent.isStopped = false;
            Debug.Log("Wandering");
        }
    }

    public override void Act()
    {
        Transform tank = enemyAI.gameObject.transform;
        Transform player = enemyAI.Player.transform;
        Vector3 dir = new Vector3();        

        //setDestPos

        Quaternion targetRotation = Quaternion.LookRotation(player.position - tank.position);

        tank.rotation = Quaternion.Slerp(tank.rotation,
                targetRotation, Time.deltaTime * curRotSpeed);


        tank.Translate((tank.position - player.position).normalized * curSpeed * Time.deltaTime);        
    }
}
