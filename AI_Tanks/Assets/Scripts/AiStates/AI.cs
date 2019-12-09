using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateStuff;

public class AI : MonoBehaviour
{
    public bool SwitchState = false;
    public bool SeenPlayer = false;
    public bool LowHealth = false;
    public bool InRange = false;
    public bool NoHealth = false;

    float range = 10;
    float health = 50;
    public FSM<AI> stateMachine { get; set; }

    private void Start()
    {
        stateMachine = new FSM<AI>(this);
        stateMachine.ChangeState(WanderState.Instance);
    }
    private void Update()
    {
        //stuff here for switching between states
        stateMachine.Update();
    }
}
