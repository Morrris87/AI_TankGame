using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;
using UnityEngine.AI;

public class ChaseState : FSMState
{
    AI enemyAI;
    float health;
    Rigidbody r;

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
        r = enemyAI.GetComponent<Rigidbody>();
    }

    public override void EnterStateInit()
    {
        if (!r.isKinematic)
        {
            r.isKinematic = true;
        }

        Debug.Log("Entering Chase State");
    }

    public override void Reason()
    {
        Transform tank = enemyAI.gameObject.transform;
        Transform player = enemyAI.Player.transform;

        float dist = Vector3.Distance(tank.position, player.position);

        if (tank.position.y < 399)
        {
            GameObject.Destroy(enemyAI);
        }

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
        SlotManager playerSlots = enemyAI.Player.GetComponent<SlotManager>();


        destPos =  playerSlots.GetSlotPosition(playerSlots.ReserveSlotAroundObject(enemyAI.gameObject));

        Quaternion targetRotation = Quaternion.LookRotation(player.position - tank.position);

        tank.rotation = Quaternion.Slerp(tank.rotation,
                targetRotation, Time.deltaTime * curRotSpeed);

        NavMeshHit hit;

        if (NavMesh.SamplePosition(destPos, out hit, 0.1f, NavMesh.AllAreas))
        {
            //On the mesh
            enemyAI.navAgent.SetDestination(destPos);
        }
        else
        {
            //off the mesh
            if (NavMesh.SamplePosition(destPos, out hit, 4.1f, NavMesh.AllAreas))
            {
                enemyAI.navAgent.SetDestination(hit.position);
            }
        }
    }
}
