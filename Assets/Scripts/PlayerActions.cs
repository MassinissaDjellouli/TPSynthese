using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    // Start is called before the first frame update
    public Gun gun;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            gun.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            gun.Reload();
        }
        if (Input.GetMouseButtonDown(1))
        {
            gun.Aim();
        }
        if (Input.GetMouseButtonUp(1))
        {
            gun.StopAim();
        }

    }

}
