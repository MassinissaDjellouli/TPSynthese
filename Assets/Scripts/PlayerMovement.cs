
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float horizontal;
    float vertical;
    Vector3 direction;
    public Transform body;
    public float movementSpeed = 10;
    // Start is called before the first frame update
    void Start()
    { 
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        direction = body.forward * vertical + body.right * horizontal;

        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }
}
