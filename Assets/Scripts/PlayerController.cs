using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    public float walkSpeed = 5f; 
    public float runSpeed = 8f; 
    public float jumpForce = 7f; 

    // Referencias
    public Transform cameraFollowTarget; 
    private Rigidbody rb;
    private bool isGrounded;

    
    private Animator anim; 

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>(); 
    }

    
    void Update()
    {
        HandleMovement(); 
        HandleJump();
        UpdateAnimations(); 
    }
    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        
        Vector3 moveDir = cameraFollowTarget.forward * moveZ + cameraFollowTarget.right * moveX;
        moveDir.y = 0;

      
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void UpdateAnimations()
    {
        bool isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        anim.SetBool("isWalking", isMoving);

        anim.SetBool("isJumping", !isGrounded);
        
        
    }
}

