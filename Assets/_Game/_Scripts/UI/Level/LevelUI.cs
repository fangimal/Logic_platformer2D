using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer.UI
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private Button selectButton;
        [SerializeField] private TextMeshProUGUI LevelNumber;

        [Space(5)]
        [Header("Control Buttons")]

        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;
        [SerializeField] private Button spaceButton;

        public event Action OnClickSelectButton;

        void Start()
        {
            selectButton.gameObject.SetActive(false);

            selectButton.onClick.AddListener(()=> 
            {
                OnClickSelectButton?.Invoke();
            });
        }
        public void Open(int levelIndex)
        {
            LevelNumber.text = "Level " + levelIndex.ToString();
            gameObject.SetActive(true);
        }

        public void ShowExitButton(bool show)
        {
            selectButton.gameObject.SetActive(show);
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
