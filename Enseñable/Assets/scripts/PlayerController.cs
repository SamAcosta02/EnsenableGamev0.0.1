using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public PlayerInput control;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Camera cam;
    public PlayerControlz playerControls;

    [Header("Initial Abilities")]
    public bool jump = true;
    public bool crouch = true;
    public bool run = true;
    [Space(5)]
    [Header("Unlockable Abilities")]
    public bool doubleJump;
    public bool airDive;
    public bool groundSlide;
    public bool groundPound;
    public bool airDash;
    [Space(15)]

    [Header("Magnitud Paramaters")]
    public float walkSpeed;
    public float airSpeedMultiplier;
    public float jumpForce;
    public float fallMultiplier;
    public float groundDrag;
    public float airDrag;
    public float airDashForce;
    public float runBonus;
    public float normalHeight;
    public float crouchHeight;
    public float slideHeight;
    public float crouchJumpBonus;
    public float diveForce;
    public float groundPoundSpeed;

    [Header("Initial Cunatities")]
    public float bonusJumps;
    public float slideTime;
    public float dashesAllowed;
    public float divesAllowed;


    [Header("Runtime Cuantities")]
    public Vector2 moveInput;
    public Vector3 moveDir;
    public Vector3 forwardDir;
    public Vector3 rightDir;
    public bool isGrounded;
    private bool wasGrounded;
    public bool isCrouched;
    public bool isGroundpounding;
    public bool isRunning;
    public bool isSliding;
    public bool isDiving;
    public float currentSlide;
    public float jumpsLeft;
    public float divesLeft;
    public float dashesLeft;

    private void Awake()
    {
        playerControls = new PlayerControlz();
    }

    private void Start()
    {
        becameGrounded();
    }

    private void FixedUpdate()
    {
        if (isGroundpounding)
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0f, -groundPoundSpeed, 0f);
        } else if (isSliding)
        {
            currentSlide += Time.deltaTime;
            if (currentSlide > slideTime)
            {
                exitSlideState();
            }
        } else if (isDiving)
        {
            //no player control
        }
        else
        {
            if (isGrounded)
            {
                if (isRunning)
                {
                    this.GetComponent<Rigidbody>().AddForce(moveDir * walkSpeed * runBonus, ForceMode.Acceleration);
                }
                else
                {
                    this.GetComponent<Rigidbody>().AddForce(moveDir * walkSpeed, ForceMode.Acceleration);
                }
            }
            else
            {
                this.GetComponent<Rigidbody>().AddForce(moveDir * walkSpeed * airSpeedMultiplier, ForceMode.Acceleration);
                this.GetComponent<Rigidbody>().velocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.deltaTime;
            }
        }
    }

    private void Update()
    {
        //Check for grounded change state
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);
        if (isGrounded != wasGrounded)
        {
            if (isGrounded)
            {
                becameGrounded();
            }
            else
            {
                becameAirborne();
            }
        }
        wasGrounded = isGrounded;

        //relative forward and right vector to camera
        forwardDir = this.transform.position - cam.transform.position;
        forwardDir.y = 0;
        forwardDir.Normalize();
        rightDir = new Vector3(forwardDir.z, 0f, -forwardDir.x);

        //read input
        moveInput = playerControls.Land.Movement.ReadValue<Vector2>();
        if (moveInput == Vector2.zero)
        {
            isRunning = false;
        }
        moveDir = forwardDir * moveInput.y + rightDir * moveInput.x;

        //Debug.DrawRay(this.transform.position, forwardDir * 3f, Color.red);
        //Debug.DrawRay(this.transform.position, rightDir * 3f, Color.blue);
        //Debug.DrawRay(this.transform.position, moveDir * 3f, Color.yellow);
    }

    public void OnMovement()
    {

    }

    //KEY 1 BEHAVIOUR
    public void OnJump()
    {
        //grond jump or air jump [enabled]
        if ((isGrounded || (doubleJump && jumpsLeft > 0)) && jump) 
        {
            JumpBehaviour();
        } else
        {
            if (divesLeft > 0 && airDive)
            {
                divesLeft--;
                enterDiveState();
            }
        }
    }

    //KEY 2 BEHAVIOUR
    public void OnCrouch()
    {
        if (isGrounded)
        {
            if (isRunning && groundSlide)
            {
                enterSlideState();
            }
            else
            {
                if (!isCrouched && crouch)
                {
                    enterCrouchState();
                }
                else
                {
                    exitCrouchState();
                }
            }

        } else if (!isGrounded && groundPound)
        {
            isGroundpounding = true;
            enterCrouchState();
        }
    }

    //KEY 3 BEHAVIOUR
    public void OnSpecial()
    {
        if (isGrounded && run)
        {
            isRunning = true;
        } else if (!isGrounded && airDash && dashesLeft>0 && !isDiving)
        {
            dashesLeft--;
            this.GetComponent<Rigidbody>().AddForce(moveDir * airDashForce, ForceMode.Impulse);
        }

    }

    //Start of behaviour methods

    public void enterCrouchState()
    {
        isCrouched = true;
        this.transform.localScale = new Vector3(normalHeight, normalHeight * crouchHeight, normalHeight);
    }

    public void exitCrouchState()
    {
        isCrouched = false;
        this.transform.localScale = new Vector3(normalHeight, normalHeight, normalHeight);
    }

    public void enterSlideState()
    {
        isSliding = true;
        this.GetComponent<Rigidbody>().drag = 0f;
        this.transform.localScale = new Vector3(normalHeight, normalHeight * slideHeight, normalHeight);
    }

    public void exitSlideState()
    {
        isSliding = false;
        currentSlide = 0f;
        this.GetComponent<Rigidbody>().drag = groundDrag;
        this.transform.localScale = new Vector3(normalHeight, normalHeight, normalHeight);
    }

    public void enterDiveState()
    {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(this.GetComponent<Rigidbody>().velocity.x, -1f, this.GetComponent<Rigidbody>().velocity.z) * diveForce, ForceMode.Impulse);
            isDiving = true;
            this.transform.localScale = new Vector3(normalHeight, normalHeight * slideHeight, normalHeight);
    }

    public void exitDiveState()
    {
        isDiving = false;
        divesLeft = divesAllowed;
        this.transform.localScale = new Vector3(normalHeight, normalHeight, normalHeight);
    }


    public void JumpBehaviour()
    {
        //jump
        if (!isGrounded)
        {
            //waste air jump
            jumpsLeft--;
        }

        if (isCrouched)
        {
            this.GetComponent<Rigidbody>().AddForce(this.transform.up * jumpForce * crouchJumpBonus, ForceMode.Impulse);
        } else
        {
            this.GetComponent<Rigidbody>().AddForce(this.transform.up * jumpForce, ForceMode.Impulse);
        }
    }


    public void becameGrounded()
    {
        //this.transform.localScale = new Vector3(normalHeight, normalHeight, normalHeight);
        jumpsLeft = bonusJumps;
        dashesLeft = dashesAllowed;
        divesLeft = divesAllowed;
        isGrounded = true;
        if (!isSliding)
        {
            this.GetComponent<Rigidbody>().drag = groundDrag;
        }
        if (isGroundpounding)
        {
            isGroundpounding = false;
            exitCrouchState();
        }
        if (isDiving)
        {
            isDiving = false;
            exitSlideState();
        }
    }

    public void becameAirborne()
    {
        if (!isSliding)
        {
            this.GetComponent<Rigidbody>().drag = airDrag;
        }
        isGrounded = false;
    }

    private void OnEnable()
    {
        playerControls.Land.Enable();
    }

    private void OnDisable()
    {
        
    }
}
