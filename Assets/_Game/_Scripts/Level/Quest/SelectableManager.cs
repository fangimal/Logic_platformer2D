using System;
using UnityEngine;

namespace LogicPlatformer
{
    public class SelectableManager : MonoBehaviour
    {
        [SerializeField] private ISelectableItem[] selectableItems;

        private int currentIndex;

        public event Action OnHandleEnter;

        public event Action OnHandleExit;

        public event Action OnHandleUsed;

        private void Awake()
        {
            InitSelectable();
        }

        private void InitSelectable()
        {
            for (int i = 0; i < selectableItems.Length; i++)
            {
                int index = i;

                selectableItems[index].Index = index;
                selectableItems[index].OnSelectableEnter += (int indx) =>
                {
                    OnHandleEnter?.Invoke();
                    currentIndex = indx;
                };

                selectableItems[index].OnSelectableExit += () => { OnHandleExit?.Invoke(); };

            }
        }
        public void UseSelectable()
        {
            selectableItems[currentIndex].UseSelectable();
        }
    }
}
