using System;
using UnityEngine;

namespace LogicPlatformer.UI
{
    public abstract class IMainUI : MonoBehaviour
    {
        protected IStartMainUI startMainUI;

        protected ILevelMainUI levelMainUI;

        public IStartMainUI GetStartMainUI => startMainUI;

        public ILevelMainUI GetLevelMainUI => levelMainUI;

        public abstract IStartActions GetStartActions { get; }

        public abstract ILevelUIActions GetLevelUIActions { get; }

        public abstract void Clear();

        public abstract void InitStartsUI();

        public abstract void InitLevelsUI();

        public abstract class IStartActions
        {
            public abstract event Action OnOpenedStart;

            public abstract event Action OnStarted;

            public abstract void Init(IStartMainUI startMainUI);
        }

        public abstract class ILevelUIActions
        {
            public abstract event Action OnOpenLevel;

            public abstract event Action OnEndLevel;

            public abstract void Init(ILevelMainUI levelMainUI);
        }
    }
}
