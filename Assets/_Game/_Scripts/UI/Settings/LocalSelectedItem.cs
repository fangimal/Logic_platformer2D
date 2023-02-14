using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer
{
    public class LocalSelectedItem : MonoBehaviour
    {
        [SerializeField] private Button selectedButton;
        [SerializeField] private TextMeshProUGUI langName;
        [SerializeField] private Image langImage;

        private int index;

        public event Action<int> OnClick;
        private void Awake()
        {
            selectedButton.onClick.AddListener(() =>
            {
                OnClick?.Invoke(index);
            });
        }
        public void SetLang(LocalData data, int index)
        {
            langName.text = data.name;
            langImage.sprite = data.langImage;
            this.index = index;
        }
    }
}
