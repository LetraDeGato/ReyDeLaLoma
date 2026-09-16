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

    [Header("Referencias")]
    [Tooltip("Asigna la Cámara Principal aquí. Si se deja nulo, se asignará automática en Start.")]
    public Transform cameraFollowTarget; 
    
    private Rigidbody rb;
    private Animator anim;
    private bool isGrounded;

    // Almacenamiento de entrada
    private Vector2 inputVector;
    private bool isRunning;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>(); 

        // Respaldo de seguridad para evitar congelamientos por referencias nulas
        if (cameraFollowTarget == null && Camera.main != null)
        {
            cameraFollowTarget = Camera.main.transform;
        }
    }

    private void Update()
    {
        // 1. Capturar entradas en Update
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");
        isRunning = Input.GetKey(KeyCode.LeftShift);

        HandleJump();
        UpdateAnimations(); 
    }

    private void FixedUpdate()
    {
        // 2. Aplicar movimiento físico dentro de FixedUpdate
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (cameraFollowTarget == null) return;

        // Obtener direcciones de la cámara proyectadas en el plano horizontal (Eje Y = 0)
        Vector3 camForward = Vector3.Scale(cameraFollowTarget.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(cameraFollowTarget.right, new Vector3(1, 0, 1)).normalized;

        // Dirección de movimiento calculada en base a la vista de la cámara
        Vector3 moveDir = (camForward * inputVector.y + camRight * inputVector.x).normalized;

        float targetSpeed = isRunning ? runSpeed : walkSpeed;

        // Aplicar la velocidad al Rigidbody (respetando la velocidad vertical del salto/gravedad)
        Vector3 velocity = moveDir * targetSpeed;
        
        // Usar linearVelocity en Unity reciente o velocity en versiones anteriores
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
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
        // OnCollisionStay es más estable que OnCollisionEnter para detectar el suelo
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

