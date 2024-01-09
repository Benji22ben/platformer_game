using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private SpriteRenderer sprite;

    [SerializeField] private LayerMask groundLayerMask;
    
    private enum MovementState { Idle, Running, Jumping }
    private MovementState state = MovementState.Idle;
    
    [SerializeField] public float jumpForce = 5f;
    [SerializeField] public float speed = 10;
    private float _dirX = 0f;
    private bool isJumping = false;
    
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
        rb.velocity = new Vector2(_dirX * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state = isJumping ? MovementState.Jumping : MovementState.Idle;
        
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
            isJumping = true;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.Jumping;
            isJumping = false;
        }
        
        anim.SetInteger("state", (int) state);
    }
    
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f,
            groundLayerMask);
    }
}
