using System;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer
{
    public class LevelHelper : MonoBehaviour
    {
        [SerializeField] private Button okButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private Transform onePage;
        [SerializeField] private Transform twoPage;
        [SerializeField] private Transform hintContent;
        [SerializeField] private Button rewardButton;
        [SerializeField] private Button backGame;

        public event Action OnNeedHelpClicked;
        public event Action OnCancelClicked;
        public event Action OnBackClicked;

        private void Awake()
        {
            okButton.onClick.AddListener (() => 
            {
                onePage.gameObject.SetActive(false);
                OnNeedHelpClicked?.Invoke();
            });

            cancelButton.onClick.AddListener(() =>
            {
                Close();
                OnCancelClicked?.Invoke();
            });

            rewardButton.onClick.AddListener(() =>
            {
                Close();
            });

            backGame.onClick.AddListener(() =>
            {
                Close();
            });
        }

        public void Init() 
        { 

        }
        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            OnBackClicked?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
