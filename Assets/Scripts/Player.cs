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
    public GameObject gunAsset;
    public Camera cam;
    Gun gun;
    // Start is called before the first frame update
    void Start()
    {
        //Lock le curseur en place
        Cursor.lockState = CursorLockMode.Locked;
        GameObject child = Instantiate(gunAsset,cam.transform.position + new Vector3(1,-1,1.7f),Quaternion.identity);
        child.transform.parent = cam.transform;

        gun = child.GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        HeadRotation();
        Move();
        ShootGun();
    }

    void ShootGun()
    {
        if (Input.GetMouseButton(0))
        {
            gun.Shoot();
        }
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
        transform.localRotation = Quaternion.Euler(0, currentYRot, 0);
        cam.transform.localRotation = Quaternion.Euler(currentXRot, 0, 0);
    }
}
