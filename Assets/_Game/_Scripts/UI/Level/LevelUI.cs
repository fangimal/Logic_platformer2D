using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer.UI
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI LevelNumber;
        [SerializeField] private Animation anim;
        [SerializeField] private Image image;

        [Space(5), Header("Game Group")]

        [SerializeField] private Button pauseButton;

        [Space(5), Header("Level Helper")]

        [SerializeField] private Button helpButton;
        [SerializeField] private LevelHelper levelHelper;

        [Space(5), Header("Pause Group")]

        [SerializeField] private Transform pauseGroup;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button playButton;
        [SerializeField] private Button backLevelRoomButton;

        [Space(5), Header("Control Buttons")]

        [SerializeField] private Button leftButton;
        [SerializeField] private Button rightButton;
        [SerializeField] private Button jumpButton;
        [SerializeField] private Button selectButton;
        [SerializeField] private InputKeys inputKeys;

        private LevelData levelData;
        private Color startColor;

        public event Action OnClickSelectButton;
        public event Action<int> OnRestartClicked;
        public event Action OnBackLevelRoomClicked;

        void Start()
        {
            pauseGroup.gameObject.SetActive(false);
            levelHelper.Close();
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
                levelHelper.Open();
                helpButton.gameObject.SetActive(false);
                Time.timeScale = 0;
            });

            backLevelRoomButton.onClick.AddListener(() =>
            {
                pauseGroup.gameObject.SetActive(false);
                Time.timeScale = 1;
                Close();
                OnBackLevelRoomClicked?.Invoke();
            });

            levelHelper.OnNeedHelpClicked += () => 
            { 

            };

            levelHelper.OnCancelClicked += () => 
            {
                Time.timeScale = 1;
            };

            levelHelper.OnBackClicked += () => 
            {
                Time.timeScale = 1;
                helpButton.gameObject.SetActive(true);
            };
        }

        public void Init(LevelData levelData)
        {
            this.levelData = levelData;
            levelHelper.Init();
        }
        public void Open(PlayerController playerController)
        {
            image.color = startColor;
            inputKeys.Init(playerController);
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

        public void Fail()
        {
            anim.Play();
        }
    }
}
