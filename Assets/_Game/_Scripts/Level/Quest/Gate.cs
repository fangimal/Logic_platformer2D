using LogicPlatformer.Player;
using UnityEngine;

namespace LogicPlatformer
{
    public class Gate : IActivate
    {
        [SerializeField] private bool isLocking = false;
        [SerializeField] private Transform lockIcon;
        [SerializeField] private BoxCollider2D colider;
        [SerializeField] private Key key;

        private Animation anim;

        private void Start()
        {
            anim = GetComponent<Animation>();
            lockIcon.gameObject.SetActive(isLocking);
            colider.enabled = isLocking;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerManager>() && collision.GetComponent<PlayerManager>().Key != null)
            {
                if (key.GetKeyID == collision.GetComponent<PlayerManager>().Key.GetKeyID)
                {
                    colider.enabled = false;
                    collision.GetComponent<PlayerManager>().AplayArm();
                    anim.Play();
                }
            }
        }

        public override void Activate()
        {
            anim.Play();
        }
    }
}
