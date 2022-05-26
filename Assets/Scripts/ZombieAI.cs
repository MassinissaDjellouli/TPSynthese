using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ZombieAI : MonoBehaviour
{
    private Animator anim;
    private Collider collider;
    private GameObject player;
    private bool isHitting = false;
    private bool isAlive = true;
    private float capturedPos;
    private NavMeshAgent NavMeshAgent;

    //public Text scoreText;

    public int zombieHp = 6;
    public float movementSpeed = 10f;
    public float speed = 1.0f;


    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
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
            if (capturedPos != transform.position.x)
            {
                isHitting = false;
            }
        }
        else if (isAlive)
        {
            move();
        }
    }


    public void stopAllAnimation()
    {
        anim.SetBool("Attack", false);
        anim.SetBool("isRunning", false);
    }

    public void move()
    {
        //Allows the zombie to face the player
        Vector3 targetDirection = player.transform.position - transform.position;
        float singleStep = speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

        //Makes the zombie move towards the player
        stopAllAnimation();
        anim.SetBool("isRunning", true);
        Vector3 playerPos = new Vector3(player.transform.position.x-1f, 1, player.transform.position.z);
        NavMeshAgent.destination = playerPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //this will never happen because the body identified with TAG Player will never touch the zombie's Collider
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit player");
            isHitting = true;
            capturedPos = player.transform.position.x;
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
    }



}
