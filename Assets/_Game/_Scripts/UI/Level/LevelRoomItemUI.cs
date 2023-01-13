using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer.UI
{
    public class LevelRoomItemUI : MonoBehaviour
    {
        [SerializeField] private Button itemButton;
        [SerializeField] private TextMeshProUGUI chapterText;
        [SerializeField] private Color curentLevelColor;
        [SerializeField] private Image image;
        [SerializeField] private Transform lockImg;

        public event Action OnClick;

        private Color defaultColor;
        private int levelIndex;

        private void Awake()
        {
            defaultColor = image.color;

            itemButton.onClick.AddListener(() =>
            {
                OnClick?.Invoke();
            });
        }

        public void Init(int index, LevelData levelData)
        {
            levelIndex = index;

            if (levelData.lastOpenLevel > index )
            {
                lockImg.gameObject.SetActive(false);
                curentLevelColor = defaultColor;
                itemButton.interactable = true;
            }
            else if (levelData.lastOpenLevel == index)
            {
                lockImg.gameObject.SetActive(false);
                image.color = curentLevelColor;
                itemButton.interactable = true;
            }
            else
            {
                lockImg.gameObject.SetActive(!levelData.isOpenAllLevel);
                curentLevelColor = defaultColor;
                itemButton.interactable = levelData.isOpenAllLevel;
            }

            chapterText.text = levelIndex.ToString();
        }
    }
}
