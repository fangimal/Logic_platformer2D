using LogicPlatformer.Player;
using System;
using UnityEngine;

namespace LogicPlatformer
{
    public class Handle : MonoBehaviour
    {
        [SerializeField] private Transform handleContour;
        [SerializeField] private SpriteRenderer light;
        [SerializeField] private Color idleColor;
        [SerializeField] private Color activeColor;
        [SerializeField] private Animation handleAnimation;

        [SerializeField] private IActivate targetActivate;

        public event Action OnHadleEnter;
        public event Action OnHadleExit;
        public event Action OnHandleUsed;
        private void Start()
        {
            light.color = idleColor;
            handleContour.gameObject.SetActive(false);

            OnHandleUsed += () => 
            { 
                light.color = activeColor; 
            };
        }

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.GetComponent<PlayerManager>())
        //    {
        //        handleContour.gameObject.SetActive(true);
        //        OnHadleEnter?.Invoke();
        //    }
        //}
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                handleContour.gameObject.SetActive(true);
                OnHadleEnter?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            handleContour.gameObject.SetActive(false);
            OnHadleExit?.Invoke();
        }

        public void UseHandle()
        {
            handleContour.gameObject.SetActive(false);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            handleAnimation.Play();
        }

        public void SwitchLight() //Animation Event
        {
            OnHandleUsed?.Invoke();
        }

        public void StartTargetAnimation()
        {
            targetActivate.Activate();
        }
    }
}
