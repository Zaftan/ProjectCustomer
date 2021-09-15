using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Transform groundCheck;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;

    [Header("Ground Detection Settings")]
    [SerializeField] private bool canJump = true;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;

    //velocity
    private Vector3 velocity;

    //interaction
    [Header("Dialogue UI Reference")]
    [SerializeField] private DialogueUI dialogueController;
    public DialogueUI dialogueUI => dialogueController;

    public IInteractable interactable { get; set; }

    //animation
    [Header("Animation Reference")]
    [SerializeField] private Animator animator;
    private Vector3 lastPos;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        groundCheck = transform.Find("GroundCheck");
        lastPos = transform.position;
    }

    private void Update()
    {
        //talking? no moveing!
        if (!dialogueUI.isOpen)
        {
            //ground check    Brynn TODO: potentially convert this part to ontriggerenter and ontriggerexit
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            MoveHorizontal();
            MoveVertical();

            //interaction
            InteractWithObject();
        }
        //update animation param
        if (animator != null)
        {
            float combinedSpeed = (Mathf.Abs(lastPos.x - transform.position.x) + Mathf.Abs(lastPos.z - transform.position.z)) * 10;
            animator.SetFloat("walkSpeed", combinedSpeed);
            lastPos = transform.position;
        }
    }

    private void MoveHorizontal()
    {
        //get inputs
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //calc vector
        Vector3 playerDir = transform.right * horizontal + transform.forward * vertical;
        //apply movement
        characterController.Move(playerDir * speed * Time.deltaTime);
    }
    private void MoveVertical()
    {
        //slightly push down to adjust for slopes etc.
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        //vertical velocity update
        if (canJump) Jump();
        velocity.y += gravity * Time.deltaTime;
        //apply new movement
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    { //physics jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void InteractWithObject()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactable?.Interact(this);
        }
    }
}