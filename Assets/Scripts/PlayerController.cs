using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Velocidad de Movimiento")]
    public float walkSpeed = 5f; 
    public float runSpeed = 8f; 
    public float jumpForce = 7f; 

    private Rigidbody rb;
    private Animator anim;
    private bool isGrounded;
    private Transform mainCamTransform;

    private Vector2 inputVector;
    private bool isRunning;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>(); 

        if (Camera.main != null)
        {
            mainCamTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogError("PlayerController: No se encontró la Main Camera.");
        }

        // Asegura que el ratón esté bloqueado en el centro para jugar con la cámara
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");
        isRunning = Input.GetKey(KeyCode.LeftShift);

        HandleJump();
        UpdateAnimations(); 

        // Opcional: Presionar ESC para liberar el ratón si necesitas revisar el editor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
    if (mainCamTransform == null) return;

    
    Vector3 camForward = Vector3.Scale(mainCamTransform.forward, new Vector3(1, 0, 1)).normalized;
    Vector3 camRight = Vector3.Scale(mainCamTransform.right, new Vector3(1, 0, 1)).normalized;

    Vector3 moveDir = (camForward * inputVector.y + camRight * inputVector.x).normalized;
    float targetSpeed = isRunning ? runSpeed : walkSpeed;

    Vector3 velocity = moveDir * targetSpeed;
    rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

    
    if (camForward != Vector3.zero)
    {
        
        Quaternion targetRotation = Quaternion.LookRotation(camForward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.fixedDeltaTime);
    }
    }   
    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void UpdateAnimations()
    {
        if (anim == null) return;

        bool isMoving = inputVector.sqrMagnitude > 0.01f;
        anim.SetBool("isWalking", isMoving);
        anim.SetBool("isJumping", !isGrounded);
    }
}