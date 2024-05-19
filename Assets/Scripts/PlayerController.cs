using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
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
    public Rigidbody2D[] rbs;
    Manager manager;
    public Vector3 offset;

    public void Awake(){
        instance = this;
    }

    void Start(){
        manager = Manager.instance;
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

        if(Input.GetButtonDown("Restart")){
            manager.Restart();
        }

        if(Input.GetButtonDown("Jump") && isGrounded){
            Jump();
        }
    }

    void FixedUpdate(){

        isGrounded = false;
        anim.SetBool("isJumping", true);
        if (isFlipped)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position - offset, new Vector2(boxCollider.size.x - boxColliderX, boxCollider.size.y), 0f, whatIsGround);
            if(colliders.Length != 0){
                isGrounded = true;
                anim.SetBool("isJumping", false);
            }
        }
        else
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + offset, new Vector2(boxCollider.size.x - boxColliderX, boxCollider.size.y), 0f, whatIsGround);
            if(colliders.Length != 0){
                isGrounded = true;
                anim.SetBool("isJumping", false);
            }
        }
    
        rb.velocity = new Vector2(horizontalVelocity * Time.fixedDeltaTime, rb.velocity.y);
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
        GameObject background = Background.instance.gameObject;
        isFlipped = !isFlipped;
        if (isFlipped)
        {
            background.GetComponentInChildren<Light2D>().color = Color.black;
        }
        else
        {
            background.GetComponentInChildren<Light2D>().color = Color.white;
        }
        anim.SetBool("isFlipped", isFlipped);
        foreach (Rigidbody2D rbToChange in rbs)
        {
            rbToChange.gravityScale *= -1;
        }
        
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offset, new Vector2(boxCollider.size.x - boxColliderX, boxCollider.size.y));
    }
}
