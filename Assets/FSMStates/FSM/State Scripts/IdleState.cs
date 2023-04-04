using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.States
{
    [CreateAssetMenu(fileName = "IdleState", menuName="Unity-FSM/States/Idle")]
    public class IdleState: AbstractFSMState
    {
        public override bool EnterState()
        {
            base.EnterState();
            return true;

        }
        public override void UpdateState()
        {
            
        }

        public override bool ExitState()
        {
            base.ExitState();
            return true;
        }
    }
}

