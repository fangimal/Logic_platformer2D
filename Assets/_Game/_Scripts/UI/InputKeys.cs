using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LogicPlatformer
{
    public class InputKeys : MonoBehaviour
    {
        private PlayerController playerController;

        public void Init(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        public void OnLeftDown()
        {
            playerController.HorizontalInput(-1f);
        }

        public void OnLeftUp()
        {
            playerController.HorizontalInput(0f);
        }
        public void OnRightDown()
        {
            playerController.HorizontalInput(1f);
        }
        public void OnRightUp()
        {
            playerController.HorizontalInput(0f);
        }

        public void OnJump()
        {
            playerController.JumpInput();
        }
    }
}
