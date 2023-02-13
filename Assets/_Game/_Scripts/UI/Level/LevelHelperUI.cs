using LogicPlatformer.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Localization;
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

        private StringTableCollection hintsTable;
        public List<HintUI> hints;
        private LevelData levelData;
        private int levelHintsCount = 0;

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
        public void Init(StringTableCollection hintsTable, LevelData levelData)
        {
            this.hintsTable = hintsTable;
            this.levelData = levelData;
            
        }
        public void UpateData()
        {
            Debug.Log("Opened hints: " + levelData.levelsHintData[levelData.currentlevel - 1]);
            CheckData();

            if (hints != null)
            {
                ClearHints();
            }
            hints = new List<HintUI>();
        }

        private void CheckData()
        {
            int level;
            int currentLevel = 0;
            int hintsCount = 0;

            foreach (var hint in hintsTable.GetRowEnumerator()) 
            {
                if (hint.KeyEntry.Key == "0")
                {
                    continue;
                }
                if (!Int32.TryParse(hint.KeyEntry.Key.Substring(0, hint.KeyEntry.Key.IndexOf('.')), out level))
                {
                    level = 0;
                }

                if (level == levelData.currentlevel)
                {
                    currentLevel = level;
                    Int32.TryParse(hint.KeyEntry.Key.Split(new char[] { '.' }, 2)[1], out hintsCount);

                    if (hintsCount > levelHintsCount) 
                    {
                        levelHintsCount = hintsCount;
                    }
                }
            }
            Debug.Log("currentLevel: " + currentLevel + ", levelHintsCount: " + levelHintsCount);
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

            if (levelHintsCount != 0 && levelData.levelsHintData.Count != 0)
            {
                for (int i = 0; i < levelData.levelsHintData[levelData.currentlevel -1]; i++)
                {
                    HintUI hint = Instantiate(hintPrefab, hintsContent);
                    string hintKey = levelData.currentlevel + "." + (i+1).ToString();
                    hint.SetHint(hintsTable, hintKey);
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
            if (levelData.levelsHintData[levelData.levelsHintData.Count - 1] <= levelHintsCount && levelHintsCount != 0)
            {
                HintUI hint = Instantiate(hintPrefab, hintsContent);
                string hintKey = levelData.currentlevel + "." + levelData.levelsHintData[levelData.currentlevel - 1].ToString();
                hint.SetHint(hintsTable, hintKey);
                hints.Add(hint);
            }
            else if (levelHintsCount == 0)
            {
                HintUI hint = Instantiate(hintPrefab, hintsContent);
                
                //Set default hint
                string hintDefaultKey = "0";
                hint.SetHint(hintsTable, hintDefaultKey);
                hints.Add(hint);
            }
        }

        private void CheckHintsCount()
        {
            if (levelData.levelsHintData[levelData.currentlevel - 1] >= levelHintsCount)
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
