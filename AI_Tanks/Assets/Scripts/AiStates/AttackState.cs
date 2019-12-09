using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class AttackState : State<AI>
{
    private static AttackState _instance;

    private AttackState()
    {
        if(_instance != null)
        {
            return;
        }

        _instance = this;
    }
    public static  AttackState Instance
    {
        get
        {
            if(_instance == null)
            {
                new AttackState();
            }
            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
        Debug.Log("Entering Attack State");
    }

    public override void ExitState(AI _owner)
    {
        Debug.Log("Exiting Attack State");
    }

    public override void UpdateState(AI _owner)
    {
        if (_owner.LowHealth)
        {
            _owner.stateMachine.ChangeState(FleeState.Instance);
        }
        else if(!_owner.InRange)
        {
            _owner.stateMachine.ChangeState(ChaseState.Instance);
        }
        else if(!_owner.SeenPlayer)
        {
            _owner.stateMachine.ChangeState(WanderState.Instance);
        }
        else if (_owner.NoHealth)
        {
            _owner.stateMachine.ChangeState(DeathState.Instance);
        }
    }
}
