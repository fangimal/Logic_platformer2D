using System;
using UnityEngine;

namespace LogicPlatformer
{
    public abstract class ISelectableItem : MonoBehaviour
    {
        [HideInInspector] public int Index;

        public abstract event Action<int> OnSelectableEnter;
        public abstract event Action OnSelectableExit;
        public abstract void UseSelectable();

    }
}
