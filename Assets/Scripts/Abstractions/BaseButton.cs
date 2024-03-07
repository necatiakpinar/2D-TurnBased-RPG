using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Abstractions
{
    [RequireComponent(typeof(Button))]
    public abstract class BaseButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        
        private Button _button;
        private void Awake()
        {
            TryGetComponent(out _button);
            TryGetComponent(out _label);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        public virtual void Init(Action buttonAction)
        {
            _button.onClick.AddListener((() => buttonAction()));
        }
        public virtual void Init(Action buttonAction, string buttonName)
        {
            _button.onClick.AddListener((() => buttonAction()));
            _label.text = buttonName;
        }

        public void Interact()
        {
            _button.onClick?.Invoke();
        }
        
    }
}