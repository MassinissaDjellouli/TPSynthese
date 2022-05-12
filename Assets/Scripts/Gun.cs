using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int RPM = 30;
    public int AMMO_COUNT = 30;
    public int TOTAL_AMMO = 150;

    int currentTotalAmmo;
    int currentAmmo = 0;

     float shootCountDown = 0;
    public ParticleSystem muzzleFlash;
    // Start is called before the first frame update
    void Start()
    {
        currentTotalAmmo = TOTAL_AMMO;
        Reload();
    }

    // Update is called once per frame
    void Update()
    {
        if(shootCountDown > 0)
        {
            shootCountDown -= Time.deltaTime;
            //Fait en sorte que le countdown reste plus grand que 0
            shootCountDown = Mathf.Max(shootCountDown, 0);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }
    public void Shoot()
    {
        if(currentAmmo == 0)
        {
            return;
        }
        if(shootCountDown > 0)
        {
            return;
        }
        currentAmmo--;
        shootCountDown = (60f/RPM);
        muzzleFlash.startRotation = Random.RandomRange(0, 180);
        muzzleFlash.Play();
        Debug.Log("Shootin");
    }
    public void Reload()
    {
        if(currentTotalAmmo == 0)
        {
            return;
        }
        if(currentTotalAmmo < AMMO_COUNT - currentAmmo)
        {
            currentAmmo += currentTotalAmmo;
            currentTotalAmmo = 0;
            return;
        }
        currentTotalAmmo -= AMMO_COUNT - currentAmmo;
        currentAmmo = AMMO_COUNT;
    }
}
