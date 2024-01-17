using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//made by Aiden
public class MovingObjects : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public bool shouldRotate = false;
    public bool shouldMove = false;

    private Rigidbody2D rb;

    public float moveSpeed = 5f; // Adjust the speed as needed
    private bool moveRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Set gravity scale to 0 to allow the block to float
        rb.gravityScale = 0f;
    }

    void Update()
    {
        // Rotate the object around the Z-axis at a constant speed
        if (shouldRotate)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        // Move the object in one direction and reverse upon collision with "MovingBlock"
        if (shouldMove)
        {
            if (moveRight)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is not a "MovingBlock"
        if (other.CompareTag("MovingBlock"))
        {
            // Reverse the direction upon colliding with an object
            moveRight = !moveRight;
        }
    }
}
