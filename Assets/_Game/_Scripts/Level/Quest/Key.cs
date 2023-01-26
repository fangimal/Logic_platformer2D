using LogicPlatformer.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public class Key : MonoBehaviour
    {
        [SerializeField] private string keyID;
        [SerializeField] private bool isMatchet = false;
        [SerializeField] private BoxCollider2D bc;

        public string GetKeyID { get { return keyID;} }

        private void Awake()
        {
            if (!isMatchet)
            {
                Destroy(GetComponent<Rigidbody2D>());
                bc.enabled = false;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>() && collision.GetComponent<PlayerManager>().Key == null && !isMatchet)
            {
                if (transform.parent.gameObject.GetComponent<Parent>())
                {
                    Destroy(transform.parent.gameObject);
                }
                gameObject.transform.SetParent(collision.GetComponent<PlayerManager>().GetArm);
                gameObject.transform.localPosition = Vector2.zero;
                float rotationPlayerY = collision.transform.localEulerAngles.y;
                float rotationY = Mathf.Abs(rotationPlayerY) == 180 ? 180 : 180;
                gameObject.transform.localRotation = Quaternion.Euler(0, rotationY, 0);
                gameObject.transform.localScale = new Vector2(0.4f, 0.4f);
                collision.GetComponent<PlayerManager>().Key = this;
            }
            else if (isMatchet && collision.GetComponent<IActivate>()) 
            {
                Debug.Log("1111");
                collision.GetComponent<IActivate>().OpenWithKey();

            }
        }
    }
}
