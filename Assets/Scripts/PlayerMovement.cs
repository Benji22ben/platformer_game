using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private SpriteRenderer sprite;
    private bool inWater = false;

    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private BoxCollider2D waterCollider;
    
    private enum MovementState { Idle, Running, Jumping }
    private MovementState state = MovementState.Idle;
    
    [SerializeField] public float jumpForce = 5f;
    [SerializeField] public float speed = 10;
    private float _dirX = 0f;
    private bool _isJumping = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _dirX = Input.GetAxisRaw("Horizontal");
        Vector2 velocity = rb.velocity;
        velocity = new Vector2(_dirX * speed, velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            velocity = new Vector2(velocity.x, jumpForce);
        }

        if (inWater)
        {
            velocity = new Vector2(velocity.x * 0.95f, velocity.y * 0.95f);
        }
        
        rb.velocity = velocity;
        
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state = _isJumping ? MovementState.Jumping : MovementState.Idle;
        
        if (_dirX > 0f)
        {
            if (IsGrounded())
            {
                state = MovementState.Running;
            }
            transform.localScale= new Vector3(1, 1, 1);
        }
        else if (_dirX < 0f)
        {
            if (IsGrounded())
            {
                state = MovementState.Running;
            }
            transform.localScale= new Vector3(-1, 1, 1);
        } 
        
        if (rb.velocity.y > .1f)
        {
            state = MovementState.Jumping;
            _isJumping = true;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.Jumping;
            _isJumping = false;
        }
        
        anim.SetInteger("state", (int) state);
    }
    
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f,
            groundLayerMask);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            inWater = true;
        }
    }
}
