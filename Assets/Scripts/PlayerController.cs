using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    float horizontalVelocity;
    bool isGrounded;
    public LayerMask whatIsGround;
    public Rigidbody2D rb;
    BoxCollider2D boxCollider;
    public float boxColliderX;
    bool isFlipped = false;
    public Animator anim;

    void Start(){
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");

        horizontalVelocity = horizontalMove * speed;

        if(Input.GetButtonDown("Flip") && isGrounded){
            Flip();
        }
    }

    void FixedUpdate(){

        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(boxCollider.size.x - boxColliderX, boxCollider.size.y), 0f, whatIsGround);
        if(colliders.Length != 0){
            isGrounded = true;
        }

        rb.velocity = new Vector2(horizontalVelocity * Time.fixedDeltaTime, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && isGrounded){
            Jump();
        }
    }

    void Jump(){
        isGrounded = false;
            
        if(isFlipped){
            rb.AddForce(new Vector2 (0f, jumpForce * -1));
        }
        else
        {
            rb.AddForce(new Vector2 (0f, jumpForce));   
        }
    }

    void Flip(){
        isFlipped = !isFlipped;
        anim.SetBool("isFlipped", isFlipped);
        rb.gravityScale *= -1;
    }
}
