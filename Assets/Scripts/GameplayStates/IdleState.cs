using System;
using Abstractions;
using Misc;

namespace GameplayStates
{
    public class IdleState : BasePhaseState
    {
        private Action<PhaseStateType, StateInfoTransporter> _changeStateCallback;

        public IdleState(Action<PhaseStateType, StateInfoTransporter> changeStateCallback)
        {
            _changeStateCallback = changeStateCallback;
        }

        public override void Start(StateInfoTransporter stateInfoTransporter)
        {
            throw new System.NotImplementedException();
        }

        public override void End()
        {
            throw new System.NotImplementedException();
        }
    }
}