using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gun : MonoBehaviour
{
    public int RPM = 500;
    public int AMMO_COUNT = 30;
    public int TOTAL_AMMO = 150;
    public Canvas aimingHUD;

    public Text currentAmmoText;
    public Text totalAmmoText;
    public Text score;

    public int currentTotalAmmo;
    int currentAmmo = 0;
    float shootCountDown = 0;
    bool aiming = false;
    Animator animator;
    List<GameObject> bullets = new List<GameObject>();

    public ParticleSystem muzzleFlash;
    public Transform camera;
    public GameObject bullet;
    public Transform canon;

    public AudioSource gunshot;
    public AudioSource reload;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentTotalAmmo = TOTAL_AMMO;
        aimingHUD.enabled = false;
        Reload();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = Menu.score.ToString();
        currentAmmoText.text = currentAmmo.ToString();
        totalAmmoText.text = currentTotalAmmo.ToString();
        DestroyOldBullets();
        transform.rotation = transform.rotation; 
        if(shootCountDown > 0)
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

    void DestroyOldBullets()
    {
        float maxDistance = 1000;
        for(int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i].GetComponent<Bullet>().collided)
            {
                GameObject bullet = bullets[i];
                bullets.RemoveAt(i);
                Destroy(bullet);
            }
            if (getPlayerDistance(bullets[i]) > maxDistance)
            {
                GameObject bullet = bullets[i];
                bullets.RemoveAt(i);
                Destroy(bullet);
            }   
        }
        
    }
    private float getPlayerDistance(GameObject objet)
    {
        PlayerActions player = FindObjectOfType<PlayerActions>();
        Vector3 dist = objet.transform.position - player.transform.position;
        return dist.magnitude;
    }

    public void Shoot()
    {
        if (!(animator.GetCurrentAnimatorStateInfo(0).IsName("IdleState") || animator.GetCurrentAnimatorStateInfo(0).IsName("Aiming")))
        {
            return;
        }
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
        if (!aiming)
        {
            muzzleFlash.startRotation = Random.RandomRange(0, 180);
            muzzleFlash.Play();
        }
        gunshot.Play();
        bullets.Add(Instantiate(bullet,canon.position,Quaternion.identity));
    }
    public void Reload()
    {
        if(currentAmmo == AMMO_COUNT)
        {
            return;
        }
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
        StopAim();
        animator.SetTrigger("isReloading");
        currentTotalAmmo -= AMMO_COUNT - currentAmmo;
        reload.Play();
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
        }
    }
}
