using Abstractions;
using GameplayStates;
using Misc;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class PhaseManager : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
    {
        private InputState _inputState;
        private DecisionState _decisionState;
        private EnemyTurnState _enemyTurnState;
        private IdleState _idleState;
        private EndState _endState;
    
        private BasePhaseState _currentState;
    
        void Start()
        {
            _inputState = new InputState(ChangeGameState);
            _decisionState = new DecisionState(ChangeGameState);
            _enemyTurnState = new EnemyTurnState(ChangeGameState);
            _idleState = new IdleState(ChangeGameState);
            _endState = new EndState(ChangeGameState);

            StateInfoTransporter stateInfoTransporter = new StateInfoTransporter();
        
            ChangeGameState(PhaseStateType.Input, stateInfoTransporter);
        }
    
        private void ChangeGameState(PhaseStateType stateType, StateInfoTransporter stateInfoTransporter)
        {
            switch (stateType)
            {
                case PhaseStateType.Input:
                    _currentState = _inputState;
                    break;
                case PhaseStateType.Decision:
                    _currentState = _decisionState;
                    break;
                case PhaseStateType.EnemyTurn:
                    _currentState = _enemyTurnState;
                    break;
                case PhaseStateType.End:
                    _currentState = _endState;
                    break;
                case PhaseStateType.Idle:
                    _currentState = _idleState;
                    break;
            }

            _currentState.Start(stateInfoTransporter);
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            if (_currentState is IPointerDownHandler handler) handler.OnPointerDown(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_currentState is IPointerUpHandler handler) handler.OnPointerUp(eventData);
        }
    
    }
}
