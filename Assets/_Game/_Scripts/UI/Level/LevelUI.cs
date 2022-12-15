using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer.UI
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI LevelNumber;

        [Space(5), Header("Game Group")]

        [SerializeField] private Button pauseButton;
        [SerializeField] private Button helpButton;

        [Space(5), Header("Help Group")]

        [SerializeField] private Transform helpGroup;
        [SerializeField] private Button okButton;
        [SerializeField] private Button cancelButton;

        [Space(5), Header("Pause Group")]

        [SerializeField] private Transform pauseGroup;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button playButton;

        [Space(5), Header("Control Buttons")]

        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;
        [SerializeField] private Button jumpButton;
        [SerializeField] private Button selectButton;
        [SerializeField] private InputKeys inputKeys;

        private LevelData levelData;

        public event Action OnClickSelectButton;
        public event Action<int> OnRestartClicked;

        void Start()
        {
            pauseGroup.gameObject.SetActive(false);
            helpGroup.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(false);

            selectButton.onClick.AddListener(()=> 
            {
                OnClickSelectButton?.Invoke();
            });

            restartButton.onClick.AddListener(() => 
            {
                pauseGroup.gameObject?.SetActive(false);
                Time.timeScale = 1;
                OnRestartClicked?.Invoke(levelData.currentlevel);
            });

            pauseButton.onClick.AddListener(() =>
            {
                Time.timeScale = 0;
                pauseGroup.gameObject?.SetActive(true);
            });

            playButton.onClick.AddListener(() =>
            {
                pauseGroup.gameObject?.SetActive(false);
                Time.timeScale = 1;
            });

            helpButton.onClick.AddListener(() => 
            {
                helpGroup.gameObject.SetActive(true);
                Time.timeScale = 0;
            });

            okButton.onClick.AddListener(() => 
            {
                Debug.Log("TODO"); //TODO
                helpGroup.gameObject.SetActive(false);
                Time.timeScale = 1;
            });

            cancelButton.onClick.AddListener(() =>
            {
                Debug.Log("TODO"); //TODO
                helpGroup.gameObject.SetActive(false);
                Time.timeScale = 1;
            });

        }

        public void Init(PlayerController playerController)
        {
            inputKeys.Init(playerController);
        }
        public void Open(LevelData levelData)
        {
            this.levelData = levelData;
            LevelNumber.text = "Level " + levelData.currentlevel.ToString();
            gameObject.SetActive(true);
        }
        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void ShowExitButton(bool show)
        {
            selectButton.gameObject.SetActive(show);
        }
    }
}
