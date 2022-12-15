using System;
using UnityEngine;

namespace LogicPlatformer
{
    public class CharacterInputController : MonoBehaviour
    {
        private IControlable controlable;

        //private void Awake()
        //{
        //    controlable = GetComponent<IControlable>();

        //    if (controlable == null)
        //    {
        //        throw new Exception($"There is no IControllable component on the object: {gameObject.name}");
        //    }
        //}

        private void Update()
        {
            //ReadMove();
            //ReadJump();
        }
        private void ReadMove()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var direction = new Vector2(horizontal, 0f);

            controlable.Move(direction);
        }

        private void ReadJump()
        {
            if (Input.GetButtonDown("Jump"))
            {
                controlable.Jump();
            }
        }
    }
}
