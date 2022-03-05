using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer.UI
{
    public class SimpleMainUI : IMainUI
    {
        [SerializeField] private Transform content;

        public override void Clear()
        {
            if (startMainUI)
            {
                Destroy(startMainUI.gameObject);
            }

            if (levelMainUI)
            {
                Destroy(levelMainUI.gameObject);
            }
        }

        public override void InitLevelsUI()
        {
            Debug.Log("InitLevelsUI");

            if (levelMainUI)
            {
                return;
            }

            levelMainUI = Instantiate(Resources.Load<LevelMainUI>("UI/Level/_LevelUI"), content);
            levelMainUI.transform.SetAsFirstSibling();
            levelMainUI.Init();

            GetLevelUIActions.Init(levelMainUI);



        }
        private IStartActions startActions;

        private ILevelUIActions levelActions;

        public override IStartActions GetStartActions
        {
            get
            {
                if (startActions == null)
                {
                    startActions = new SimpleStartActions();
                }

                return startActions;
            }
        }

        public override ILevelUIActions GetLevelUIActions
        {
            get
            {
                if (levelActions == null)
                {
                    levelActions = new SimpleLevelUIActions();
                }

                return levelActions;
            }
        }
        public override void InitStartsUI()
        {
            if (startMainUI)
            {
                return;
            }

            startMainUI = Instantiate(Resources.Load<StartMainUI>("UI/Start/_StartUI"), content);
            startMainUI.transform.SetAsFirstSibling();
            startMainUI.Init();

            GetStartActions.Init(startMainUI);
        }

        public class SimpleStartActions : IStartActions
        {
            public override event Action OnOpenedStart;
            public override event Action OnStarted;

            public override void Init(IStartMainUI startMainUI)
            {
                OnOpenedStart?.Invoke();

                startMainUI.OnStartButton += () =>
                {
                    OnStarted?.Invoke();
                };
            }
        }

        public class SimpleLevelUIActions : ILevelUIActions
        {
            public override event Action OnOpenLevel;
            public override event Action OnEndLevel;

            public override void Init(ILevelMainUI levelMainUI)
            {
                Debug.Log("Init: SimpleLevelActions");

                levelMainUI.OnClickExitButton += () => 
                {
                    OnEndLevel?.Invoke();
                };
            }
        }
    }
}
