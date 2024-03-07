using System;
using Abstractions;
using Misc;
using UnityEngine;

namespace GameplayStates
{
    public class EndState : BasePhaseState
    {
        private Action<PhaseStateType, StateInfoTransporter> _changeStateCallback;
        private bool _isWin;
        
        public EndState(Action<PhaseStateType, StateInfoTransporter> changeStateCallback)
        {
            _changeStateCallback = changeStateCallback;
        }
        public override void Start(StateInfoTransporter stateInfoTransporter)
        {
            _isWin = stateInfoTransporter.IsWin;
            EventManager.Notify(ActionType.OnLevelFinished, new object[]{_isWin});
        }

        public override void End()
        {
            
        }
    }
}