using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class WanderState : State<AI>
{
    private static WanderState _instance;

    int currentWaypoint;

    private WanderState()
    {
        currentWaypoint = Random.Range(0, 7);
        
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }
    public static WanderState Instance
    {
        get
        {
            if (_instance == null)
            {
                new WanderState();
            }
            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {

        Debug.Log("Entering Wander State");
    }

    public override void ExitState(AI _owner)
    {
        Debug.Log("Exiting Wander State");
    }

    public override void UpdateState(AI _owner)
    {
        float dist = Vector3.Distance(_owner.transform.position, _owner.Player.transform.position);

        if (_owner.health <= 0)
        {
            _owner.stateMachine.ChangeState(DeathState.Instance);
        }

        if (dist < _owner.chaseRange)
        {
            _owner.stateMachine.ChangeState(ChaseState.Instance);
        }
        else if(_owner.health <= 15)
        {
            _owner.stateMachine.ChangeState(FleeState.Instance);
        }
    }

    public override void Act(AI _owner)
    {
        float dist = Vector3.Distance(_owner.transform.position, _owner.waypoints[currentWaypoint].transform.position);

        if(dist < 0.1)
        {
            int temp = currentWaypoint;
            while (temp == currentWaypoint)
            {
                temp = Random.Range(0, 7);
            }
            currentWaypoint = temp;
        }
        else
        {
            Vector3 target = _owner.waypoints[currentWaypoint].transform.position - _owner.transform.position;

            Quaternion targetRot = Quaternion.LookRotation(target);

            _owner.transform.rotation = Quaternion.Slerp(_owner.transform.rotation, targetRot, 3 * Time.deltaTime);

            _owner.transform.position += _owner.transform.forward * 7 * Time.deltaTime;
        }
    }
}
