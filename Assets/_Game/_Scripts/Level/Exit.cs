using LogicPlatformer.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public class Exit : MonoBehaviour
    {
        public event Action OnExit;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                collision.GetComponent<PlayerManager>().UnParent();
                OnExit?.Invoke();
            }
        }
    }
}
