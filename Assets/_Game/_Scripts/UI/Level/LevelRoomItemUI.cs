using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace LogicPlatformer.UI
{
    public class LevelRoomItemUI : MonoBehaviour
    {
        [SerializeField] private Button itemButton;
        [SerializeField] private Image chapterImage;
        [SerializeField] private Image lockImage;
        [SerializeField] private TextMeshProUGUI chapterText;

        public event Action OnClick;

        private bool isActive;

        private void Awake()
        {
            itemButton.onClick.AddListener(() => 
            {
                if (isActive)
                {
                    OnClick?.Invoke();
                }
            });
        }
    }
}
