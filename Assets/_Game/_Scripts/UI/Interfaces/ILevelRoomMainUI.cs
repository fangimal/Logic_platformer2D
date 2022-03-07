using System;
using UnityEngine;

namespace LogicPlatformer.UI
{
    public abstract class ILevelRoomMainUI : MonoBehaviour
    {
        public abstract event Action OnBackClick;
        public abstract void Init();
    }
}
