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

            if (levelRoomMainUI)
            {
                Destroy(levelRoomMainUI.gameObject);
            }

            Resources.UnloadUnusedAssets();
        }


        private IStartActions startActions;

        private ILevelRoomUIActions levelRoomUIActions;

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

        public override ILevelRoomUIActions GetLevelRoomUIActions
        {
            get 
            {
                if (levelRoomUIActions == null)
                {
                    levelRoomUIActions = new SimpleLevelRoomUIActions();
                }

                return levelRoomUIActions;
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
        public override void InitLevelsUI()
        {
            if (levelMainUI)
            {
                return;
            }

            levelMainUI = Instantiate(Resources.Load<LevelMainUI>("UI/Level/_LevelUI"), content);
            levelMainUI.transform.SetAsFirstSibling();
            levelMainUI.Init();

            GetLevelUIActions.Init(levelMainUI);
        }
        public override void InitLevelRoomUI()
        {
            if (levelRoomMainUI)
            {
                return;
            }

            levelRoomMainUI = Instantiate(Resources.Load<LevelRoomMainUI>("UI/Level/_LevelsRoom"), content);
            levelRoomMainUI.transform.SetAsFirstSibling();
            levelRoomMainUI.Init();

            GetLevelRoomUIActions.Init(levelRoomMainUI);
        }

        public class SimpleStartActions : IStartActions
        {
            public override event Action OnOpenedStart;
            public override event Action OnStarted;
            public override event Action OnLevelRoomOpened;

            public override void Init(IStartMainUI startMainUI)
            {
                OnOpenedStart?.Invoke();

                startMainUI.OnStart += () =>
                {
                    OnStarted?.Invoke();
                };

                startMainUI.OnLevelRoom += () => 
                {
                    OnLevelRoomOpened?.Invoke();
                };
            }
        }

        public class SimpleLevelRoomUIActions : ILevelRoomUIActions
        {

            public override event Action OnBack;
            public override void Init(ILevelRoomMainUI levelRoomUI)
            {
                levelRoomUI.OnBackClick += () => 
                {
                    OnBack?.Invoke();
                };
            }
        }

        public class SimpleLevelUIActions : ILevelUIActions
        {
            public override event Action OnOpenLevel;
            public override event Action OnEndLevel;

            public override void Init(ILevelMainUI levelMainUI)
            {
                levelMainUI.OnClickExitButton += () => 
                {
                    OnEndLevel?.Invoke();
                };
            }
        }
    }
}
