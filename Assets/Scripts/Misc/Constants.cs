using UnityEngine;

namespace Misc
{
    public class Constants
    {
        #region File
        public static readonly string PLAYERDATA_FILENAME = "GameData";
        public static readonly string SAVEFILE_EXTENSION = "NA";
        #endregion

        #region Unity 
        public static readonly int LAYER_CHARACTER = LayerMask.GetMask("Character");
        #endregion

        #region Gameplay
            #region Heroes
            public static readonly float LEVELUP_MULTIPLIER = 0.10f;
            public static readonly int REQUIREDEXP_TOLEVELUP = 5;
            #endregion

            #region Enemies
            public static readonly float ENEMY_STAT_INCREASER = 0.05f;
            #endregion
            
        #endregion
        
        #region HeroSelection
        public static readonly int HERO_SELECTION_COUNT = 3;
        public static readonly float HERO_INFO_HOLD_DURATION = 3f;
        #endregion
        
        #region Scenes
        public static readonly string SCENE_HEROSELECTION = "HeroSelectionScene";
        public static readonly string SCENE_GAMEPLAY = "GameplayScene";
        #endregion
        
        #region VFX Keys
        public static readonly string VFX_DAMAGETEXT = "VFX_DamageText";
        #endregion
        
        #region VFX Keys
        public static readonly string SFX_MELEE_HIT = "SFX_MeleeHit";
        #endregion
    }
}