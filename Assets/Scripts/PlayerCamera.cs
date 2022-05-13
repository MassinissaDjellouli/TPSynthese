using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float mouseSensitivity = 300f;
    float currentXRot = 0;
    float currentYRot = 0;

    public Transform body;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float sourisX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float sourisY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        currentYRot += sourisX;
        currentXRot -= sourisY;
        currentXRot = Mathf.Clamp(currentXRot, -90f, 90f);

        transform.rotation = Quaternion.Euler(currentXRot, currentYRot, 0f);
        body.rotation = Quaternion.Euler(0, currentYRot, 0f);
        
        body.Rotate(Vector3.up * sourisX);
    }

}
