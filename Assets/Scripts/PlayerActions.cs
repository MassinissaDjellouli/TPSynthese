using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour
{
    // Start is called before the first frame update
    public int life = 100;
    float lastHit = 0;
    float hitDelay = 2;
    public Gun gun;
    public Canvas gameOverHud;
    public CanvasGroup damageHud;
    public AudioSource damageSound;
    public AudioSource deathSound;
    public Text lifeText;
    public int killCount;
    void Start()
    {
        damageHud.alpha = 0;
        gameOverHud.enabled = false;    
    }

    // Update is called once per frame
    void Update()
    {
        lifeText.text = $"Life: {life}";
        if(lastHit > 0) lastHit -= Time.deltaTime;
        if(lastHit < 0) lastHit = 0; 
        if(damageHud.alpha > 0)
        {
            damageHud.alpha-=Time.deltaTime/4;
        }
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

    internal void Hit(int damage)
    {
        if(lastHit == 0 && life > 0)
        {
            lastHit = hitDelay;
            life -= damage;
            damageHud.alpha = 0.5f ;
            damageSound.Play();
        }
        if (life <= 0)
        {
            gameOverHud.enabled = true;
            AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
            life = 0;
            foreach (AudioSource audio in allAudioSources) {
                audio.Stop();
            }
            deathSound.Play();
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }

    public void GiveLife(int amount)
    {
        killCount++;
        if(life < 100)
        {
            life += amount;
        }
        if (life > 100) life = 100;
        if(killCount == 10)
        {
            gun.currentTotalAmmo += 30;
            killCount = 0;
        }
    }
}
