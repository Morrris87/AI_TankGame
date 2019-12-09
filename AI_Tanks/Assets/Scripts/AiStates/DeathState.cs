using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class DeathState : State<AI>
{
    private static DeathState _instance;

    private DeathState()
    {
        if (_instance != null)
        {
            return;
        }

        _instance = this;
    }
    public static DeathState Instance
    {
        get
        {
            if (_instance == null)
            {
                new DeathState();
            }
            return _instance;
        }
    }

    public override void EnterState(AI _owner)
    {
        Debug.Log("Entering Death State");
    }

    public override void ExitState(AI _owner)
    {
        Debug.Log("Exiting Daeth State");
    }

    public override void UpdateState(AI _owner)
    {

    }

    public override void Act(AI _owner)
    {
        throw new System.NotImplementedException();
    }
}
