using UnityEngine;

namespace LogicPlatformer.Player
{
    public class PlayerManager : MonoBehaviour, IControlable
    {
        [SerializeField] private Transform arm;
        [SerializeField] private float speed = 3f; // скорость движения
        [SerializeField] private int lives = 5; // количество жизней
        [SerializeField] private float jumpForce = 15f; // сила прыжка

        [SerializeField] private Rigidbody2D rb;
        //[SerializeField] private Animator _animator; // Интерфейс для контроля анимационной системы Mecanim.

        private RaycastHit2D _checkBorderHeadRay;
        private RaycastHit2D _checkBorderBodyRay;

        private Vector3 _leftFlip = new Vector3(0, 180, 0);
        private Vector2 _horizontalVelocity;
        private float _horizontalSpeed;
        private float _signPrevFrame;
        private float _signCurrentFrame;
        private bool isGrounded;
        private bool _isBorder;

        public Transform CheckBorderHeadRayTransform;
        public Transform CheckBorderBodyRayTransform;
        private LayerMask BorderLayerMask;

        public Transform GetArm => arm;
        public Key Key = null;
        public float MoveSpeed;
        public float RayDistance;
        private Vector2 moveDirection;

        public void Initialize(Transform startPosition)
        {
            //rb = GetComponent<Rigidbody2D>();
            //_animator = GetComponent<Animator>();

            gameObject.transform.position = startPosition.position;
            gameObject.SetActive(true);
        }
        //private void Start()
        //{
        //    rb = GetComponent<Rigidbody2D>();
        //    _animator = GetComponent<Animator>();
        //}

        private void FixedUpdate()
        {
            MoveInternal();
            //Move();
            CheckGround();
        }

        //private void Update()
        //{
        //    _horizontalSpeed = Input.GetAxis("Horizontal");

        //    //moveDirection = new Vector2(Input.GetAxis("Horizontal"), 0f);

        //    if (Input.GetButtonDown("Jump"))
        //    { 
        //        Jump(); 
        //    }

        //    Flip();
        //}

        private void StateUpdate()
        {
            _checkBorderHeadRay = Physics2D.Raycast
                (
                    CheckBorderHeadRayTransform.position,
                    CheckBorderHeadRayTransform.right,
                    RayDistance,
                    BorderLayerMask
                );

            _checkBorderBodyRay = Physics2D.Raycast
                (
                    CheckBorderBodyRayTransform.position,
                    CheckBorderBodyRayTransform.right,
                    RayDistance,
                    BorderLayerMask
                );

            _isBorder = _checkBorderHeadRay || _checkBorderBodyRay;

            Debug.DrawRay
                (
                transform.position,
                -Vector2.up * RayDistance,
                Color.red
                );

            Debug.DrawRay
                (
                CheckBorderHeadRayTransform.position,
                CheckBorderHeadRayTransform.right * RayDistance,
                Color.red
                );

            Debug.DrawRay
                (
                CheckBorderBodyRayTransform.position,
                CheckBorderBodyRayTransform.right * RayDistance,
                Color.red
                );
        }

        private void Move()
        {
            _horizontalVelocity.Set(_horizontalSpeed * MoveSpeed, rb.velocity.y);
            rb.velocity = _horizontalVelocity;
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

        private void CheckGround() //TODO можно прыгать от триггера
        {
            Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
            isGrounded = collider.Length > 1;
        }

        public void AplayArm()
        {
            Destroy(arm.transform.GetChild(0).gameObject);
        }

    }
}

