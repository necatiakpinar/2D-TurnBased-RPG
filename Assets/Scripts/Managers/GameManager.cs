using System;
using Data.PersistentData;
using Misc;
using UnityEngine;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}