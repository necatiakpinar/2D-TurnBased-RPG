using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Helpers
{
    public static class FileHelper
    {
        public async static Task<bool> WriteToFile(string filePath, string fileContents)
        {
            try
            {
                await File.WriteAllTextAsync(filePath, fileContents);
                return true;
            }
            catch (Exception e)
            {
              //  Debug.LogError($"Failed to write to {filePath} with exception {e}");
                return false;
            }
        }

        public static bool IsThereFileByThatName(string fileName)
        {
            return File.Exists(Path.Combine(Application.persistentDataPath, fileName));
        }

        public async static Task<Tuple<bool, string>> LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Debug.Log("There is no file at the path");
                return Tuple.Create(false, default(string));
            }

            try
            {
                string result = await File.ReadAllTextAsync(filePath);
                return Tuple.Create(true, result);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to read from {filePath} with exception {e}");
                return Tuple.Create(false, default(string));
            }
        }


        public static bool MoveFile(string fileName, string newFileName)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, fileName);
            var newFullPath = Path.Combine(Application.persistentDataPath, newFileName);

            try
            {
                if (File.Exists(newFullPath))
                {
                    File.Delete(newFullPath);
                }

                if (!File.Exists(fullPath))
                {
                    return false;
                }

                File.Move(fullPath, newFullPath);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to move file from {fullPath} to {newFullPath} with exception {e}");
                return false;
            }

            return true;
        }
    }
}