using System;
using UnityEngine;

namespace LogicPlatformer.Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private Transform arm;
        [SerializeField] private Transform visualizer;
        [SerializeField] private PlayerController playerController;

        [SerializeField] private PlayerVisual visual;

        [HideInInspector] public Key Key = null;
        public Transform GetArm => arm;
        public PlayerController GetPlayerController => playerController;
        public float MoveSpeed;

        public event Action IsDead;
        public void Initialize(PlayerData playerData, Transform startPosition)
        {
            playerController.IsAlive= true;
            playerController.SetAnimator(visual.GetAnimator);

            gameObject.transform.position = startPosition.position;
            gameObject.SetActive(true);

            FreedArm();

            Debug.Log("Player Initialize");
        }

        public void PlayerDead()
        {
            playerController.IsAlive = false;
            IsDead?.Invoke();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                playerController.HorizontalInput(-1f);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                playerController.HorizontalInput(0f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerController.HorizontalInput(1f);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                playerController.HorizontalInput(0f);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                playerController.JumpInput(1);
            }
        }

        public void FreedArm()
        {
            if (Key != null)
            {
                Destroy(Key.gameObject);
            }
        }
    }
}

