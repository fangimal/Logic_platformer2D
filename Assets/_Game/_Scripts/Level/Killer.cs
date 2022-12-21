using LogicPlatformer.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public class Killer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                collision.GetComponent<PlayerManager>().PlayerDead();
            }
        }
    }
}
