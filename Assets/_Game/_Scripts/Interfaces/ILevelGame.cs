using System;
using UnityEngine;

namespace LogicPlatformer.Level
{
    public abstract class ILevelGame : MonoBehaviour
    {
        public abstract event Action OnShowLevelDoor;

        public abstract event Action OnHideLevelDoor;

        public abstract event Action OnStartLevel;
    }
}
