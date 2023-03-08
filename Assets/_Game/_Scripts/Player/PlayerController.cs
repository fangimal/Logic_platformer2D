using System;
using UnityEngine;

namespace LogicPlatformer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [Range(0.0f, 20.0f)]
        [SerializeField] private float moveSpeed = 5f;
        [Range(0.0f, 20.0f)]
        [SerializeField] private float jumpForce = 15f;
        [SerializeField] private float checkRadius = 0.3f;
        [SerializeField] private ParticleSystem dustParticle;

        private float xInput;
        private Animator animator = null;
        private bool facingRight;
        private bool jump;
        private Rigidbody2D rb;
        private bool isGrounded;
        private bool isJumping;
        private bool groundDedected = false;
        private float dustTimer;

        public bool IsAlive { get; set; } = true;

        public LayerMask groundLayer;
        public Transform groundCheck;

        public float MoveSpeed
        {
            get
            {
                return moveSpeed;
            }
            set
            {
                moveSpeed = value;
            }
        }

        public float JumpForce
        {
            get
            {
                return jumpForce;
            }
            set
            {
                jumpForce = value;
            }
        }

        public event Action PlayerMoved;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void SetAnimator(Animator animator)
        {
            this.animator = animator;
        }
        private void Start()
        {
            facingRight = true;
        }

        private void Update()
        {
            if (jump && !isJumping)
            {
                jump = false;
                isJumping = true;
            }
        }
        private void FixedUpdate()
        {
            if (IsAlive)
            {
                rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);

                animator.SetBool("grounded", isGrounded);
                animator.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));

                //flipping the player
                if (xInput < 0 && facingRight)
                {
                    FlipPlayer();
                }
                else if (xInput > 0 && !facingRight)
                {
                    FlipPlayer();
                }

                isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);    //Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);

                if (!groundDedected && isGrounded)
                {
                    CreateDust();
                    groundDedected = true;
                }

                if (!isGrounded && isJumping)
                {
                    isJumping = false;
                    return;
                }

                if (isGrounded && isJumping)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    //rb.AddForce(transform.up * jumpForce * jumpMultiple, ForceMode2D.Impulse);

                    isJumping = false;
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        private void FlipPlayer()
        {
            facingRight = !facingRight;

            Vector2 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;

            if (isGrounded)
            {
                CreateDust();
            }

        }
        public void HorizontalInput(float value)
        {
            xInput = value;
            if (isGrounded)
            {
                PlayerMoved?.Invoke();
            }
        }

        public void JumpInput()
        {
            jump = true;
            groundDedected = false;
        }

        private void CreateDust()
        {
            if (isGrounded && dustTimer + 0.3f < Time.time)
            {
                dustParticle.Play();
                SoundManager.PlaySound(SoundManager.Sound.Jump, transform.position);
                dustTimer = Time.time;
            }
        }
    }
}
