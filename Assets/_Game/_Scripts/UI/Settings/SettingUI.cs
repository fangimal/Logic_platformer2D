using System;
using System.Collections.Generic;
using TMPro;
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

        [Space(5), Header("Localization")]
        [SerializeField] private LocalSelectedItem localSelectedPrefab;
        [SerializeField] private Transform localContent;
        [SerializeField] private TextMeshProUGUI currentLanguage;

        private SettingsData settingsData;

        public List<LocalSelectedItem> localSelectedItemUIs;

        private Action onChanged;
        public event Action OnCloseButton;
        public event Action OnButtonClicked;
        public event Action<int> OnLanguageChanged;

        private void Awake()
        {
            closeButton.onClick.AddListener(() =>
            {
                Close();
                OnCloseButton?.Invoke();
            });
        }

        public void Init(SettingsData settingsData, LocalizationConfig localConfig)
        {
            this.settingsData = settingsData;

            vibrationToggle.OnClicked += (bool isOn) =>
            {
                settingsData.vibrationIsOn = isOn;
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

            ShowLangItems(localConfig);
            //currentLanguage.text = localConfig.GetLocalDatas[settingsData.langIndex].name;
        }

        public void Open(Action onChanged)
        {
            gameObject.SetActive(true);

            this.onChanged = onChanged;

            vibrationToggle.SetIsOnOf(settingsData.vibrationIsOn);

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

        private void ShowLangItems(LocalizationConfig localConfig)
        {
            localSelectedItemUIs = new List<LocalSelectedItem>();

            for (int i = 0; i < localConfig.GetLocalDatas.Length; i++)
            {
                LocalSelectedItem localSelectedItemUI = Instantiate(localSelectedPrefab, localContent);

                int index = i;
                localSelectedItemUI.SetLang(localConfig.GetLocalDatas[i], index);

                localSelectedItemUI.OnClick += (ind) =>
                {
                    OnLanguageChanged?.Invoke(ind);
                    OnButtonClicked?.Invoke();
                    //currentLanguage.text = localConfig.GetLocalDatas[ind].name;
                };

                localSelectedItemUIs.Add(localSelectedItemUI);
            }
        }
    }
}
