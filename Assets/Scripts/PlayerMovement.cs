using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private enum MovementState { idle, running, jumping }
    private MovementState state = MovementState.idle;
    
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
    }

    // Update is called once per frame
    void Update()
    {
        _dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(_dirX * speed, rb.velocity.y);
        
        if (Input.GetButtonDown("Jump")) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        
        
        if (_dirX > 0f && !isJumping)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (_dirX < 0f && !isJumping)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else 
        {
            state = MovementState.idle;
        }
        
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
            isJumping = true;
        }
        else if (rb.velocity.y < -.1f || rb.velocity.y == 0f && isJumping)
        {
            state = MovementState.jumping;
            isJumping = false;
        }
        
        anim.SetInteger("state", (int) state);
    }
}
