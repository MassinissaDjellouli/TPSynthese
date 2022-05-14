using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gun : MonoBehaviour
{
    public int RPM = 30;
    public int AMMO_COUNT = 30;
    public int TOTAL_AMMO = 150;
    public Canvas aimingHUD;

    public Text currentAmmoText;
    public Text totalAmmoText;

    public Text score;

    int currentTotalAmmo;
    int currentAmmo = 0;
    float shootCountDown = 0;
    bool aiming = false;
    public ParticleSystem muzzleFlash;
    Animator animator;
    public Transform camera;
    public GameObject bullet;
    public Transform canon;

    Vector3 aimingPos;
    Vector3 initPos;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentTotalAmmo = TOTAL_AMMO;
        aimingHUD.enabled = false;
        Reload();
        aimingPos = new Vector3(0, -0.055f, 0.475f);
        initPos = new Vector3(0.374f, -0.149f, 0.415f);
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        score.text = Menu.score.ToString();
        currentAmmoText.text = currentAmmo.ToString();
        totalAmmoText.text = currentTotalAmmo.ToString();
        transform.rotation = camera.transform.rotation;
        if (shootCountDown > 0)
=======
        transform.rotation = transform.rotation; 
        if(shootCountDown > 0)
>>>>>>> 4ac4ccebff49f968e016f25cacfe0a41a1affb4d
        {
            shootCountDown -= Time.deltaTime;
            //Fait en sorte que le countdown reste plus grand que 0
            shootCountDown = Mathf.Max(shootCountDown, 0);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ReloadFinished"))
        {
            animator.ResetTrigger("isReloading");
        }
    }

    public void StopAim()
    {
        animator.ResetTrigger("isAiming");
        aiming = false;
        aimingHUD.enabled = false;
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void Shoot()
    {
        Debug.Log("test");
        if (!(animator.GetCurrentAnimatorStateInfo(0).IsName("IdleState") || animator.GetCurrentAnimatorStateInfo(0).IsName("Aiming")))
        {
            return;
        }
        if (currentAmmo == 0)
        {
            return;
        }
        if (shootCountDown > 0)
        {
            return;
        }
        currentAmmo--;
        shootCountDown = (60f / RPM);
        if (!aiming)
        {
            muzzleFlash.startRotation = Random.RandomRange(0, 180);
            muzzleFlash.Play();
        }
        Instantiate(bullet, canon.position, Quaternion.identity);
        Debug.Log("Shootin");
    }
    public void Reload()
    {
        if (currentAmmo == AMMO_COUNT)
        {
            return;
        }
        if (currentTotalAmmo == 0)
        {
            return;
        }
        if (currentTotalAmmo < AMMO_COUNT - currentAmmo)
        {
            currentAmmo += currentTotalAmmo;
            currentTotalAmmo = 0;
            return;
        }
        StopAim();
        animator.SetTrigger("isReloading");
        currentTotalAmmo -= AMMO_COUNT - currentAmmo;
        currentAmmo = AMMO_COUNT;
    }
    public void Aim()
    {
        if (!aiming)
        {
            aimingHUD.enabled = true;
            GetComponent<MeshRenderer>().enabled = false;
            animator.SetTrigger("isAiming");
            aiming = true;
            Debug.Log("aimin");
        }
    }
}
