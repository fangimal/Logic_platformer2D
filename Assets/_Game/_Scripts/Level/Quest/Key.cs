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
            if (collision.GetComponent<PlayerManager>() && collision.GetComponent<PlayerManager>().Key == null)
            {
                gameObject.transform.SetParent(collision.GetComponent<PlayerManager>().GetArm);
                gameObject.transform.localPosition = Vector2.zero;
                float rotationPlayerY = collision.transform.localEulerAngles.y;
                float rotationY = Mathf.Abs(rotationPlayerY) == 180 ? 180 : 180;
                gameObject.transform.localRotation = Quaternion.Euler(0, rotationY, 0);
                gameObject.transform.localScale = new Vector2(0.4f, 0.4f);
                collision.GetComponent<PlayerManager>().Key = this;
            }
        }
    }
}
