using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer.UI
{
    public class LevelRoomItemUI : MonoBehaviour
    {
        [SerializeField] private Button itemButton;
        [SerializeField] private Image chapterImage;
        [SerializeField] private Image lockImage;
        [SerializeField] private TextMeshProUGUI chapterText;

        [SerializeField] private Sprite lockImg;
        [SerializeField] private Sprite unLockImg;
        [SerializeField] private Sprite currentLevelImg;

        public event Action OnClick;

        private int levelIndex;

        private void Awake()
        {
            itemButton.onClick.AddListener(() =>
            {
                OnClick?.Invoke();
            });
        }

        public void Init(int index, LevelData levelData)
        {
            levelIndex = index;

            if (levelData.lastOpenLevel > index)
            {
                lockImage.sprite = unLockImg;
                itemButton.interactable = true;
            }
            else if (levelData.lastOpenLevel == index)
            {
                lockImage.sprite = currentLevelImg;
                itemButton.interactable = true;
            }
            else
            {
                lockImage.sprite = lockImg;

                itemButton.interactable = false;
            }

            chapterText.text = "Level: " + levelIndex;
        }
    }
}
