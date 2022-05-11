using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{

    private Animator anim;
    private GameObject player = null;
    private bool isHitting = false;


    public int zombieHp = 6;
    public float movementSpeed = 10f;
    public float speed = 1.0f;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (isHitting)
        {
            Debug.Log("Zombie is hitting the player");
            stopAllAnimation();
            anim.SetBool("Attack", true);
        }
        else {
            move();
        }
    }

    public void stopAllAnimation() {
        anim.SetBool("Attack", false);
        anim.SetBool("isRunning", false);
    }

    public void move()
    {
        //the next four lines allows the zombie to face the player
        Vector3 targetDirection = player.transform.position - transform.position;
        float singleStep = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        //Makes the zombie move towards the player
        stopAllAnimation();
        anim.SetBool("isRunning", true);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            isHitting = true;
        }

        //else if other.CompareTag("Bullet") then stopAllAnimation(); anim.SetBool("isDead", true);

    }



}
