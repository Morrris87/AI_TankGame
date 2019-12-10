using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class DeathState : FSMState
{
    public DeathState(AI enemyTank)
    {
        AI enemyAI = enemyTank;
        curSpeed = 0;
        stateID = FSMStateID.Dead;
        enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {
    }

    public override void Reason()
    {
    }
}
