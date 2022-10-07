using System;
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

        public event Action OnBackClick;

        private void Start()
        {
            backStartButton.onClick.AddListener(() =>
            {
                OnBackClick?.Invoke();
            });
        }

        public void Open(GameConfig gameConfig)
        {
            gameObject.SetActive(true);

            for (int i = 0; i < gameConfig.GetMaxLevel; i++)
            {
                LevelRoomItemUI levelRoomItemUI = Instantiate(levelItemPrefab, contentOnePage);
            }
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
