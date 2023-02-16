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
            Debug.Log("Clicked select!");
        }

    }

    [Serializable]
    public struct LevelHellper
    {
        public string Hint;
    }
}
