using UnityEngine;

namespace LogicPlatformer
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        public Animator GetAnimator => animator;
    }
}
