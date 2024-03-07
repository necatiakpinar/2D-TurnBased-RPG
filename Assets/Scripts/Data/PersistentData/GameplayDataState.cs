using System;
using System.Threading.Tasks;
using Helpers;
using Misc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data.PersistentData
{
    public static class GameplayDataState
    {
        private static GameplayData _gameplayData;
        public static GameplayData GameplayData => _gameplayData;

        private static string _filePath =
            $"{Application.persistentDataPath}/{Constants.PLAYERDATA_FILENAME}.{Constants.SAVEFILE_EXTENSION}";

        public async static void ResetData()
        {
            _gameplayData = new GameplayData();
            await SaveDataToDisk();
        }

        public static async Task SaveDataToDisk()
        {
            var data = JsonUtility.ToJson(_gameplayData, true);
            await FileHelper.WriteToFile(_filePath, data);
        }

        public static async Task LoadSaveDataFromDisk()
        {
            GameplayData playerData;

            var (success, diskData) = await FileHelper.LoadFromFile(_filePath);

            if (success)
            {
                try
                {
                    playerData = JsonUtility.FromJson<GameplayData>(diskData);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    playerData = new GameplayData();
                }
            }
            else
            {
                Debug.Log("There isn't a disk data.");
                playerData = new GameplayData();
                _gameplayData = playerData;
                await SaveDataToDisk(); // Save data if there is no existing data on disk
            }

            _gameplayData = playerData;
        }
    }
}