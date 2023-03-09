using LogicPlatformer.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LogicPlatformer
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private Button openCloseButton;
        [SerializeField] private Transform content;
        [SerializeField] private Button saveButton;
        [SerializeField] private Button loadButton;
        [SerializeField] private Button clearButton;
        [SerializeField] private Button addArrayButton;
        [SerializeField] private Button addLevelButton;

        [SerializeField] private TextMeshProUGUI soundText;
        [SerializeField] private TextMeshProUGUI arrayLenghtText;
        [SerializeField] private TextMeshProUGUI startLevelText;

        public DataManager DM;

        private bool isOpen = true;

        private void Start()
        {
            openCloseButton.onClick.AddListener(() => 
            {
                content.gameObject.SetActive(!isOpen);
                isOpen = !isOpen;
            });

            SetValue();

            saveButton.onClick.AddListener(() =>
            {
                DM.SaveData();
            });

            loadButton.onClick.AddListener(() =>
            {
                DM.LoadYandexData();
                SetValue();
            });

            clearButton.onClick.AddListener(() =>
            {
                DM.DG = new DataGroup();
                SetValue();
            });

            addArrayButton.onClick.AddListener(() =>
            {
                DM.DG.levelData.levelsHintData.Add(0);
                SetValue();
            });

            addLevelButton.onClick.AddListener(() =>
            {
                DM.DG.levelData.lastOpenLevel++;
                SetValue();
            });
        }

        private void SetValue()
        {
            soundText.text = DM.DG.settingsData.musicIsOn.ToString();
            arrayLenghtText.text = DM.DG.levelData.levelsHintData.Count.ToString();
            startLevelText.text = DM.DG.levelData.lastOpenLevel.ToString();
        }
    }
}
