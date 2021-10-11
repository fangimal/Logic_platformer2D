using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
/*
{
[SerializeField] private float speed = 3f; // �������� ��������
[SerializeField] private int lives = 5; // ���������� ������
[SerializeField] private float jumpForce = 15f; // ���� ������

private Rigidbody2D rb;
private SpriteRenderer sprite;

private float _signPrevFrame;
private float _signCurrentFrame;

private void Awake()
{
    rb = GetComponent<Rigidbody2D>();
    sprite = GetComponentInChildren<SpriteRenderer>();
}

private void Update()
{
    if (Input.GetButton("Horizontal"))
        Run();
}

private void Run()
{
    Vector3 dir = transform.right * Input.GetAxis("Horizontal");
    transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

    sprite.flipX = dir.x < 0.0f;
}

}
*/

{
    [SerializeField] private float speed = 3f; // �������� ��������
    [SerializeField] private int lives = 5; // ���������� ������
    [SerializeField] private float jumpForce = 15f; // ���� ������

    private Rigidbody2D rb;
    private Animator _animator; // ��������� ��� �������� ������������ ������� Mecanim.

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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

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

        StateUpdate();
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
