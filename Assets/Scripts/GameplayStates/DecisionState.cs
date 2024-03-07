using System;
using System.Collections;
using System.Collections.Generic;
using Abstractions;
using DG.Tweening;
using Managers;
using Misc;
using UnityEngine;

namespace GameplayStates
{
    public class DecisionState : BasePhaseState
    {
        private Action<PhaseStateType, StateInfoTransporter> _changeStateCallback;
        private BaseHero _selectedHero;
        private BaseEnemy _selectedEnemy;

        private readonly float _attackDuration = 1.0f;
        private readonly float _attackOffset = -2.0f;
        public DecisionState(Action<PhaseStateType, StateInfoTransporter> changeStateCallback)
        {
            _changeStateCallback = changeStateCallback;
        }

        public override void Start(StateInfoTransporter stateInfoTransporter)
        {
            _selectedHero = stateInfoTransporter.SelectedHero;
            _selectedEnemy = stateInfoTransporter.SelectedEnemy;

            GameManager.Instance.StartCoroutine(CO_AttackTheEnemy());
        }

        private IEnumerator CO_AttackTheEnemy()
        {
            var startingPosition = _selectedHero.transform.position;
            var attackPosition = new Vector2(_selectedEnemy.transform.position.x + _attackOffset, _selectedEnemy.transform.position.y);
            _selectedHero.transform.DOMove(attackPosition, _attackDuration);
            
            yield return new WaitForSeconds(_attackDuration);
            var selectedHeroAttackPower = _selectedHero.Attributes.AttackPower;
            GFXManager.Instance.PlayVFX(Constants.VFX_DAMAGETEXT, _selectedEnemy.transform.position, Quaternion.identity, _selectedEnemy.transform.position, selectedHeroAttackPower);
            
            _selectedHero.Attack(_selectedEnemy);
            _selectedHero.transform.DOMove(startingPosition, _attackDuration);
            yield return new WaitForSeconds(_attackDuration);
            
            End();
        }

        public override void End()
        {
            bool isEnemiesDead = EventManager.NotifyWithReturn<bool>(FunctionType.IsAllEnemiesDead);
            if (isEnemiesDead)
                _changeStateCallback.Invoke(PhaseStateType.End, new StateInfoTransporter(true));    
            else
                _changeStateCallback.Invoke(PhaseStateType.EnemyTurn, new StateInfoTransporter(_selectedHero, _selectedEnemy));
        }
    }
}