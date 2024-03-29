using UnityEngine;

namespace LogicPlatformer
{
    public class Platform : IActivate
    {
        [SerializeField] private Transform startPosition;
        [SerializeField] private Transform endPosition;
        [SerializeField] private Transform platform;
        [SerializeField] private float duration = 2f;
        [SerializeField] private bool hideInStart = true;

        private void Awake()
        {
            platform.gameObject.SetActive(!hideInStart);

        }
        public override void Activate()
        {
            StartAction();
        }

        private void StartAction()
        {
            platform.transform.position = startPosition.position;
            platform.gameObject.SetActive(true);
            AnimationTransform.Move(platform, endPosition.position, duration);
        }

        public override void Deactivate()
        {
            AnimationTransform.Move(platform, startPosition.position, duration);
        }
    }
}
