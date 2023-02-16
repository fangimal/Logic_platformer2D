using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer.UI
{
    public class LevelRoomUI : MonoBehaviour
    {
        [SerializeField] private LevelRoomItemUI levelItemPrefab;

        [SerializeField] private Transform pageOne;
        [SerializeField] private Transform contentOnePage;
        [SerializeField] private Transform pageTwo;

        [SerializeField] private Button backStartButton;
        [SerializeField] private Button backOnePageButton;

        private List<LevelRoomItemUI> levelRoomItemUIs = new List<LevelRoomItemUI>();

        public event Action OnBackClick;
        public event Action<int> OnLevelClicked;
        public event Action OnButtonClicked;


        private void Start()
        {
            backStartButton.onClick.AddListener(() =>
            {
                OnBackClick?.Invoke();
                OnButtonClicked?.Invoke();
            });
        }

        public void Open(LevelData levelData)
        {
            gameObject.SetActive(true);

            if (levelRoomItemUIs.Count!=0)
            {
                for (int i = 0; i < levelRoomItemUIs.Count; i++)
                {
                    Destroy(levelRoomItemUIs[i].gameObject);
                }
                levelRoomItemUIs.Clear();
            }

            for (int i = 0; i < levelData.maxLevels; i++)
            {
                LevelRoomItemUI levelRoomItemUI = Instantiate(levelItemPrefab, contentOnePage);

                int index = i + 1;

                levelRoomItemUI.Init(index, levelData);

                levelRoomItemUI.OnClick += () =>
                {
                    OnLevelClicked?.Invoke(index);
                    OnButtonClicked?.Invoke();
                };

                levelRoomItemUIs.Add(levelRoomItemUI);
            }
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
