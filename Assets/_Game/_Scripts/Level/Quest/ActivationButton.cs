using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public class ActivationButton : MonoBehaviour
    {
        [SerializeField] private IActivate targetActivate;

        [SerializeField] private Transform detected;

        [SerializeField] private bool doubleButton = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == detected.gameObject) 
            {
                targetActivate.Activate();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == detected.gameObject && doubleButton)
            {
                targetActivate.Activate();
            }
        }

    }
}
