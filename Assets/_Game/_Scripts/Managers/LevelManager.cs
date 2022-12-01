using System;
using UnityEngine;

namespace LogicPlatformer.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private QuestManager questManager;
        [SerializeField] private Transform startPlayerPosition;
        public Transform GetStartPlayerPosition => startPlayerPosition;

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
            //OnExitLevel?.Invoke();
            Debug.Log("Clicked select!");
        }

    }
}
