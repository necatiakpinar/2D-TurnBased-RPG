namespace Addressables
{
    public static class AddressableKeys
    {
        public enum AssetKeys
        {
            //Scriptable Objects
            HeroDataContainer_SO,
            EnemyDataContainer_SO,
            VFXContainer_SO,
            SFXContainer_SO,
            
            //UI Widgets
            HeroCardWidget
            
        }
        public static string GetKey(AssetKeys key)
        {
            return key.ToString();
        }
    }

}