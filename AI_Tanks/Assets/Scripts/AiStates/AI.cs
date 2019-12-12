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
    public FSMStateID id;

    public static int SLOT_DIST = 1;
    public static int WAYPOINT_DIST = 1;

    public int m_CharNumber = 1;
    public SlotManager coverPositionsSlotManager;
    public NavMeshAgent navAgent;

    [HideInInspector]
    public Rigidbody rigBody;

    public int attackRange = 10;
    public int chaseRange = 15;
    public int rammingRange = 5;
    public float health = 50;
    public int damage = 10;
    public int enemyClipSize = 2;

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
            else if (CurrentState.ID == FSMStateID.Fleeing)
            {
                state = "HIDING";
            }
            else if (CurrentState.ID == FSMStateID.Ramming)
            {
                state = "RAMMING";
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
            id = CurrentStateID;
        }
    }

    private void ConstructFSM()
    {
        WanderState wander = new WanderState(this);
        wander.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        wander.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
        wander.AddTransition(Transition.Hiding, FSMStateID.Fleeing);
        wander.AddTransition(Transition.Enable, FSMStateID.Patrolling);

        AttackState attack = new AttackState(this);
        attack.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        attack.AddTransition(Transition.Enable, FSMStateID.Patrolling);
        attack.AddTransition(Transition.LostPlayer, FSMStateID.Patrolling);
        attack.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
        attack.AddTransition(Transition.Hiding, FSMStateID.Fleeing);
                
        FleeState flee = new FleeState(this);
        flee.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        flee.AddTransition(Transition.LostPlayer, FSMStateID.Patrolling);
        flee.AddTransition(Transition.Enable, FSMStateID.Patrolling);

        ChaseState chase = new ChaseState(this);
        chase.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        chase.AddTransition(Transition.LostPlayer, FSMStateID.Patrolling);
        chase.AddTransition(Transition.Hiding, FSMStateID.Fleeing);
        chase.AddTransition(Transition.ReachPlayer, FSMStateID.Attacking);
        chase.AddTransition(Transition.Enable, FSMStateID.Patrolling);

        //RammingState charge = new RammingState(this);
        //charge.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        //charge.AddTransition(Transition.LostPlayer, FSMStateID.Patrolling);
        //charge.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);
        //charge.AddTransition(Transition.Hiding, FSMStateID.Fleeing);
        //charge.AddTransition(Transition.ReachPlayer, FSMStateID.Attacking);
        //charge.AddTransition(Transition.Enable, FSMStateID.Patrolling);

        DeathState death = new DeathState(this);

        AddFSMState(wander);
        AddFSMState(attack);
        AddFSMState(death);
        AddFSMState(flee);
        AddFSMState(chase);
        //AddFSMState(charge);

        navAgent.speed = 3.0f;
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
