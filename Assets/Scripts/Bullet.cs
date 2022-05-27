using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float movementSpeed = 100;
    Transform canon;
    Vector3 direction;
    public bool collided = false;
    public float maxDistance = 2000f;
    public float beginDelay = 1;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePosition;
        canon = FindObjectOfType<Gun>().canon.transform;
        direction = canon.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (beginDelay > 0)
        {
            beginDelay -= Time.deltaTime;
            return;
        }
        rb.constraints = RigidbodyConstraints.None;
        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Weapon" && collision.gameObject.tag != "Player")
        {
            collided = true;
        }

    }

}
