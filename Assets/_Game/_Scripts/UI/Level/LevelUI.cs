using System;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer.UI
{
    internal class LevelUI : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;

        public event Action OnClickExitButton;

        void Start()
        {
            _exitButton.gameObject.SetActive(false);

            _exitButton.onClick.AddListener(()=> 
            {
                OnClickExitButton?.Invoke();
            });
        }
        public void Open(LevelData levelData)
        {
            gameObject.SetActive(true);
        }

        public void ShowExitButton(bool show)
        {
            _exitButton.gameObject.SetActive(show);
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
