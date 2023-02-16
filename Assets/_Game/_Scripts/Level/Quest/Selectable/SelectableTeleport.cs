using LogicPlatformer.Player;
using System;
using UnityEngine;

namespace LogicPlatformer
{
    public class SelectableTeleport : ISelectableItem
    {
        [SerializeField] private Transform endPoint;
        [SerializeField] private Transform detected;
        [SerializeField] private Transform lightTransform;
        [SerializeField] private Color lightIdleColor;
        [SerializeField] private Color lightEnterColor;

        private PlayerManager player;
        private SpriteRenderer lightColor;

        public override event Action<int> OnSelectableEnter;
        public override event Action OnSelectableExit;

        private void Start()
        {
            lightColor = lightTransform.gameObject.GetComponent<SpriteRenderer>();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                player = collision.GetComponent<PlayerManager>();
                lightColor.color = lightEnterColor;
                OnSelectableEnter?.Invoke(Index);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                lightColor.color = lightIdleColor;
                OnSelectableExit?.Invoke();
            }
        }
        public override void UseSelectable()
        {
            SoundManager.PlaySound(SoundManager.Sound.Teleportation);
            player.gameObject.transform.position = endPoint.position;
        }
    }
}
