using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float movementSpeed = 3;
    public float mouseSensitivity = 300;
    public float playerHeight = 3.5f;
    float currentXRot = 0;
    float currentYRot = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Lock le curseur en place
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        HeadRotation();
        Move();
    }

    void Move()
    {
        float mouvementX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float mouvementZ = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        transform.Translate(Vector3.forward * mouvementZ);
        transform.Translate(Vector3.right * mouvementX);

        transform.position = new Vector3(transform.position.x, playerHeight, transform.position.z);
    }
    void HeadRotation()
    {
        if (currentYRot > 360) currentYRot -= 360;
        if (currentYRot < -360) currentYRot += 360;
        float sourisX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float sourisY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        currentXRot -= sourisY;
        //Permet de mettre les limites de la rotation
        currentXRot = Mathf.Clamp(currentXRot, -90f, 90f);
        //localrotation: rotation de l'objet en relation avec son parent

        currentYRot += sourisX;
        Debug.Log(currentYRot);
        transform.localRotation = Quaternion.Euler(currentXRot, currentYRot, 0);
    }
}
