using LogicPlatformer.Data;
using UnityEngine;

namespace LogicPlatformer.Settings
{
    public abstract class ISettingsManager : MonoBehaviour
    {
        private SettingsData settingsData;

        public SettingsData GetSettingsData => settingsData;

        public virtual void Init(IDataManager dataManager)
        {
            settingsData = dataManager.GetSettingsData();
        }
    }
}
