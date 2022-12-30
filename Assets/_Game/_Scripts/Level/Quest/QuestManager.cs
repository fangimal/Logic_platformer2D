using LogicPlatformer.Level;
using System;
using UnityEngine;

namespace LogicPlatformer
{
    public enum Quest
    {
        None,
        ExitDoor,
        Selectable
    }
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private ExitDoor exitDoor;
        [SerializeField] private Exit exit;
        [SerializeField] private SelectableManager selectableManager;

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

            if (selectableManager != null)
            {
                selectableManager.OnHandleEnter += () =>
                {
                    quest = Quest.Selectable;
                    OnShowSelect?.Invoke();
                };

                selectableManager.OnHandleExit += () =>
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
                case Quest.Selectable:
                    selectableManager.UseSelectable();
                    break;
                default:
                    //Debug.LogWarning("Ошибка значения Quest enum!");
                    break;
            }
        }
    }
}
