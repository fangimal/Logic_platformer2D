using LogicPlatformer.UI;
using UnityEngine;

namespace LogicPlatformer
{
    public class Tutorial : MonoBehaviour
    {
        public IActivate platform;
        public GameObject box;
        public GameObject animateTutor;

        private LevelUI levelUI;
        private LevelData levelData;
        private QuestManager questManager;
        public int questIndex = 0;

        public bool firstGeneration = false;
        public bool GetGeneration => firstGeneration;

        private void Start()
        {
            DestroyClone();
        }

        private void DestroyClone()
        {
            Tutorial[] clones = FindObjectsOfType<Tutorial>();

            if (clones.Length > 1)
            {
                int generalIndex = 0;
                int cloneIndex = 0;
                for (int i = 0; i < clones.Length; i++)
                {
                    if (clones[i].GetGeneration)
                    {
                        generalIndex = i;
                    }
                    else
                    {
                        cloneIndex = i;
                    }
                }
                clones[generalIndex].platform = clones[cloneIndex].platform;
                clones[generalIndex].box = clones[cloneIndex].box;
                clones[generalIndex].animateTutor = clones[cloneIndex].animateTutor;

                Destroy(gameObject);

                return;
            }

            SetFirstGeneration();
        }

        private void SetFirstGeneration()
        {
            levelUI = FindObjectOfType<LevelUI>();

            levelUI.GetHelpAnimation.wrapMode = WrapMode.Loop;
            levelUI.GetHelpAnimation.playAutomatically = true;
            levelUI.GetHelpAnimation.Play();

            levelUI.OnBackLevelRoomClicked += EndTutorial;

            questManager = FindObjectOfType<QuestManager>();
            questManager.OnExitLevel += EndTutorial;

            transform.parent = levelUI.transform;

            levelData = levelUI.GetLevelData;

            levelData.levelsHintData[levelData.currentlevel - 1] = 1;

            firstGeneration = true;

            levelUI.OnHelpClicked += FirstQuest;

            levelUI.OnRestartClicked += SecondQuest;

            levelUI.GetLevelHelper.AcivateNextLevelBtn(false);

        }

        private void FirstQuest()
        {
            levelUI.OnHelpClicked -= FirstQuest;

            levelUI.GetHelpAnimation.playAutomatically = false;
            levelUI.GetHelpAnimation.wrapMode = WrapMode.Once;

            levelUI.GetPauseAnimation.playAutomatically = true;
            levelUI.GetPauseAnimation.Play();

            questIndex = 1;

            Invoke("CheckQuestCompleted", 2f);
        }

        private void SecondQuest(int level)
        {
            if (questIndex == 1)
            {
                questIndex = 2;
            }

            Invoke("CheckQuestCompleted", 2f);
        }

        private void CheckQuestCompleted()
        {
            if (questIndex == 1)
            {
                platform.Activate();

                animateTutor.gameObject.SetActive(true);

                levelUI.GetHelpAnimation.Play();

            }
            else if (questIndex == 2)
            {
                levelUI.GetPauseAnimation.playAutomatically = false;
                levelUI.GetPauseAnimation.Stop();

                platform.Activate();
                Invoke("ActivateBox", 2f);

            }
        }

        private void ActivateBox()
        {
            box.gameObject.SetActive(true);
        }

        private void EndTutorial()
        {
            questManager.OnExitLevel -= EndTutorial;
            levelUI.OnRestartClicked -= SecondQuest;
            levelUI.OnBackLevelRoomClicked -= EndTutorial;
            levelUI.OnHelpClicked -= FirstQuest;

            levelUI.GetHelpAnimation.playAutomatically = false;
            levelUI.GetHelpAnimation.wrapMode = WrapMode.Once;

            levelUI.GetPauseAnimation.playAutomatically = false;
            levelUI.GetPauseAnimation.Stop();

            levelUI.GetLevelHelper.AcivateNextLevelBtn(true);
            Destroy(gameObject);
        }
    }
}
