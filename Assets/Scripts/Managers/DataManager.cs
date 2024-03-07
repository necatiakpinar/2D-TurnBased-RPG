using System.Threading.Tasks;
using Addressables;
using Data.PersistentData;
using Data.ScriptableObjects;
using Misc;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class DataManager : Singleton<DataManager>
    {
        private HeroDataContainerSO _heroDataContainer;
        private EnemyDataContainerSO _enemyDataContainer;
        private VFXContainerSO _vfxContainer;
        private SFXContainerSO _sfxContainer;
        
        public HeroDataContainerSO HeroDataContainer => _heroDataContainer;
        public EnemyDataContainerSO EnemyDataContainer => _enemyDataContainer;
        public VFXContainerSO VFXContainer => _vfxContainer;
        public SFXContainerSO SFXContainer => _sfxContainer;
        
        private async void Awake()
        {
            DontDestroyOnLoad(this);
            await GameplayDataState.LoadSaveDataFromDisk();
            await LoadAddressableData();
        }

        public async Task LoadAddressableData()
        {
            _heroDataContainer = await AddressableLoader.LoadAssetAsync<HeroDataContainerSO>(AddressableKeys.GetKey(AddressableKeys.AssetKeys.HeroDataContainer_SO));
            _enemyDataContainer = await AddressableLoader.LoadAssetAsync<EnemyDataContainerSO>(AddressableKeys.GetKey(AddressableKeys.AssetKeys.EnemyDataContainer_SO));
            _vfxContainer = await AddressableLoader.LoadAssetAsync<VFXContainerSO>(AddressableKeys.GetKey(AddressableKeys.AssetKeys.VFXContainer_SO));
            _sfxContainer = await AddressableLoader.LoadAssetAsync<SFXContainerSO>(AddressableKeys.GetKey(AddressableKeys.AssetKeys.SFXContainer_SO));
            
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}