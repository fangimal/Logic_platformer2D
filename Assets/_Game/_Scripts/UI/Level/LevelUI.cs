using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer.UI
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private Button exitButton;
        [SerializeField] private TextMeshProUGUI LevelNumber;

        public event Action OnClickExitButton;

        void Start()
        {
            exitButton.gameObject.SetActive(false);

            exitButton.onClick.AddListener(()=> 
            {
                OnClickExitButton?.Invoke();
            });
        }
        public void Open(int levelIndex)
        {
            LevelNumber.text = levelIndex.ToString();
            gameObject.SetActive(true);
        }

        public void ShowExitButton(bool show)
        {
            exitButton.gameObject.SetActive(show);
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
