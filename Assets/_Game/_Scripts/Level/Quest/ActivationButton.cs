using UnityEngine;

namespace LogicPlatformer
{
    public class ActivationButton : MonoBehaviour
    {
        [SerializeField] private IActivate targetActivate;

        [SerializeField] private Transform detected;

        [SerializeField] private SelectActivation activationMethod = SelectActivation.Physical;

        private Rigidbody2D rb;
        private bool oneClick = false;
        private enum SelectActivation
        {
            Physical,
            DoublePhysical,
            Click
        }

        private void Start()
        {
            if (activationMethod == SelectActivation.Click)
            {
                rb = GetComponent<Rigidbody2D>();
                rb.isKinematic = true;
            } 
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == detected.gameObject &&
                activationMethod != SelectActivation.Click)
            {
                targetActivate.Activate();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == detected.gameObject &&
                activationMethod == SelectActivation.DoublePhysical)
            {
                targetActivate.Deactivate();
            }
        }

        private void OnMouseDown()
        {
            if (activationMethod == SelectActivation.Click && !oneClick)
            {
                oneClick = true;
                rb.isKinematic = false;
                rb.gravityScale = 10;
                targetActivate.Activate();
            }
        }
    }
}
