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
        if(_owner.InRange)
        {
            _owner.stateMachine.ChangeState(AttackState.Instance);
        }
        else if (_owner.LowHealth)
        {
            _owner.stateMachine.ChangeState(FleeState.Instance);
        }
        if(!_owner.SeenPlayer)
        {
            _owner.stateMachine.ChangeState(WanderState.Instance);
        }
        else if (_owner.NoHealth)
        {
            _owner.stateMachine.ChangeState(DeathState.Instance);
        }
    }
}
