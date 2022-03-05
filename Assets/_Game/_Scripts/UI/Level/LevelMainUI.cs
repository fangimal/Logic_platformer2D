using System;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer.UI
{
    internal class LevelMainUI : ILevelMainUI
    {
        [SerializeField] private Button _exitButton;

        public override event Action OnClickExitButton;

        void Start()
        {
            _exitButton.gameObject.SetActive(false);

            _exitButton.onClick.AddListener(()=> 
            {
                OnClickExitButton?.Invoke();
            });
        }
        public override void Init()
        {
            Debug.Log("Init: " + name);
        }

        public override void ShowExitButton(bool show)
        {
            _exitButton.gameObject.SetActive(show);
        }
    }
}
