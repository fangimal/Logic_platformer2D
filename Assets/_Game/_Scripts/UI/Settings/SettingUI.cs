using System;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer
{
    public class SettingUI : MonoBehaviour
    {
        [SerializeField] private Button closeButton;

        [SerializeField] private SettingsToggleUI musicToggle;
        [SerializeField] private SettingsToggleUI soundToggle;
        [SerializeField] private SettingsToggleUI vibrationToggle;

        private SettingsData settingsData;

        private Action onChanged;

        public event Action OnCloseButton;
        public event Action OnButtonClicked;

        private void Awake()
        {
            closeButton.onClick.AddListener(() =>
            {
                Close();
                OnCloseButton?.Invoke();
            });
        }

        public void Init(SettingsData settingsData)
        {
            this.settingsData = settingsData;

            vibrationToggle.OnClicked += (bool isOn) =>
            {
                //settingsData.vibrationIsOn = isOn;
                onChanged?.Invoke();
                OnButtonClicked?.Invoke();
            };

            soundToggle.OnClicked += (bool isOn) =>
            {
                settingsData.soundIsOn = isOn;
                onChanged?.Invoke();
                OnButtonClicked?.Invoke();
            };

            musicToggle.OnClicked += (bool isOn) =>
            {
                settingsData.musicIsOn = isOn;
                onChanged?.Invoke();
                OnButtonClicked?.Invoke();
            };
        }

        public void Open(Action onChanged)
        {
            gameObject.SetActive(true);

            OnButtonClicked?.Invoke();

            this.onChanged = onChanged;

            //vibrationToggle.SetIsOnOf(settingsData.vibrationIsOn);

            soundToggle.SetIsOnOf(settingsData.soundIsOn);

            musicToggle.SetIsOnOf(settingsData.musicIsOn);

            Time.timeScale = 0.0f;
        }

        public void Close()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1.0f;
            OnButtonClicked?.Invoke();
        }
    }
}
