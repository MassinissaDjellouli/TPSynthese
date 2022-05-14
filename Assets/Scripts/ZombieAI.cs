using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieAI : MonoBehaviour
{

    private Animator anim;
    private GameObject player;
    private bool isHitting = false;
    private bool isAlive = true;
    private Vector3 capturedPos;

    public Text scoreText;

    private Collider collider;

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
            if (capturedPos != transform.position)
            {
                isHitting = false;
            }
        }
        else if (isAlive)
        {

            //return;


            move();
            Vector3 targetDirection = player.transform.position - transform.position;
            float singleStep = speed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

        }
    }


    public void stopAllAnimation()
    {
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit player");
            isHitting = true;
            capturedPos = player.transform.position;
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit gun");
            stopAllAnimation();
            isAlive = false;
            anim.SetBool("isDead", true);
            Object.Destroy(gameObject, 3.0f);
            collider = gameObject.GetComponent<Collider>();
            collider.enabled = false;
            Menu.score += 10;
        }



        //else if other.CompareTag("Bullet") then

    }



}
