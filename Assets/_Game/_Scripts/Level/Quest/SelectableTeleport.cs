using LogicPlatformer.Player;
using System;
using UnityEngine;

namespace LogicPlatformer
{
    public class SelectableTeleport : ISelectableItem
    {
        [SerializeField] private Transform endPoint;
        [SerializeField] private Transform detected;
        [SerializeField] private Transform detectedSelect;

        private PlayerManager player;
        public override event Action<int> OnSelectableEnter;
        public override event Action OnSelectableExit;

        private void Awake()
        {
            detectedSelect.gameObject.SetActive(false);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                player = collision.GetComponent<PlayerManager>();
                detectedSelect.gameObject.SetActive(true);
                OnSelectableEnter?.Invoke(Index);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                detectedSelect.gameObject.SetActive(false);
                OnSelectableExit?.Invoke();
            }
        }
        public override void UseSelectable()
        {
            player.gameObject.transform.position = endPoint.position;
        }
    }
}
