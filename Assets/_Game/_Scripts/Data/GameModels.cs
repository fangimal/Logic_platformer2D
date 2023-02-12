using System;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public delegate void PlayerDataUpdateHandler(PlayerData playerData, bool save);

    [Serializable]
    public class LevelData
    {
        public int currentlevel;
        public int lastOpenLevel;
        public int maxLevels;
        public bool isOpenAllLevel;
        public List<int> levelsHintData;

        public LevelData()
        {
            levelsHintData = new List<int>() { 0 };
            lastOpenLevel = 1;
            isOpenAllLevel = false;
        }
    }

    [Serializable]
    public class SettingsData
    {
        //public bool vibrationIsOn;
        public bool soundIsOn;
        public bool musicIsOn;
        public bool saveLang;
        public int langIndex;

        public SettingsData()
        {
            soundIsOn = true;
            musicIsOn = true;
            saveLang = false;
            langIndex = 1;
        }
    }

    [Serializable]
    public class PlayerData
    {
        public int currentSkinNumber;

        public int lastUnlockSkin;

        public event PlayerDataUpdateHandler OnPlayerDataUpdated;

        public void Update(bool save = true)
        {
            OnPlayerDataUpdated?.Invoke(this, save);
        }
    }

}
