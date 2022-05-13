using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float movementSpeed = 100;
    Transform canon;
    Vector3 direction;
    PlayerActions player;
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
        player = FindObjectOfType<PlayerActions>();
    }

    // Update is called once per frame
    void Update()
    {
        if(beginDelay > 0)
        {
            beginDelay -= Time.deltaTime;
            return;
        }
        rb.constraints = RigidbodyConstraints.None;
        transform.Translate(direction * movementSpeed * Time.deltaTime);
        if(getPlayerDistance() > maxDistance)
        {
            Debug.Log(getPlayerDistance());
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Weapon" && collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
    private float getPlayerDistance()
    {
        Vector3 dist = transform.position - player.transform.position;
        return dist.magnitude;
    }
}
