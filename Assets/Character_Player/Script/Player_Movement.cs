using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Movement : MonoBehaviour
{
    Rigidbody rb;
    Animator ani;

    [SerializeField] float speed;
    float vertical;
    float horizontal;
    float Rotationspeed = 500;
    Quaternion TargetRotation;

    Camera_Movement CamController;
    CharacterController characterController;

    public Camera_Movement cam;
    Vector3 Run_cam = new Vector3(-0.7f, -1.25f, 1.5f);
    Vector3 Reset_cam = new Vector3(-1f, -1.25f, 2f);

    void Awake()
    {
        rb= GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        CamController = Camera.main.GetComponent<Camera_Movement>();
        characterController =GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        
        var movingMagnitude = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        var movement = new Vector3(horizontal, 0, vertical).normalized;

        var MoveDir = CamController.PlanarRotation * movement;

        if (movingMagnitude > 0)
        {
            characterController.Move(MoveDir * speed * Time.deltaTime);
            TargetRotation = Quaternion.LookRotation(MoveDir);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && movingMagnitude > 0)
        {
            speed = 10;
            ani.SetBool("Gun_Running", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5;
            ani.SetBool("Gun_Running", false);
        }


        transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, Rotationspeed * Time.deltaTime);

        ani.SetFloat("Run", movingMagnitude, 0.2f, Time.deltaTime);
    }
}
