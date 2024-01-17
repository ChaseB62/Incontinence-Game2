using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //this epic movement script was made by Chase
    public KeyCode upKey;
    public KeyCode rightKey;
    public KeyCode leftKey;

    public LayerMask ground;

    public bool isGrounded;
    public bool isMoving;

    public Collider2D groundDetector;
    public Collider2D wallJump1;
    public Collider2D wallJump2;

    public float jumpForce = 100f;
    public float walkSpeed = 10f;

    public float maxSpeed = 10f;
    public float maxAirSpeed = 12f;

    public float airWalkSpeed = 3f;

    public Rigidbody2D rb;

    private particleHandler particleHandler;

    private bool right = false;
    private bool left = false;

    void Start(){
        particleHandler = GetComponent<particleHandler>();
    }

    void Update()
    {
        if(groundDetector.IsTouchingLayers(ground) || wallJump1.IsTouchingLayers(ground) || wallJump2.IsTouchingLayers(ground)){
            isGrounded = true;
            Debug.Log("touching ground");
        } else {
            isGrounded = false;
            Debug.Log("not touching ground");
        }

        if(Input.GetKeyDown(upKey) && isGrounded)
        {
            Debug.Log("jamped");
            particleHandler.Jump();
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }

        if(Input.GetKey(rightKey) && isGrounded){
            Debug.Log("right");
            right = true;
        } else if(Input.GetKey(rightKey) && !isGrounded)
        {
            
            right = true;
        } else {
            right = false;
        }

        if(Input.GetKey(leftKey) && isGrounded){
            Debug.Log("left");
            left = true;
        } else if(Input.GetKey(leftKey) && !isGrounded)
        {
            left = true;
        } else {
            left = false;
        }

        if (rb.velocity.x > maxSpeed && isGrounded)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        } 
        else if(rb.velocity.x > maxAirSpeed && !isGrounded)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }

        if (rb.velocity.x < (maxSpeed * -1) && isGrounded)
        {
            rb.velocity = new Vector2((maxSpeed * -1), rb.velocity.y);
        } 
        else if(rb.velocity.x < (maxAirSpeed * -1) && !isGrounded)
        {
            rb.velocity = new Vector2((maxAirSpeed * -1), rb.velocity.y);
        }


    }

    public void FixedUpdate(){
        if(left && isGrounded){
            rb.AddForce(transform.right * (walkSpeed * -1));
        } else if(left && !isGrounded){
            rb.AddForce(transform.right * (airWalkSpeed * -1));
        }

        if(right && isGrounded){
            rb.AddForce(transform.right * walkSpeed);
        } else if(right && !isGrounded){
            rb.AddForce(transform.right * airWalkSpeed);
        }
    }
}
