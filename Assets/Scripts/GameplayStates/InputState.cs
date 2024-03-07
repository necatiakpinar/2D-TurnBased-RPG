using System;
using Abstractions;
using Interfaces;
using Managers;
using Misc;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameplayStates
{
    public class InputState : BasePhaseState, IPointerDownHandler, IPointerUpHandler
    {
        private Action<PhaseStateType, StateInfoTransporter> _changeStateCallback;
        private BaseHero _selectedHero;
        private BaseEnemy _selectedEnemy;
        private Camera _mainCamera;

        public  InputState(Action<PhaseStateType, StateInfoTransporter> changeStateCallback)
        {
            _changeStateCallback = changeStateCallback;
            _mainCamera = Camera.main;
        }

        public override void Start(StateInfoTransporter stateInfoTransporter)
        {
            _selectedHero = null;
            _selectedEnemy = null;
        }

        public override void End()
        {
            StateInfoTransporter stateInfoTranporter = new StateInfoTransporter(_selectedHero, _selectedEnemy);
            _changeStateCallback.Invoke(PhaseStateType.Decision, stateInfoTranporter);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            HandleInput();
        }

        private void HandleInput()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D characterHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, Constants.LAYER_CHARACTER);
            Debug.DrawLine(ray.origin, ray.direction * 100, Color.red, 5f);

            if (characterHit.collider != null)
            {
                var hero = characterHit.collider.gameObject.GetComponent<BaseHero>();
                var enemy = characterHit.collider.gameObject.GetComponent<BaseEnemy>();
                
                if (hero != null)
                {
                    if (_selectedHero)
                        _selectedHero.Deselect();
                    
                    _selectedHero = hero;
                    _selectedHero.Select();
                }
                else if (_selectedHero && enemy != null)
                {
                    if (_selectedHero)
                        _selectedHero.Deselect();
                    
                    _selectedEnemy = enemy;
                    _selectedEnemy.Select();
                }
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_selectedHero == null || _selectedEnemy == null)
                return;

            End();
        }
    }
}