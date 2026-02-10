using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    private Vector2 moveInput;
    private float moveAnimationInputX;
    private float moveAnimationInputY;
    private Vector3 velocity;
    public float speed;
    public float runSpeed;
    public float gravity;
    public float jumpHeight;
    public PlayerCam playerCam;
    public bool canWalk;
    float walkResetTimer;
    bool walkResetTimerGo;
    bool running;


    public InputAction pauseGameButton;


    public Transform orientation;
    public Transform cameraor;

    public GameObject cinemachineCam;

    float horizontalInput;
    float verticalInput;

    public GameObject sword;
    public Transform swordSpawnPoint;
    public Transform swordSpawnPoint2;
    public Transform swordSpawnPoint3;
    public int lastSwing;
    public float timeSinceLastSwing;
    public bool holdSwing;
    public float holdSwingTimer;
    public bool swingBeingHeld;

    public float dashDistanceTimer, dashDistance, dashDuration;
    public Collider myCollider;
    public LayerMask excludeEnemy;

    public float HP = 10;

    public int inventorySlot = 1;

    public float energy = 100;
    public float swungTimer;
    public bool swung;
    public Slider energySlider;
    public GameObject energyObj;
    float energyObjTimer;
    public GameObject energyFill;

    public bool jumped;

    

    

    

    public RectTransform healthUI;
    

    public GameObject uiController;

    public PlayerInput playerInput;

    InputAction jumpAction, walkAction, attackAction, pauseAction, runAction, lookAction;

    private void Awake()
    {
        
    }

    void Start()
    {
        cinemachineCam = GameObject.Find("CinemachineCamera");
        canWalk = true;

        jumpAction = playerInput.actions["Jump"];
        walkAction = playerInput.actions["Walk"];
        attackAction = playerInput.actions["Attack"];
        pauseAction = playerInput.actions["Pause"];
        runAction = playerInput.actions["Run"];
        lookAction = playerInput.actions["Look"];
    }
    void PlayerInputs()
    {
        if (pauseAction.WasPressedThisFrame() && Time.timeScale == 1)
        {
            uiController.GetComponent<PauseMenuControls>().mainCanvas.SetActive(false);
            uiController.GetComponent<PauseMenuControls>().pauseCanvas.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (pauseAction.WasPressedThisFrame())
        {
            uiController.GetComponent<PauseMenuControls>().mainCanvas.SetActive(true);
            uiController.GetComponent<PauseMenuControls>().pauseCanvas.SetActive(false);
            uiController.GetComponent<PauseMenuControls>().settingsCanvas.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        moveInput = walkAction.ReadValue<Vector2>();

        if (runAction.WasPressedThisFrame() && moveAnimationInputY == 1 && !running)
        {
            running = true;
        }
        else if (runAction.WasPressedThisFrame() && moveAnimationInputY == 2 && running)
        {
            running = false;
            moveAnimationInputY = 1;
        }

        playerCam.lookInput = lookAction.ReadValue<Vector2>();

        if (attackAction.WasPressedThisFrame() && inventorySlot == 1)
        {
            swingBeingHeld = true;
            if (GameObject.FindGameObjectsWithTag("Sword").Length < 1)
            {
                if (energy > 7)
                    SwingSword();
            }
            else
            {
                swung = true;
                swungTimer = 0;
                holdSwingTimer = 0;
                holdSwing = true;
            }
        }
        if (attackAction.WasReleasedThisFrame() && inventorySlot == 1)
        {
            swingBeingHeld = false;
            energyFill.GetComponent<Image>().color = Color.darkRed;
            if (dashDistanceTimer > 1)
            {
                dashDistance = Mathf.Floor(dashDistanceTimer * 8);
                dashDuration = 15;
            }
            dashDistanceTimer = 0;
        }

        if (jumpAction.WasPressedThisFrame() && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
            jumped = true;
        }
        if (jumpAction.WasReleasedThisFrame() && !controller.isGrounded)
        {
            jumped = false;
        }
    }
    void Update()
    {
        PlayerInputs();

        timeSinceLastSwing += Time.deltaTime;
        if (timeSinceLastSwing > .5f)
            lastSwing = 0;

        holdSwingTimer += Time.deltaTime;
        if (holdSwing && holdSwingTimer > .15f)
            holdSwing = false;
        else if (holdSwing && GameObject.FindGameObjectsWithTag("Sword").Length < 1)
        {
            holdSwing = false;
            if (energy > 7)
                SwingSword();
        }
        if (!holdSwing)
            holdSwingTimer = 0;

        if (swung)
        {
            swungTimer += Time.deltaTime;
        }
        if (swungTimer > 1f)
            swung = false;
        if (!swung)
        {
            swungTimer = 0;
            energy += 70 * Time.deltaTime;

        }
        if (energy < 0)
            energy = 0;
        if (energy > 100)
            energy = 100;

        energySlider.value = energy;

        healthUI.sizeDelta = new Vector2((Mathf.Ceil(HP) - 1) + (9 * HP), healthUI.sizeDelta.y);

        //MyInput();
        if (swingBeingHeld)
        {
            swung = true;
            swungTimer = 0;
            timeSinceLastSwing = 0;

            energy -= 20 * Time.deltaTime;
            dashDistanceTimer += Time.deltaTime;
            if (dashDistanceTimer > 1)
                energyFill.GetComponent<Image>().color = Color.deepPink;
            if (energy <= 0)
            {
                if (dashDistanceTimer > 1)
                {
                    dashDistance = Mathf.Floor(dashDistanceTimer * 8);
                    dashDuration = 15;
                }
                dashDistanceTimer = 0;
            }
        }

        if (energy == 100)
        {
            energyObjTimer += Time.deltaTime;
            if (energyObjTimer > .4)
            {
                energyObj.SetActive(false);
            }

        }
        else
        {
            energyObjTimer = 0;
            energyObj.SetActive(true);
        }



        if (dashDuration > 0)
        {
            SwordDash();

            dashDuration--;
        }
        if (dashDuration == 1)
        {
            //rb.linearVelocity = Vector3.zero;

            dashDuration--;
        }



        
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f;
            
        }
        //else if (controller.isGrounded && !jumpAction.WasPerformedThisFrame())
        //{
        //    velocity.y = 0;
        //}
        if (jumpAction.WasPerformedThisFrame() && controller.isGrounded)
        {
            //Debug.Log("jumped");
            //velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        //Debug.Log(controller.isGrounded);

        if (canWalk /*&& !running*/)
        {
            Vector3 move = new Vector3(moveInput.x * speed, 0, moveInput.y * speed);
            Vector3 move2 = transform.TransformDirection(move);
            controller.Move(move2 * Time.deltaTime);
        }
        //else if (canWalk && running)
        //{
        //    Vector3 move = new Vector3(moveInput.x * speed, 0, moveInput.y * runSpeed);
        //    Vector3 move2 = transform.TransformDirection(move);
        //    controller.Move(move2 * Time.deltaTime);
        //}
        controller.Move(velocity * Time.deltaTime);
    }
    private void MyInput()
    {
        
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            

        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        



        if (Time.timeScale == 1)
        {
            timeSinceLastSwing += Time.deltaTime;
            if (timeSinceLastSwing > .5f)
                lastSwing = 0;

            holdSwingTimer += Time.deltaTime;
            if (holdSwing && holdSwingTimer > .15f)
                holdSwing = false;
            else if (holdSwing && GameObject.FindGameObjectsWithTag("Sword").Length < 1)
            {
                holdSwing = false;
                if (energy > 7)
                    SwingSword();
            }
            if (!holdSwing)
                holdSwingTimer = 0;

            if (swung)
            {
                swungTimer += Time.deltaTime;
            }
            if (swungTimer > 1f)
                swung = false;
            if (!swung)
            {
                swungTimer = 0;
                energy += 70 * Time.deltaTime;

            }
            if (energy < 0)
                energy = 0;
            if (energy > 100)
                energy = 100;

            energySlider.value = energy;

            healthUI.sizeDelta = new Vector2((Mathf.Ceil(HP)-1) + (9 * HP), healthUI.sizeDelta.y);

            if (inventorySlot == 1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (GameObject.FindGameObjectsWithTag("Sword").Length < 1)
                    {
                        if (energy > 7)
                            SwingSword();
                    }
                    else
                    {
                        swung = true;
                        swungTimer = 0;
                        holdSwingTimer = 0;
                        holdSwing = true;
                    }
                }
                if (Input.GetMouseButton(0))
                {
                    swung = true;
                    swungTimer = 0;
                    timeSinceLastSwing = 0;

                    energy -= 20 * Time.deltaTime;
                    dashDistanceTimer += Time.deltaTime;
                    if (dashDistanceTimer > 1)
                        energyFill.GetComponent<Image>().color = Color.deepPink;
                    if (energy <= 0)
                    {
                        if (dashDistanceTimer > 1)
                        {
                            dashDistance = Mathf.Floor(dashDistanceTimer * 8);
                            dashDuration = 15;
                        }
                        dashDistanceTimer = 0;
                    }
                }
                //if (!Input.GetMouseButton(0))
                //energyFill.GetComponent<Image>().color = new Color(196, 3, 0, 255);
                if (Input.GetMouseButtonUp(0))
                {
                    energyFill.GetComponent<Image>().color = Color.darkRed;
                    if (dashDistanceTimer > 1)
                    {
                        dashDistance = Mathf.Floor(dashDistanceTimer * 8);
                        dashDuration = 15;
                    }
                    dashDistanceTimer = 0;
                }
            }

            if (inventorySlot == 2)
            {

            }

            if (inventorySlot == 3)
            {

            }
        }
    }
    private void SwingSword()
    {
        swung = true;
        swungTimer = 0;
        energy -= 7;
        if (lastSwing == 0 || lastSwing == 3)
        {
            GameObject newSword = Instantiate(sword, swordSpawnPoint);
            lastSwing = 1;
            timeSinceLastSwing = 0;
        }
        else if (lastSwing == 1)
        {
            GameObject newSword = Instantiate(sword, swordSpawnPoint2);
            lastSwing = 2;
            timeSinceLastSwing = 0;
        }
        else if (lastSwing == 2)
        {
            GameObject newSword = Instantiate(sword, swordSpawnPoint3);
            lastSwing = 3;
            timeSinceLastSwing = 0;
        }
    }

    private void MovePlayer()
    {
       

        if (dashDuration > 0)
        {
            SwordDash();

            dashDuration--;
        }
        if (dashDuration == 1)
        {
            //rb.linearVelocity = Vector3.zero;

            dashDuration--;
        }
    }
    private void SwordDash()
    {
        myCollider.excludeLayers = excludeEnemy;
        //Debug.Log($"distance:{dashDistance}");
        //rb.AddForce(Camera.main.transform.forward * dashDistance, ForceMode.Impulse);
        controller.Move(Camera.main.transform.forward * dashDistance / 15);
        swung = true;
        swungTimer = 0;
        timeSinceLastSwing = 0;
    }
    
    
}
