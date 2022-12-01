using JetBrains.Annotations;
using LogicPlatformer.Level;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public enum Quest
    {
        None,
        ExitDoor,
        Handle
    }
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private ExitDoor exitDoor;
        [SerializeField] private Exit exit;
        [SerializeField] private Handle handle;
 
        public event Action OnShowSelect;
        public event Action OnHideSelect;
        public event Action OnExitLevel;

        private Quest quest = Quest.None;

        private void Awake()
        {
            exit.OnExit += () =>
            {
                OnExitLevel?.Invoke();
            };

            exitDoor.OnDoorEnter += () =>
            {
                quest = Quest.ExitDoor;
                OnShowSelect?.Invoke();
            };

            exitDoor.OnDoorExit += () =>
            {
                OnHideSelect?.Invoke();
            };

            handle.OnHadleEnter += () =>
            {
                quest = Quest.Handle;
                OnShowSelect?.Invoke();
            };

            handle.OnHadleExit += () =>
            {
                OnHideSelect?.Invoke();
            };

            handle.OnHandleUsed += () =>
            {
                handle.StartTargetAnimation();
            };
        }

        public void Select()
        {
            switch(quest)
            {
                case Quest.ExitDoor:
                    OnExitLevel?.Invoke();
                    break;
                case Quest.Handle:
                    handle.UseHandle();
                    break;
                default:
                    Debug.Log("Ошибка значения Quest enum!");
                    break;
            }
        }
    }
}
