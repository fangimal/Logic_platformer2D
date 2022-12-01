using LogicPlatformer.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public class Key : MonoBehaviour
    {
        [SerializeField] private string keyID;

        public string GetKeyID { get { return keyID;} }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                gameObject.transform.SetParent(collision.GetComponent<PlayerManager>().GetArm);
                gameObject.transform.localPosition = Vector2.zero;
                gameObject.transform.localScale = new Vector2(0.4f, 0.4f);
                collision.GetComponent<PlayerManager>().Key = this;
            }
        }
    }
}
