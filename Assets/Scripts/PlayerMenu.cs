using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class PlayerMenu : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    bool flipped;
    public void flip(){
        if(!flipped){
            flipped = true;
            rb.gravityScale *= -1;
            anim.SetBool("isFlipped", true);
            anim.SetBool("isJumping", true);
            GameObject background = Background.instance.gameObject;
            background.GetComponentInChildren<Light2D>().color = Color.black;
        }
    }
}
