using System;
using UnityEngine;

namespace LogicPlatformer.UI
{
    public abstract class IStartMainUI : MonoBehaviour
    {
        public abstract event Action OnStart;

        public abstract event Action OnLevelRoom;

        public abstract void Init();


    }
}
