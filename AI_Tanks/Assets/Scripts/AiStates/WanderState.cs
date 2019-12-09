using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class WanderState : State<AI>
{
    private static WanderState _instance;

    private WanderState()
    {
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
        if(_owner.SeenPlayer)
        {
            _owner.stateMachine.ChangeState(ChaseState.Instance);
        }
        else if(_owner.InRange)
        {
            _owner.stateMachine.ChangeState(AttackState.Instance);
        }
        else if(_owner.LowHealth)
        {
            _owner.stateMachine.ChangeState(FleeState.Instance);
        }
        else if(_owner.NoHealth)
        {
            _owner.stateMachine.ChangeState(DeathState.Instance);
        }
    }
}
