using UnityEngine;

namespace LogicPlatformer.Player
{
    public class PlayerManager : MonoBehaviour
    {
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

        public float MoveSpeed;
        public float RayDistance;

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
            Move();
            CheckGround();
        }

        private void Update()
        {
            _horizontalSpeed = Input.GetAxis("Horizontal");

            if (isGrounded && Input.GetButtonDown("Jump"))
                Jump();

            //StateUpdate();
            // Jump();
            Flip();
        }

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

        private void Flip()
        {
            _signCurrentFrame = _horizontalSpeed == 0 ? _signPrevFrame : Mathf.Sign(_horizontalSpeed);

            if (_signCurrentFrame != _signPrevFrame)
            {
                transform.rotation = Quaternion.Euler(_horizontalSpeed < 0 ? _leftFlip : Vector3.zero);
            }
            _signPrevFrame = _signCurrentFrame;
        }

        private void Jump()
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        private void CheckGround()
        {
            Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
            isGrounded = collider.Length > 1;
        }

    }
}

