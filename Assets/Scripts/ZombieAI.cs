using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{

    private Animator anim;
    private GameObject player = null;

    public GameObject Player;
    public int zombieHp = 6;
    public float movementSpeed = 10f;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        //if player is alive then run towards him
        //if player is dead then idle

            anim.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);


        //Zombie runs towards player
        if (Input.GetKey(KeyCode.LeftArrow)) {
            anim.SetBool("isRunning", true);
        }
        //when the zombie is close enough, the zombie kills the player
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("Attack", true);
        }          

        if (Input.GetKey(KeyCode.DownArrow)) {
            stopAllAnimation();
        }

        //Zombie dies, zombieHp < 0
        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("isDead", true);
        }


        //TEMPORARY cant make the colliders work so im using this instead
        if (transform.position == player.transform.position)
        {
            Debug.Log("Impact");
            stopAllAnimation();
            anim.SetBool("Attack", true);

        }


    }


    public void stopAllAnimation() {
        anim.SetBool("Attack", false);
        anim.SetBool("isRunning", false);
    }

    private void OnTriggerEnter(Collider other)
    {
     //   if (other.CompareTag("Player")) {
      //      Debug.Log("Impact");
      //      anim.SetBool("isRunning", false);
      //     anim.SetBool("Attack", true);
       // }

    }



}
