using LogicPlatformer.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public class FallingPlatform : MonoBehaviour
    {
        Rigidbody2D rb;
        // Start is called before the first frame update
        void Start()
        {
        rb = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                Invoke("FallPlatform", 1f);
                Destroy(gameObject, 1.5f);
            }
        }

        void FallPlatform()
        {
            rb.isKinematic = false;
        }
    }
}
