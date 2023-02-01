using LogicPlatformer.Player;
using System;
using UnityEngine;

namespace LogicPlatformer
{
    public class SelectableRoomDoor : ISelectableItem
    {
        [SerializeField] private Color enterColor;
        [SerializeField] private Color lightIdleColor;
        [SerializeField] private Color lightEnterColor;
        [SerializeField] private Transform lightTransform;
        [SerializeField] private Transform thisRoom;
        [SerializeField] private Transform nextRoom;
        [SerializeField] private Transform exitDoor;

        private SpriteRenderer lightColor;
        private SpriteRenderer door;
        private Color myColor;
        private PlayerManager player;

        public override event Action<int> OnSelectableEnter;
        public override event Action OnSelectableExit;
        private void Start()
        {
            myColor = gameObject.GetComponent<SpriteRenderer>().color;
            door = gameObject.GetComponent<SpriteRenderer>();
            lightColor = lightTransform.gameObject.GetComponent<SpriteRenderer>();
            player = FindObjectOfType<PlayerManager>();
        }
        public override void UseSelectable()
        {
            thisRoom.gameObject.SetActive(false);
            nextRoom.gameObject.SetActive(true);
            if (exitDoor)
            {
                player.transform.position = exitDoor.transform.position;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                door.color = enterColor;
                lightColor.color = lightEnterColor;
                OnSelectableEnter?.Invoke(Index);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                door.color = myColor;
                lightColor.color = lightIdleColor;
                OnSelectableExit?.Invoke();
            }
        }
    }
}
