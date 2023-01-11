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
        [SerializeField] private Transform light;
        [SerializeField] private Transform thisRoom;
        [SerializeField] private Transform nextRoom;

        private SpriteRenderer lightColor;
        private SpriteRenderer door;
        private Color myColor;

        public override event Action<int> OnSelectableEnter;
        public override event Action OnSelectableExit;
        private void Start()
        {
            myColor = gameObject.GetComponent<SpriteRenderer>().color;
            door = gameObject.GetComponent<SpriteRenderer>();
            lightColor = light.gameObject.GetComponent<SpriteRenderer>();
        }
        public override void UseSelectable()
        {
            thisRoom.gameObject.SetActive(false);
            nextRoom.gameObject.SetActive(true);
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
