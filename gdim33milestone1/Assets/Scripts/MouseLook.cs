using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 15f;
    public Transform playerBody;

    private float xRotation = 0f;
    private float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
#if UNITY_WEBGL
        mouseSensitivity = mouseSensitivity / 2.0f;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-80f,80f);

        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);

        playerBody.rotation = Quaternion.Euler(0f,yRotation,0f);
        
    }
}
