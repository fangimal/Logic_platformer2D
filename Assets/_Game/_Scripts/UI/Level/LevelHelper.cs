using System;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer
{
    public class LevelHelper : MonoBehaviour
    {
        [SerializeField] private Button okButton;
        [SerializeField] private Button cancelButton;

        public event Action OnNeedHelpClicked;
        public event Action OnCancelClicked;

        private void Awake()
        {
            okButton.onClick.AddListener (() => 
            {
                OnNeedHelpClicked?.Invoke();
            });

            cancelButton.onClick.AddListener(() =>
            {
                Close();
                OnCancelClicked?.Invoke();
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
            gameObject.SetActive(false);
        }
    }
}
