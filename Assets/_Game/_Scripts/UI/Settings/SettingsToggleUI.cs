using System;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer
{
    public class SettingsToggleUI : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image toggleOn_Image;
        [SerializeField] private Image toggleOff_Image;

        public Action<bool> OnClicked;

        public bool IsOn { get; private set; }

        private void Awake()
        {
            button.onClick.AddListener(() => 
            { 
                IsOn= !IsOn;
                SetIsOnOf(IsOn);
                OnClicked?.Invoke(IsOn);
            });
        }

        public void SetIsOnOf(bool isOn)
        {
            IsOn = isOn;
            toggleOn_Image.gameObject.SetActive(isOn);
            toggleOff_Image.gameObject.SetActive(!isOn);
        }
    }
}
