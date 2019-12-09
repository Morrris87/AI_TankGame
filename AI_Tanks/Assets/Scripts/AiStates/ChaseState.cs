using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class ChaseState : State<AI>
{
    private static ChaseState _instance;

    private ChaseState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }
    public static ChaseState Instance
    {
        get
        {
            if (_instance == null)
            {
                new ChaseState();
            }
            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
        Debug.Log("Entering Chase State");
    }

    public override void ExitState(AI _owner)
    {
        Debug.Log("Exiting Chase State");
    }

    public override void UpdateState(AI _owner)
    {
        float dist = Vector3.Distance(_owner.transform.position, _owner.Player.transform.position);

        if (_owner.health <= 0)
        {
            _owner.stateMachine.ChangeState(DeathState.Instance);
        }

        if (dist < _owner.attackRange)
        {
            _owner.stateMachine.ChangeState(AttackState.Instance);
        }
        else if (_owner.health < 15)
        {
            _owner.stateMachine.ChangeState(FleeState.Instance);
        }
        else if(dist > _owner.chaseRange)
        {
            _owner.stateMachine.ChangeState(WanderState.Instance);
        }
    }

    public override void Act(AI _owner)
    {
        throw new System.NotImplementedException();
    }
}
