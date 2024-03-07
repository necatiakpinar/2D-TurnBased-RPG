using System.Collections;
using Managers;
using UnityEngine;

namespace Abstractions
{
    public abstract class BaseVFXMono : MonoBehaviour
    {
        protected string _vfxKey;
        protected WaitForSeconds _waitForEnding;
        
        public virtual void Init(string key)
        {
            _vfxKey = key;
            gameObject.SetActive(false);
        }
        
        public virtual IEnumerator Play()
        {
            yield break;
        }
        
        public virtual IEnumerator Play(params object[] parameters)
        {
            yield break;
        }
        
        public virtual void ReturnToPool()
        {
            gameObject.SetActive(false);
            GFXManager.Instance.VFXReturnToPool(_vfxKey, this);
        }
        
    }
}