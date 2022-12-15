using UnityEngine;

namespace LogicPlatformer.Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private Transform arm;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private int lives = 5; // количество жизней
        [SerializeField] private float jumpForce = 15f; // сила прыжка

        [SerializeField] private Rigidbody2D rb;
        //[SerializeField] private Animator _animator; // Интерфейс для контроля анимационной системы Mecanim.

        private Vector3 _leftFlip = new Vector3(0, 180, 0);
        private Vector2 _horizontalVelocity;
        private float _signPrevFrame;
        private float _signCurrentFrame;
        private bool isGrounded;
        private Vector2 moveDirection;

        [HideInInspector] public Key Key = null;
        public Transform GetArm => arm;
        public PlayerController GetPlayerController => playerController;
        public float MoveSpeed;
        public void Initialize(PlayerData playerData, Transform startPosition)
        {
            //rb = GetComponent<Rigidbody2D>();
            //_animator = GetComponent<Animator>();

            gameObject.transform.position = startPosition.position;
            gameObject.SetActive(true);

            FreedArm();

            Debug.Log("Player Initialize");
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
        private void FixedUpdate()
        {
            //MoveInternal();
            //CheckGround();
        }

        public void Move(Vector2 direction)
        {
            moveDirection = direction;
            Flip();
        }

        private void MoveInternal()
        {
            _horizontalVelocity.Set(moveDirection.x * MoveSpeed, rb.velocity.y);
            rb.velocity = _horizontalVelocity;
        }

        private void Flip()
        {
            _signCurrentFrame = moveDirection.x == 0 ? _signPrevFrame : Mathf.Sign(moveDirection.x);

            if (_signCurrentFrame != _signPrevFrame)
            {
                transform.rotation = Quaternion.Euler(moveDirection.x < 0 ? _leftFlip : Vector3.zero);
            }
            _signPrevFrame = _signCurrentFrame;
        }

        public void Jump()
        {
            if (isGrounded)
            {
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        //private void CheckGround() //TODO можно прыгать от триггера
        //{
        //    Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        //    isGrounded = collider.Length > 1;
        //}

        public void FreedArm()
        {
            if (Key != null)
            {
                Destroy(Key.gameObject);
            }
            //Destroy(arm.transform.GetChild(0).gameObject);
        }
    }
}

