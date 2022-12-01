using System;
using UnityEngine;
using LogicPlatformer.Player;

namespace LogicPlatformer.Level
{
    internal class ExitDoor : MonoBehaviour
    {
        [SerializeField] private Color _enterColor;
        private Color _myColor;

        public event Action OnDoorEnter;
        public event Action OnDoorExit;

        private void Start()
        {
            _myColor = gameObject.GetComponent<SpriteRenderer>().color;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                gameObject.GetComponent<SpriteRenderer>().color = _enterColor;
                OnDoorEnter?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            gameObject.GetComponent<SpriteRenderer>().color = _myColor;
            OnDoorExit?.Invoke();
        }

    }
}

