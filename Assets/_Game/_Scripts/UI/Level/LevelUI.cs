using LogicPlatformer.Level;
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
        [SerializeField] private LevelHelperUI levelHelper;

        [Space(5), Header("Pause Group")]

        [SerializeField] private Transform pauseGroup;
        [SerializeField] private Button settingsButton;
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
        public event Action OnRewardedNextLevelClicked;
        public event Action OnTakeHint;
        public event Action OnSettingsClicked;
        public event Action OnButtonClicked;

        void Start()
        {
            pauseGroup.gameObject.SetActive(false);
            levelHelper.Close();
            selectButton.gameObject.SetActive(false);

            selectButton.onClick.AddListener(() =>
            {
                OnButtonClicked?.Invoke();
                OnClickSelectButton?.Invoke();
            });

            restartButton.onClick.AddListener(() =>
            {
                OnButtonClicked?.Invoke();
                pauseGroup.gameObject?.SetActive(false);
                Time.timeScale = 1;
                OnRestartClicked?.Invoke(levelData.currentlevel);
            });

            pauseButton.onClick.AddListener(() =>
            {
                OnButtonClicked?.Invoke();
                Time.timeScale = 0;
                pauseGroup.gameObject?.SetActive(true);
            });

            playButton.onClick.AddListener(() =>
            {
                OnButtonClicked?.Invoke();
                pauseGroup.gameObject?.SetActive(false);
                Time.timeScale = 1;
            });

            helpButton.onClick.AddListener(() =>
            {
                OnButtonClicked?.Invoke();
                levelHelper.Open();
                helpButton.gameObject.SetActive(false);
                Time.timeScale = 0;
            });

            backLevelRoomButton.onClick.AddListener(() =>
            {
                OnButtonClicked?.Invoke();
                pauseGroup.gameObject.SetActive(false);
                Time.timeScale = 1;
                Close();
                OnBackLevelRoomClicked?.Invoke();
            });

            levelHelper.OnCancelClicked += () =>
            {
                OnButtonClicked?.Invoke();
                Time.timeScale = 1;
            };

            levelHelper.OnBackClicked += () =>
            {
                OnButtonClicked?.Invoke();
                Time.timeScale = 1;
                helpButton.gameObject.SetActive(true);
            };

            levelHelper.OnExitLevel += () =>
            {
                OnButtonClicked?.Invoke();
                Time.timeScale = 1;
                OnRewardedNextLevelClicked?.Invoke();
            };

            levelHelper.OnTakeHint += () =>
            {
                OnButtonClicked?.Invoke();
                OnTakeHint?.Invoke();
            };

            settingsButton.onClick.AddListener(() =>
            {
                pauseGroup.gameObject?.SetActive(false);
                OnSettingsClicked?.Invoke();
            });
        }

        public void Init(LevelData levelData)
        {
            this.levelData = levelData;

        }
        public void SetHints(LevelManager levelManager)
        {
            levelHelper.Init(levelManager.GetLevelHelpers, levelData);
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
