using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Dictionary<string, int> valuePairs = new Dictionary<string, int>()
    {
        {"First" , 1},
        {"Second", 2 },
        {"Third", 3}
    };
    private float rotX, rotY;
    public float horzSensitvity, vertSensitivity;
    public float clampAngle = 90;
    private CharacterController myCC;
    private Skills mySkills;
    private float walkSpeed = 2f, runSpeed = 3f;
    private float stamina = 100;
    private float staminaUse;
    private bool isRunning;
    private bool isMoving;
    private bool jump;
    private float verticalVelocity;
    private float gravity = 9.81f;
    private float jumpForce = 10f;

    private bool inventoryOpen;
    

    void Awake()
    {
        GameObject.FindObjectOfType<InventoryInput>().InvOpen += Movement_InvOpen;
        mySkills = GetComponent<Skills>();
        myCC = GetComponent<CharacterController>();
    }

    private void Movement_InvOpen(bool obj)
    {
        inventoryOpen = obj;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        int temp = 0;
        valuePairs.Add("Fourth", 4);
        Debug.Log(valuePairs.ContainsKey("Second"));// Returns if exists in dictionary
        Debug.Log(valuePairs.TryGetValue("First", out temp));//Will output value of "First" to temp
        Debug.Log(temp);
        */
        UpdateMoveSpeed();
        Cursor.lockState = CursorLockMode.Locked;
        jump = false;
        isRunning = false;
        isMoving = false;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!inventoryOpen)
        {
            PlayerView();
            MovePlayer();

        }
        CheckForGround();

    }

    void PlayerView()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");
        
        rotX += mouseX * horzSensitvity * Time.deltaTime;
        rotY += mouseY * vertSensitivity * Time.deltaTime; //Gets Sensitivity For Player View relitive to real time.

        rotY = Mathf.Clamp(rotY, -clampAngle, clampAngle);//Stops Player Vertical from Flipping

        Quaternion localRotation = Quaternion.Euler(rotY, rotX, 0.0f);
        Quaternion playerRotation = Quaternion.Euler(0.0f, rotX, 0.0f);

        transform.rotation = playerRotation;
        Camera.main.transform.rotation = localRotation;
    }

    public void UpdateMoveSpeed()
    {
        jumpForce = 4f + (mySkills.AgilityLevel / 3);
        walkSpeed = 3f + (mySkills.AgilityLevel / 3);
        runSpeed = 5f + (mySkills.AgilityLevel / 2);
    }

    void MovePlayer()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            if (Input.GetKey(KeyCode.W))
            {
                if((Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightShift)) && stamina > 0)
                {
                    myCC.Move(new Vector3(transform.forward.x, 0.0f, transform.forward.z) * Time.deltaTime * runSpeed);
                    stamina -= staminaUse;
                    if (mySkills)
                    {
                        mySkills.AgilityXP = 2.5f * Time.deltaTime;
                    }
                    isRunning = true;
                }else
                {
                    myCC.Move(new Vector3(transform.forward.x, 0.0f, transform.forward.z) * Time.deltaTime * walkSpeed);
                    isRunning = false;
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                myCC.Move(-new Vector3(transform.forward.x, 0.0f, transform.forward.z) * Time.deltaTime * walkSpeed);
            }
            if (Input.GetKey(KeyCode.A))
            {
                myCC.Move(-new Vector3(transform.right.x, 0.0f, transform.right.z) * Time.deltaTime * walkSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                myCC.Move(new Vector3(transform.right.x, 0.0f, transform.right.z) * Time.deltaTime * walkSpeed);
            }

        }
        else// if the player is not running and isRunning is true it will allow for Stamina Regen to start.
        {
            isRunning = false;
            isMoving = false;
        }
    }

    private void CheckForGround()
    {
        RaycastHit hit;

        Debug.DrawRay(gameObject.transform.position, Vector3.down, Color.red, 1.5f);

        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out hit, 1.15f))
        {
            //Debug.Log("Grounded");
            verticalVelocity = -gravity * Time.deltaTime;

            if (jump)
            {
                verticalVelocity = jumpForce;
                if (mySkills)
                {
                    mySkills.AgilityXP = 50f;
                }
            }

        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 verticalMove = new Vector3(0, verticalVelocity, 0);
        myCC.Move(verticalMove * Time.deltaTime);
    }


}


