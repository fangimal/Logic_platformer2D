using LogicPlatformer.Level;
using System;
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
        [SerializeField] private HandleManager handleManager;

        public event Action OnShowSelect;
        public event Action OnHideSelect;
        public event Action OnExitLevel;

        public Quest quest = Quest.None;

        private void Awake()
        {
            exit.OnExit += () =>
            {
                OnExitLevel?.Invoke();
            };

            if (exitDoor)
            {
                exitDoor.OnDoorEnter += () =>
                {
                    quest = Quest.ExitDoor;
                    OnShowSelect?.Invoke();
                };

                exitDoor.OnDoorExit += () =>
                {
                    OnHideSelect?.Invoke();
                };
            }

            if (handleManager != null)
            {
                handleManager.OnHandleEnter += () =>
                {
                    quest = Quest.Handle;
                    OnShowSelect?.Invoke();
                };

                handleManager.OnHandleExit += () =>
                {
                    OnHideSelect?.Invoke();
                    quest = Quest.None;
                };
            }
        }

        public void Select()
        {
            switch (quest)
            {
                case Quest.ExitDoor:
                    OnExitLevel?.Invoke();
                    break;
                case Quest.Handle:
                    handleManager.UseHandle();
                    break;
                default:
                    Debug.LogWarning("Ошибка значения Quest enum!");
                    break;
            }
        }
    }
}
