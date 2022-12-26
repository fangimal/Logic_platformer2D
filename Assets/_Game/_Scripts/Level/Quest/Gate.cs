using LogicPlatformer.Player;
using UnityEngine;

namespace LogicPlatformer
{
    public class Gate : IActivate
    {
        [SerializeField] private bool isLocking = false;

        [SerializeField] private Transform lockIcon;
        [SerializeField] private BoxCollider2D colider;

        [SerializeField] private Transform gateTransform;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private float duration = 1f;

        [Space(10), Header("Set Key")]
        [SerializeField] private Key key;

        private Animation anim;
        private bool isOpen = true;

        private void Start()
        {
            anim = GetComponent<Animation>();
            lockIcon.gameObject.SetActive(isLocking);
            colider.enabled = isLocking;

            if (key != null)
            {
                lockIcon.gameObject.GetComponent<SpriteRenderer>().color = key.gameObject.GetComponent<SpriteRenderer>().color;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>() && collision.GetComponent<PlayerManager>().Key != null)
            {
                if (key.GetKeyID == collision.GetComponent<PlayerManager>().Key.GetKeyID)
                {
                    colider.enabled = false;
                    collision.GetComponent<PlayerManager>().FreedArm();
                    anim.Play();
                }
            }
        }

        public override void Activate()
        {
            Animation();
        }

        private void Animation()
        {
            if (isOpen)
            {
                isOpen = false;
                AnimationTransform.Move(gateTransform, endPoint.position, duration);
            }
            else
            {
                isOpen = true;
                AnimationTransform.Move(gateTransform, startPoint.position, duration);
            }
        }
    }
}
