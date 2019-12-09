using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class FleeState : State<AI>
{
    private static FleeState _instance;

    private FleeState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }
    public static FleeState Instance
    {
        get
        {
            if (_instance == null)
            {
                new FleeState();
            }
            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
        Debug.Log("Entering Flee State");
    }

    public override void ExitState(AI _owner)
    {
        Debug.Log("Exiting Flee State");
    }

    public override void UpdateState(AI _owner)
    {
        if(!_owner.LowHealth)
        {
            _owner.stateMachine.ChangeState(WanderState.Instance);
        }
        else if (_owner.NoHealth)
        {
            _owner.stateMachine.ChangeState(DeathState.Instance);
        }

    }
}
