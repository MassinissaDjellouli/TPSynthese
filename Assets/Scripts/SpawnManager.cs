using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;

public class SpawnManager : MonoBehaviour
{
    public GameObject zombie;
    public int zombiesPerRespawn;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnZombies", 1.5f, 6.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnZombies()
    {

        for (int i = 0; i < zombiesPerRespawn; i++)
        {
            Instantiate(zombie, transform.position, transform.rotation);
        }
    }
}
