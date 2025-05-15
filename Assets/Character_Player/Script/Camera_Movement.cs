using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    [SerializeField] GameObject Player;
    public Vector3 offset;

    float rotationspeed = 2;
    float rotationx;
    float rotationy;
    float miny = -20;
    float maxy = 45;

    private void Start()
    {
        Cursor.visible= false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        rotationx -= Input.GetAxis("Mouse Y") * rotationspeed;
        rotationx = Mathf.Clamp(rotationx, miny, maxy);

        rotationy += Input.GetAxis("Mouse X") * rotationspeed;
  
        var targetRotation = Quaternion.Euler(rotationx, rotationy, 0);

        transform.position = Player.transform.position - targetRotation * offset;

        transform.rotation = targetRotation;     
    }
    
    public Quaternion PlanarRotation => Quaternion.Euler(0, rotationy, 0);

}
