using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public class Platform : IActivate
    {
        [SerializeField] private Transform startPosition;
        [SerializeField] private Transform endPosition;
        [SerializeField] private Transform platform;
        [SerializeField] private float duration = 2f;

        private void Awake()
        {
            platform.gameObject.SetActive(false);
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
    }
}
