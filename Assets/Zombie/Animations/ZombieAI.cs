using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{

    private Animator anim;
    public GameObject Player;
    public int zombieHp = 6;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //if player is alive then run towards him
        //if player is dead then idle


        //Zombie runs towards player
        if (Input.GetKey(KeyCode.LeftArrow)) {
            anim.SetBool("isRunning", true);
        }
        //when the zombie is close enough, the zombie kills the player
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("Attack", true);
        }
        else {
           // anim.SetBool("Attack", false);
           // anim.SetBool("isRunning", false);

        }           

        // Debug.Log("Right else" + anim.GetBool("Attack") + " //" + anim.GetBool("isRunning"));

        if (Input.GetKey(KeyCode.DownArrow)) {
            anim.SetBool("Attack", false);
            anim.SetBool("isRunning", false);
        }


        //Zombie dies, zombieHp < 0
        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("isDead", true);
        }
        

    }
}
