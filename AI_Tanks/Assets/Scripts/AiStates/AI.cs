using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Complete;

public class AI : AdvancedFSM
{
    public GameObject Player;
    public GameObject Turret;
    public GameObject[] waypoints;

    public static int SLOT_DIST = 1;
    public static int WAYPOINT_DIST = 1;

    public int m_CharNumber = 1;
    public SlotManager coverPositionsSlotManager;
    public NavMeshAgent navAgent;

    [HideInInspector]
    public Rigidbody rigBody;

    public int attackRange = 20;
    public int chaseRange = 35;
    public float health = 50;
    public int damage = 10;

    private SlotManager playerSlotManager;

    public SlotManager GetPlayerSlot()
    {
        return playerSlotManager;
    }
    private string GetStateString()
    {
        string state = "NONE";
        if (CurrentState != null)
        {
            if (CurrentState.ID == FSMStateID.Dead)
            {
                state = "DEAD";
            }
            else if (CurrentState.ID == FSMStateID.Patrolling)
            {
                state = "PATROL";
            }
            else if (CurrentState.ID == FSMStateID.Chasing)
            {
                state = "CHASE";
            }
            else if (CurrentState.ID == FSMStateID.Attacking)
            {
                state = "ATTACK";
            }
            else if (CurrentState.ID == FSMStateID.Hiding)
            {
                state = "HIDING";
            }
        }

        return state;
    }

    // Initialize the FSM for the NPC tank.
    protected override void Initialize()
    {
        // Find the Player and init appropriate data.
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerSlotManager = objPlayer.GetComponent<SlotManager>();

        rigBody = GetComponent<Rigidbody>();

        // Create the FSM for the tank.
        ConstructFSM();

    }

    // Update each frame.
    protected override void FSMUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.Reason();
            CurrentState.Act();
        }
    }

    private void ConstructFSM()
    {
        AttackState attack = new AttackState(this);
        attack.AddTransition(Transition.Enable, FSMStateID.Patrolling);
        attack.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        attack.AddTransition(Transition.LostPlayer, FSMStateID.Patrolling);
        attack.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
        attack.AddTransition(Transition.Hiding, FSMStateID.Hiding);

        AddFSMState(attack);
    }

    private void OnEnable()
    {
        if (navAgent)
            navAgent.isStopped = false;
        if (CurrentState != null)
            PerformTransition(Transition.Enable);
    }
    private void OnDisable()
    {
        if (navAgent && navAgent.isActiveAndEnabled)
            navAgent.isStopped = true;
    }
}
