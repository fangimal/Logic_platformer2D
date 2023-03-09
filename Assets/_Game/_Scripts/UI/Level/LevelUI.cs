using LogicPlatformer.Level;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace LogicPlatformer.UI
{
    public class LevelUI : MonoBehaviour
    {
        [Header("Level info")]
        [SerializeField] private TextMeshProUGUI LevelNumber;
        [SerializeField] private LocalizedString localizeStringLvlNumber;

        [Space(5)]
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

        [Space(5), Header("Tutorial Button Animations")]
        [SerializeField] private Animation helpAnimation;
        [SerializeField] private Animation pauseAnimation;

        private LevelData levelData;
        private Color startColor;

        public LevelHelperUI GetLevelHelper => levelHelper;
        public LevelData GetLevelData => levelData;
        public Animation GetHelpAnimation=> helpAnimation;
        public Animation GetPauseAnimation => pauseAnimation;

        public event Action OnClickSelectButton;
        public event Action<int> OnRestartClicked;
        public event Action OnBackLevelRoomClicked;
        public event Action OnRewardedNextLevelClicked;
        public event Action OnTakeHint;
        public event Action OnSettingsClicked;
        public event Action OnHelpClicked;
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
                OnHelpClicked?.Invoke();
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
            levelHelper.Init(levelData);

        }
        public void SetHints()
        {
            levelHelper.UpateData();
        }
        public void Open(PlayerController playerController)
        {
            image.color = startColor;
            inputKeys.Init(playerController);

            //Localization
            localizeStringLvlNumber.Arguments = new object[] { levelData.currentlevel };
            localizeStringLvlNumber.Arguments[0] = levelData.currentlevel;
            localizeStringLvlNumber.RefreshString();
            localizeStringLvlNumber.StringChanged += UpdateLevelNumber;

            gameObject.SetActive(true);
        }

        private void UpdateLevelNumber(string value)
        {
            LevelNumber.text = value;
        }

        public void Close()
        {
            localizeStringLvlNumber.StringChanged-= UpdateLevelNumber;
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
