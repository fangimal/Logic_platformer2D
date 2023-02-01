using System;
using UnityEngine;
using LogicPlatformer.Player;

namespace LogicPlatformer.Level
{
    internal class ExitDoor : MonoBehaviour
    {
        [SerializeField] private Color enterColor;
        [SerializeField] private Color lightIdleColor;
        [SerializeField] private Color lightEnterColor;
        [SerializeField] private Transform lightTransform;

        private SpriteRenderer lightColor;
        private SpriteRenderer door;

        private Color myColor;

        public event Action OnDoorEnter;
        public event Action OnDoorExit;

        private void Start()
        {
            myColor = gameObject.GetComponent<SpriteRenderer>().color;
            door = gameObject.GetComponent<SpriteRenderer>();
            lightColor = lightTransform.gameObject.GetComponent<SpriteRenderer>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                door.color = enterColor;
                lightColor.color = lightEnterColor;
                OnDoorEnter?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                door.color = myColor;
                lightColor.color = lightIdleColor;
                OnDoorExit?.Invoke();
            }
        }

    }
}

