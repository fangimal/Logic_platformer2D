using System;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer.UI
{
    public class LevelRoomMainUI : ILevelRoomMainUI
    {
        [SerializeField] private Transform pageOne;
        [SerializeField] private Transform pageTwo;

        [SerializeField] private Button backStartButton;
        [SerializeField] private Button backOnePageButton;

        public override event Action OnBackClick;

        private void Start()
        {
            backStartButton.onClick.AddListener(() =>
            {
                OnBackClick?.Invoke();
            });
        }

        public override void Init()
        {
            Debug.Log("Init: " + name);
        }
    }
}
