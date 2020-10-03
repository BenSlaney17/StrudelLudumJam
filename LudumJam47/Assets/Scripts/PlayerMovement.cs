using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] [Range(1f, 25f)] private float movementSpeed = 8f;
    [SerializeField] private float mouseSmoothness = 5.0f;

    [Header("Control Settings")]
    [Space(10)]
    [SerializeField] [Range(-90f, -20f)] private float minVerticalRotation = -85f;
    [SerializeField] [Range(20f, 90f)] private float maxVerticalRotation = 85f;
    [HideInInspector] public bool canMove = true;

    [Header("Sound Settings")]
    [Space(10)]
    [SerializeField] private AudioSource footstepAudioSource = null;
    [SerializeField] private AudioClip footstepSound = null;
    [Space(5)]

    private float movementX;
    private float movementY;
    private Vector3 moveHorizontal;
    private Vector3 moveVertical;
    private float yRotation;
    private float xRotation;
    private Vector3 rotation;
    private float distanceToGround;
    private Camera cam;
    CharacterController characterController;
    Animator playerAnimator;

    public bool isWalking { get; private set; }

    private Vector2 storedLookRotation;

    private void Awake()
    {
        //Initialize variables
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        cam = this.GetComponentInChildren<Camera>();
        distanceToGround = this.GetComponent<Collider>().bounds.extents.y;
    }

    void Start()
    {
        storedLookRotation = new Vector2(0, 0);
        LockCursor();
    }

    void Update()
    {
        KeyboardInput();

        if (IsGrounded())
            return;

        float gravity = -4 * Time.deltaTime;
        characterController.Move(new Vector3(0, gravity, 0));
    }

    private void LateUpdate()
    {
        // Rotate the player ONLY if there is any input happening
        if (rotation != Vector3.zero)
        {
            //Rotate the camera of the player
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.rotation.x, yRotation, transform.rotation.z), (50 / mouseSmoothness) * Time.deltaTime);
        }

        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, Quaternion.Euler(-xRotation, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z), (50 / mouseSmoothness * 40) * Time.deltaTime);
    }

   

    void KeyboardInput()
    {
        if (!canMove)
        {
            SetHeadBob(false);
            return;
        }

        isWalking = true;

        // Store the values of the horizontal and vertical axes in these variables
        movementX = Input.GetAxis("Horizontal") * movementSpeed;
        movementY = Input.GetAxis("Vertical") * movementSpeed;

        // The Vector3 values representing the horizontal and vertical movement are calculated here.
        moveHorizontal = transform.right * movementX;
        moveVertical = transform.forward * movementY;

        // The player gameobject rotates based on the x axis of the mouse, NOT THE CAMERA
        yRotation += Input.GetAxis("Mouse X");
        rotation = new Vector3(0, yRotation, 0);

        // The camera rotates based on the y axis of the mouse
        xRotation += Input.GetAxis("Mouse Y");

        // Clamps rotation so the plaer can only look up and down so far
        xRotation = Mathf.Clamp(xRotation, minVerticalRotation, maxVerticalRotation);

        if (movementX != 0 || movementY != 0)
        {
            characterController.Move(moveHorizontal * Time.deltaTime);
            characterController.Move(moveVertical * Time.deltaTime);

            SetHeadBob(true);
           // AudioManager.audioManager.PlayFootstep();
        }
        else
        {
            SetHeadBob(false);
            isWalking = false;
        }
    }

    public void SetHeadBob(bool value){ playerAnimator.SetBool("Walking", value); }
    public void SetCanMove(bool value) { canMove = value; }
    bool IsGrounded() { return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f); }
    public void StoreLookRotation() { storedLookRotation.x = movementX; storedLookRotation.y = movementY; }
    public void RestoreLookRotation() { movementX = storedLookRotation.x; movementY = storedLookRotation.y; }
    public void LockCursor() { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }
    public void UnlockCursor() { Cursor.lockState = CursorLockMode.None; Cursor.visible = true; }
}