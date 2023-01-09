using System;
using UnityEngine;

namespace LogicPlatformer.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private QuestManager questManager;
        [SerializeField] private Transform startPlayerPosition;
        [SerializeField] private LevelHellper[] levelHellpers;
        public Transform GetStartPlayerPosition => startPlayerPosition;
        public LevelHellper[] GetLevelHelpers => levelHellpers;

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
    public struct LevelHellper
    {
        public string Hint;
    }
}
