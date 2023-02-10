using LogicPlatformer.Player;
using System;
using UnityEngine;

namespace LogicPlatformer
{
    public class SelectableHandle : ISelectableItem
    {
        [SerializeField] private Transform handleContour;
        [SerializeField] private SpriteRenderer lightSprite;
        [SerializeField] private Color idleColor;
        [SerializeField] private Color activeColor;
        [SerializeField] private Animation handleAnimation;

        [SerializeField] private IActivate targetActivate;

        private event Action OnHandleUsed;

        public override event Action<int> OnSelectableEnter;
        public override event Action OnSelectableExit;
        private void Start()
        {
            lightSprite.color = idleColor;
            handleContour.gameObject.SetActive(false);

            OnHandleUsed += () =>
            {
                lightSprite.color = activeColor;
            };
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                handleContour.gameObject.SetActive(true);
                OnSelectableEnter?.Invoke(Index);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                handleContour.gameObject.SetActive(false);
                OnSelectableExit?.Invoke();
            }
        }

        private void UseHandle()
        {
            handleContour.gameObject.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            handleAnimation.Play();
        }

        public void SwitchLight() //Animation Event
        {
            OnHandleUsed?.Invoke();
        }

        public override void UseSelectable()
        {
            targetActivate.Activate();
            UseHandle();
        }
    }
}
