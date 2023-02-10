using LogicPlatformer.Level;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer
{
    public class LevelHelperUI : MonoBehaviour
    {
        [SerializeField] private Transform onePage;
        [SerializeField] private Transform twoPage;
        [SerializeField] private Transform hintContent;
        [SerializeField] private HintUI hintPrefab;
        [SerializeField] private Transform hintsContent;

        [SerializeField] private Button backGame;
        [SerializeField] private Button okButton;
        [SerializeField] private Button cancelButton;
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private Button takeHintButton;
        [SerializeField] private LevelHellper defaultLevelHellper;

        private LevelHellper[] levelHellpers;
        public List<HintUI> hints;
        private LevelData levelData;

        public event Action OnCancelClicked;
        public event Action OnBackClicked;
        public event Action OnExitLevel;
        public event Action OnTakeHint;

        private void Awake()
        {
            okButton.onClick.AddListener(() =>
            {
                onePage.gameObject.SetActive(false);
                OnTakeHint?.Invoke();
                TakeHint();
                CheckHintsCount();
                OpenPage();
            });

            cancelButton.onClick.AddListener(() =>
            {
                Close();
                OnCancelClicked?.Invoke();
            });

            nextLevelButton.onClick.AddListener(() =>
            {
                ClearHints();
                Close();
                OnExitLevel?.Invoke();
            });

            backGame.onClick.AddListener(() =>
            {
                Close();
            });

            takeHintButton.onClick.AddListener(() =>
            {
                OnTakeHint?.Invoke();
                TakeHint();
                CheckHintsCount();
            });

        }

        public void Init(LevelHellper[] levelHellpers, LevelData levelData)
        {
            this.levelHellpers = levelHellpers;
            this.levelData = levelData;

            if (hints != null)
            {
                ClearHints();
            }
            hints = new List<HintUI>();
        }
        public void Open()
        {
            gameObject.SetActive(true);
            OpenPage();
        }

        public void Close()
        {
            OnBackClicked?.Invoke();
            gameObject.SetActive(false);
        }

        private void LoadOpenHints()
        {
            ClearHints();

            if (levelHellpers.Length != 0 && levelData.levelsHintData.Count != 0)
            {
                for (int i = 0; i < levelData.levelsHintData[levelData.currentlevel -1]; i++)
                {
                    HintUI hint = Instantiate(hintPrefab, hintsContent);
                    hint.SetHint(levelHellpers[i].Hint);
                    hints.Add(hint);
                }
            }
            else
            {
                TakeHint();
            }
        }

        private void TakeHint()
        {
            if (levelData.levelsHintData[levelData.levelsHintData.Count - 1] <= levelHellpers.Length && levelHellpers.Length != 0)
            {
                HintUI hint = Instantiate(hintPrefab, hintsContent);
                hint.SetHint(levelHellpers[hints.Count].Hint);
                hints.Add(hint);
            }
            else if (levelHellpers.Length == 0)
            {
                HintUI hint = Instantiate(hintPrefab, hintsContent);
                hint.SetHint(defaultLevelHellper.Hint);
                hints.Add(hint);
            }
        }

        private void CheckHintsCount()
        {
            if (levelData.levelsHintData[levelData.currentlevel - 1] >= levelHellpers.Length)
            {
                takeHintButton.gameObject.SetActive(false);
            }
            else
            {
                takeHintButton.gameObject.SetActive(true);
            }
        }

        private void ClearHints()
        {
            if (hints.Count > 0)
            {
                for (int i = 0; i < hints.Count; i++)
                {
                    Destroy(hints[i].gameObject);
                }
                hints.Clear();
            }
        }

        private void OpenPage()
        {
            if (levelData.levelsHintData[levelData.currentlevel - 1] == 0)
            {
                onePage.gameObject.SetActive(true);
                twoPage.gameObject.SetActive(false);
            }
            else
            {
                onePage.gameObject.SetActive(false);
                twoPage.gameObject.SetActive(true);
                CheckHintsCount();
                LoadOpenHints();
            }
        }
    }
}
