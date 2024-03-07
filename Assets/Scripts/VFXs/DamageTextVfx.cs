using System;
using System.Collections;
using Abstractions;
using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;

namespace VFXs
{
    public class DamageTextVfx : BaseVFXMono
    {
        [SerializeField] private TMP_Text _damageLabel;
        private Vector3 _targetPosition;
        
        private readonly Vector3 _offset = new Vector3(0, 2, 0);
        private readonly float _duration = 1f;
        private readonly float _labelFadeAmount = 0f;
        
        public override void Init(string key)
        {
            base.Init(key);
            _waitForEnding = new WaitForSeconds(_duration);
        }

        public override IEnumerator Play(params object[] parameters)
        {
            transform.gameObject.SetActive(true);
            
            var targetPosition = (Vector3)parameters[0];
            var damage = (int)parameters[1];
            
            _targetPosition =  targetPosition + _offset;
            _damageLabel.text = damage.ToString();
            
            transform.DOMoveY(_targetPosition.y, _duration);
            _damageLabel.DOFade(_labelFadeAmount, _duration);

            yield return _waitForEnding;
            
            ReturnToPool();
        }
        
        private void OnDisable()
        {
            _damageLabel.DOFade(1f, 0f);
        }
        
    }
}