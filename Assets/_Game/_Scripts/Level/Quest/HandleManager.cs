using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace LogicPlatformer
{
    public class HandleManager : MonoBehaviour
    {
        [SerializeField] private ActivationHandle[] handles;

        public int currentIndex;

        public event Action OnHandleEnter;

        public event Action OnHandleExit;

        public event Action OnHandleUsed;

        private void Awake()
        {
            InitHandle();
        }

        private void InitHandle()
        {
            for (int i = 0; i < handles.Length; i++)
            {
                int index = i;

                handles[index].Index= index;
                handles[index].OnHadleEnter += (int indx) => 
                {
                    OnHandleEnter?.Invoke();
                    currentIndex = indx;
                };

                handles[index].OnHadleExit += ()=> { OnHandleExit?.Invoke(); };

            }
        }
        public void UseHandle()
        {
            handles[currentIndex].StartTargetAnimation();
        }
    }
}
