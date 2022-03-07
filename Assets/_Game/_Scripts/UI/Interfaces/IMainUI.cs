using System;
using UnityEngine;

namespace LogicPlatformer.UI
{
    public abstract class IMainUI : MonoBehaviour
    {
        protected IStartMainUI startMainUI;

        protected ILevelRoomMainUI levelRoomMainUI;

        protected ILevelMainUI levelMainUI;

        public IStartMainUI GetStartMainUI => startMainUI;

        public ILevelRoomMainUI GetLevelRoomUI => levelRoomMainUI;

        public ILevelMainUI GetLevelMainUI => levelMainUI;

        public abstract IStartActions GetStartActions { get; }

        public abstract ILevelRoomUIActions GetLevelRoomUIActions { get; }

        public abstract ILevelUIActions GetLevelUIActions { get; }

        public abstract void Clear();

        public abstract void InitStartsUI();

        public abstract void InitLevelRoomUI();

        public abstract void InitLevelsUI();

        public abstract class IStartActions
        {
            public abstract event Action OnOpenedStart;

            public abstract event Action OnStarted;

            public abstract event Action OnLevelRoomOpened;

            public abstract void Init(IStartMainUI startMainUI);
        }

        public abstract class ILevelRoomUIActions
        {
            public abstract event Action OnBack;
            public abstract void Init(ILevelRoomMainUI levelRoomUI);
        }

        public abstract class ILevelUIActions
        {
            public abstract event Action OnOpenLevel;

            public abstract event Action OnEndLevel;

            public abstract void Init(ILevelMainUI levelMainUI);
        }
    }
}
