using LogicPlatformer.Player;
using System;
using UnityEngine;

namespace LogicPlatformer
{
    public class ActivationHandle : MonoBehaviour
    {
        [SerializeField] private Transform handleContour;
        [SerializeField] private SpriteRenderer light;
        [SerializeField] private Color idleColor;
        [SerializeField] private Color activeColor;
        [SerializeField] private Animation handleAnimation;

        [SerializeField] private IActivate targetActivate;

        public int Index;

        private event Action OnHandleUsed;

        public event Action <int>OnHadleEnter;
        public event Action OnHadleExit;
        private void Start()
        {
            light.color = idleColor;
            handleContour.gameObject.SetActive(false);

            OnHandleUsed += () => 
            { 
                light.color = activeColor;
            };
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>())
            {
                handleContour.gameObject.SetActive(true);
                OnHadleEnter?.Invoke(Index);
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
            Debug.Log("Hadle used: " + Index);
        }

        public void SwitchLight() //Animation Event
        {
            OnHandleUsed?.Invoke();
        }

        public void StartTargetAnimation()
        {
            targetActivate.Activate();
            UseHandle();
        }
    }
}
