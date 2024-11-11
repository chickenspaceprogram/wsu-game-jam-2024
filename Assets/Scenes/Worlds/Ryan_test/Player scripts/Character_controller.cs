using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character_controller : MonoBehaviour
{
    public int WALK_SPEED = 5;
    public int SPRINT_SPEED = 10;
    public double MAX_STAMINA = 10;
    public static float GRAVITY = 9.8f;

    public double stamina;
    private int playerSpeed = 0;
    private bool isRunning = false;

    private Rigidbody player_rb;

    public Camera player_cam;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    float rotationX = 0;

    // Start is called before the first frame update
    void Start()
    {
        stamina = MAX_STAMINA;

        player_rb = GetComponent<Rigidbody>();

        player_cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleRunning();
        ControlCamera();

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        if (Input.GetKey(KeyCode.R))
        {
            stamina = MAX_STAMINA;
        }

        //Debug.Log(stamina);

        Vector3 vel = playerSpeed * ((Input.GetAxis("Horizontal") * right) + (Input.GetAxis("Vertical") * forward));
        //vel.y = -1;
        //vel.y *= GRAVITY;
        //vel.y *= GRAVITY;

        vel.y -= GRAVITY * GRAVITY * Time.deltaTime;

        //Debug.Log(right);
        //vel.y /= playerSpeed;

        player_rb.velocity = vel;

        
    }

    void HandleRunning() {
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }


        if (isRunning)
        {
            playerSpeed = SPRINT_SPEED;
            stamina -= 1 * Time.deltaTime;
            player_cam.fieldOfView = Mathf.MoveTowards(player_cam.fieldOfView, 70, 100f * Time.deltaTime);
        }
        else
        {
            playerSpeed = WALK_SPEED;
            player_cam.fieldOfView = Mathf.MoveTowards(player_cam.fieldOfView, 60, 100f * Time.deltaTime);
        }

    }


    void ControlCamera()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        //Debug.Log(rotationX);
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        player_cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

    }
}
