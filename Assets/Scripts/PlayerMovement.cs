using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpForce = 5f;
    public float speed = 10;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
        
        if (Input.GetButtonDown("Jump")) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (dirX > 0f)
        {
            anim.SetBool("running", true);
        }
        else if (dirX < 0f)
        {
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }
    } 
}
