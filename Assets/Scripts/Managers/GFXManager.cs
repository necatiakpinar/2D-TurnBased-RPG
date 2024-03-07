using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Abstractions;
using Data.ScriptableObjects;
using UnityEngine;

namespace Managers
{
    public class GFXManager : MonoBehaviour
    {
        [SerializeField] private GameObject _audioSourcePrefab;

        private VFXContainerSO _soVFXDataContainer;
        private SFXContainerSO _soSfxDataContainer;

        private Dictionary<string, AudioClip> _sfxDictionary;
        private Dictionary<string, BaseVFXMono> _vfxDictionary;

        private Queue<AudioSource> _audioSources = new Queue<AudioSource>();
        private readonly int _sfxPoolSize = 3;
        private readonly int _vfxPoolSize = 5;

        private Dictionary<string, Queue<BaseVFXMono>> _vfxPool = new Dictionary<string, Queue<BaseVFXMono>>();

        public static GFXManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }

        private void Start()
        {
            _soSfxDataContainer = DataManager.Instance.SFXContainer;
            _soVFXDataContainer = DataManager.Instance.VFXContainer;

            _sfxDictionary = _soSfxDataContainer.GameplaySFXes.ToDictionary(sfx => sfx.Name, sfx => sfx.Clip);
            _vfxDictionary = _soVFXDataContainer.GameplayVFXes.ToDictionary(vfx => vfx.Key, vfx => vfx.VfxObject);

            CreateGFXPools();
        }

        private void CreateGFXPools()
        {
            //SFX
            AudioSource source;
            for (int i = 0; i < _sfxPoolSize; i++)
            {
                source = Instantiate(_audioSourcePrefab).GetComponent<AudioSource>();
                source.gameObject.SetActive(false);
                _audioSources.Enqueue(source);
            }

            //VFX
            foreach (GameVFX vfxData in _soVFXDataContainer.GameplayVFXes)
            {
                if (!vfxData.IsPoolObject)
                    continue;

                var queue = new Queue<BaseVFXMono>();
                for (int i = 0; i < vfxData.PoolSize; i++)
                {
                    var vfx = Instantiate(vfxData.VfxObject);
                    vfx.Init(vfxData.Key);
                    queue.Enqueue(vfx);
                }

                _vfxPool.Add(vfxData.Key, queue);
            }
        }

        public GFXManager PlaySFX(string audioKey, float volume = 1)
        {
            if (_audioSources.Count == 0) return this;

            AudioSource source;
            _sfxDictionary.TryGetValue(audioKey, out var clip);
            if (clip)
            {
                source = _audioSources.Dequeue();
                source.clip = clip;
                source.volume = volume;
                source.gameObject.SetActive(true);
                source.Play();
                StartCoroutine(ReturnToPool(source));
            }

            return this;
        }

        private IEnumerator<YieldInstruction> ReturnToPool(AudioSource source)
        {
            yield return new WaitForSeconds(source.clip.length);
            source.gameObject.SetActive(false);
            _audioSources.Enqueue(source);
        }

        public GFXManager PlayVFX(string name, Vector3 vfxPosition, Quaternion vfxRotation, params object[] parameters)
        {
            if (_vfxPool.TryGetValue(name, out var queue))
            {
                BaseVFXMono vfx;
                if (queue.Count > 0)
                {
                    vfx = queue.Dequeue();
                    StartCoroutine(vfx.Play(parameters));
                }
                else
                {
                    _vfxDictionary.TryGetValue(name, out var vfxPrefab);
                    vfx = Instantiate(vfxPrefab);
                    StartCoroutine(vfx.Play(parameters));
                }

                vfx.transform.position = vfxPosition;
                vfx.transform.rotation = vfxRotation;
            }

            return this;
        }
        
        public void VFXReturnToPool(string key, BaseVFXMono vfx)
        {
            _vfxPool[key].Enqueue(vfx);
        }
    }
}