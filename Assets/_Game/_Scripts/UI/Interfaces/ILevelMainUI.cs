using System;
using UnityEngine;

namespace LogicPlatformer.UI
{
    public abstract class ILevelMainUI : MonoBehaviour
    {
        public abstract event Action OnClickExitButton;
        public abstract void Init();

        public abstract void ShowExitButton(bool show);

    }
}
