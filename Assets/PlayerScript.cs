﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private GameObject gun;
    private GameObject spawnPoint;

    private IEnumerator coroutine;
    public Text AmmoText;
    AudioSource shoot;
    AudioSource reload;
    AudioSource gun_empty;
    AudioSource audioSource;

    public int maxAmmo = 20;
    public float empty_ammo_block_time_sec = 0.3f;
    public float reload_block_time_sec = 0.9f;
    private int currentAmmo;
    private bool isShotingBlocked;

    public event EventHandler GameOverEvent;

    IEnumerator blockShoting(float sec)
    {
        yield return new WaitForSeconds(sec);
        isShotingBlocked = false;
    }

    private void OnGameOver()
    {
        if (GameOverEvent != null)
            GameOverEvent(this, EventArgs.Empty);
    }

    private void updateAmmoHud() {
        AmmoText.text = currentAmmo.ToString();
    }

    private bool hasAmmo() {
        return currentAmmo > 0;
    }

    private void loadAudio(){
        var asources = GetComponents<AudioSource>();
        shoot = asources[0];
        reload = asources[1];
        gun_empty = asources[2];
    }

    private void loadGameObjects(){
        gun = gameObject.transform.GetChild(0).gameObject;
        spawnPoint = gun.transform.GetChild(0).gameObject;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 1;
        loadGameObjects();
        loadAudio();    
        currentAmmo = maxAmmo; 
        updateAmmoHud();
    }

    public IEnumerator Reload(){
        if(isShotingBlocked)
            yield break;

        isShotingBlocked = true;
        currentAmmo = maxAmmo;
        gun.GetComponent<Animation>().Play("gun_reload");
        reload.Play();
        updateAmmoHud();
        yield return StartCoroutine(blockShoting(reload_block_time_sec));
    }

    public IEnumerator Shoot()
    {
        if(isShotingBlocked)
            yield break;

        if(!hasAmmo()){
            gun_empty.Play();
            StartCoroutine(blockShoting(empty_ammo_block_time_sec));
            yield break;
        }

        currentAmmo--;
        updateAmmoHud();

        GameObject bullet = Instantiate(Resources.Load("bullet", typeof(GameObject))) as GameObject;

        //Get the bullet's rigid body component and set its position and rotation equal to that of the spawnPoint
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        bullet.transform.rotation = spawnPoint.transform.rotation;
        bullet.transform.position = spawnPoint.transform.position;

        //add force to the bullet in the direction of the spawnPoint's forward vector
        rb.AddForce(spawnPoint.transform.forward * 500f);

        shoot.Play();
        gun.GetComponent<Animation>().Play("gun");

        Destroy(bullet, 1);
        
        isShotingBlocked = true;
        yield return StartCoroutine(blockShoting(1f));
    }

    void Update()
    {
        Debug.DrawRay(spawnPoint.transform.position, spawnPoint.transform.forward, Color.green);
        if (HealthBarScript.Health < 1)
        {
            OnGameOver();
            Time.timeScale = 0;
            audioSource.mute = true;
        }
           
    }
}
