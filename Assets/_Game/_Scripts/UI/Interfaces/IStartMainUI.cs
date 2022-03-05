using System;
using UnityEngine;

namespace LogicPlatformer.UI
{
    public abstract class IStartMainUI : MonoBehaviour
    {
        public abstract event Action OnStartButton;

        public abstract void Init();


    }
}
