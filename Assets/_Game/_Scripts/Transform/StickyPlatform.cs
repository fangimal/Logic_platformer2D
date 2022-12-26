using LogicPlatformer.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public class StickyPlatform : MonoBehaviour
    {


        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    if (collision.collider.GetComponent<PlayerManager>())
        //    {
        //        collision.gameObject.transform.SetParent(transform);
        //        Debug.Log("PlayerManager");
        //    }
        //}

        //private void OnCollisionExit2D(Collision2D collision)
        //{
        //    if (collision.collider.GetComponent<PlayerManager>())
        //    {
        //        collision.gameObject.transform.SetParent(null);
        //    }
        //}

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                collision.gameObject.transform.SetParent(transform);
                Debug.Log("PlayerManager");
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                collision.gameObject.transform.SetParent(null);
            }
        }


        //private void OnCollisionEnter2D(Collider2D collision)
        //{
        //    if (collision.GetComponent<PlayerManager>())
        //    {
        //        collision.GetComponent<PlayerManager>().transform.SetParent(transform);
        //    }
        //}

        //private void OnCollisionEnter2D(Collider2D collision)
        //{
        //    if (collision.GetComponent<PlayerManager>())
        //    {
        //        collision.gameObject.transform.SetParent(null);
        //    }
        //}
    }
}
