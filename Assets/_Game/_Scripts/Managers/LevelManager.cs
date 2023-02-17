using System;
using TMPro;
using UnityEngine;

namespace LogicPlatformer.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private QuestManager questManager;
        [SerializeField] private Transform startPlayerPosition;
        [SerializeField] private TextMeshProUGUI levelHeaderText;
        [SerializeField] private LevelDataForPlayer levelDataForPlayer;
        public Transform GetStartPlayerPosition => startPlayerPosition;
        public LevelDataForPlayer GetLevelDataForPlayer => levelDataForPlayer;

        public event Action OnShowSelect;
        public event Action OnHideSelect;
        public event Action OnExitLevel;

        private void Awake()
        {
            questManager.OnShowSelect += () =>
            {
                OnShowSelect?.Invoke();
            };

            questManager.OnHideSelect += () =>
            {
                OnHideSelect?.Invoke();
            };

            questManager.OnExitLevel += () =>
            {
                OnExitLevel?.Invoke();
            };
        }

        public void SelectClicked()
        {
            questManager.Select();
            Debug.Log("Clicked select!");
        }

    }

    [Serializable]
    public class LevelDataForPlayer
    {
        [Range(0.0f, 20.0f)]
        public float moveSpeed = 5;
        [Range(0.0f, 20.0f)]
        public float jumpForse = 11;
        public float gravityScale = 3;
    }

}
