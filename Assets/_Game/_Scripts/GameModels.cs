using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public delegate void PlayerDataUpdateHandler(PlayerData playerData, bool save);
    public class LevelData
    {
        public int currentlevel;
        public int lastOpenLevel;
        public int maxLevels;
    }

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

    public class LevelResult
    {
        public bool win;
    }

    public class SettingsData
    {
        public bool vibrationIsOn;
        public bool soundIsOn;
        public bool musicIsOn;
    }
}
