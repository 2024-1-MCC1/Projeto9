using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public static float walkingSpeed = 1f;
    public static float runningSpeed = 1.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    private AudioSource Passos;

	public static bool BLOQUEIACORRIDA = true;
    CharacterController characterController;

    Vector3 moveDirection = Vector3.zero;

    float rotationX = 0;
	private bool isRunning;

    [HideInInspector]
    public static bool canMove = true;

    public static Animator animator;

    public static bool MODOCARREIRA = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GameObject.Find("Scavenger Variant").GetComponent<Animator>();
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Passos = GameObject.Find("Passos").GetComponent<AudioSource>();
    }

    void Update()
    {
        
        
        if (MODOCARREIRA == false)
        {
            walkingSpeed = 0.5f * (PROPRIEDADES_JOGADOR.energia / 200) + 0.5f - (PROPRIEDADES_JOGADOR.PESOATUAL/(PROPRIEDADES_JOGADOR.constPeso + PROPRIEDADES_JOGADOR.PESOLIMITE)) * 0.2f;
            runningSpeed = walkingSpeed * 1.5f;

            // We are grounded, so recalculate move direction based on axes

            Vector3 forward = transform.TransformDirection(Vector3.forward);

            Vector3 right = transform.TransformDirection(Vector3.right);

            // Press Left Shift to run

            if ((BLOQUEIACORRIDA == true || ORQUESTRA.COMECO == false) && PROPRIEDADES_JOGADOR.energia > 0)
            {
                isRunning = Input.GetKey(KeyCode.LeftShift);
            }


            float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;

            float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;

            float movementDirectionY = moveDirection.y;

            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (isRunning)
            {
                PROPRIEDADES_JOGADOR.energia -= Time.deltaTime * runningSpeed * 0.5f;
            }

            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (canMove)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                if (Input.GetAxis("Vertical") == 1)
                {
                    animator.SetInteger("andando", 1);
                }
                else if (Input.GetAxis("Vertical") == -1)
                {
                    animator.SetInteger("andando", 2);
                }
                else
                {
                    animator.SetInteger("andando", 0);
                }

            }
            animator.speed = isRunning ? runningSpeed : walkingSpeed;

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            // Move the controller

            characterController.Move(moveDirection * Time.deltaTime);  //-----------------CÓDIGO QUE MOVE O OBJETO JOGADOR--------------------------
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

		//Som passos do jogador
            if ((Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0 || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Horizontal") > 0) && Passos.isPlaying == false && characterController.isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
               Passos.pitch= 2.2f;
               Passos.Play(0);
            }
            else
            {
                Passos.pitch = 1f;
               Passos.Play(0);
            }
        }
        else
        {
            Passos.Pause();
        }
        }

    }
}