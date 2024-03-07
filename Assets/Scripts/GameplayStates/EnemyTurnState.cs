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
    public class EnemyTurnState : BasePhaseState
    {
        private Action<PhaseStateType, StateInfoTransporter> _changeStateCallback;
        private BaseHero _selectedHero;
        private BaseEnemy _selectedEnemy;

        private readonly float _attackDuration = 1.0f;
        private readonly float _attackOffset = 2.0f;
        public EnemyTurnState(Action<PhaseStateType, StateInfoTransporter> changeStateCallback)
        {
            _changeStateCallback = changeStateCallback;
        }
        public override void Start(StateInfoTransporter stateInfoTransporter)
        {
            _selectedHero = stateInfoTransporter.SelectedHero;
            _selectedEnemy = stateInfoTransporter.SelectedEnemy;

            GameManager.Instance.StartCoroutine(CO_AttackTheHero());
        }

        private IEnumerator CO_AttackTheHero()
        {
            var randomHero = EventManager.NotifyWithReturn<BaseHero>(FunctionType.GetRandomHero);
            
            var startingPosition = _selectedEnemy.transform.position;
            var attackPosition = new Vector2(randomHero.transform.position.x + _attackOffset, randomHero.transform.position.y);
            _selectedEnemy.transform.DOMove(attackPosition, _attackDuration);
            
            yield return new WaitForSeconds(_attackDuration);
            var selectedEnemyAttackPower = _selectedEnemy.Attributes.AttackPower;
            GFXManager.Instance.PlayVFX(Constants.VFX_DAMAGETEXT, randomHero.transform.position, Quaternion.identity, randomHero.transform.position, selectedEnemyAttackPower);
            
            _selectedEnemy.Attack(randomHero);
            _selectedEnemy.transform.DOMove(startingPosition, _attackDuration);
            yield return new WaitForSeconds(_attackDuration);
            End();
        }

        public override void End()
        {
            bool isHeroesDead = EventManager.NotifyWithReturn<bool>(FunctionType.IsAllHeroesDead);
            if (isHeroesDead)
                _changeStateCallback.Invoke(PhaseStateType.End, new StateInfoTransporter(false));    
            else
                _changeStateCallback.Invoke(PhaseStateType.Input, new StateInfoTransporter(_selectedHero, _selectedEnemy));
        }
    }
}