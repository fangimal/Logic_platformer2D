using LogicPlatformer.Data;
using UnityEngine;

namespace LogicPlatformer
{
    public class SettingsManager : MonoBehaviour
    {
        private SettingsData settingsData;

        public SettingsData GetSettingsData => settingsData;

        public void Init(IDataManager dataManager)
        {
            settingsData = dataManager.GetSettingsData();
        }
    }
}
