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

    public int maxAmmo = 20;
    private int currentAmmo;

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
        loadGameObjects();
        loadAudio();    
        currentAmmo = maxAmmo; 
        updateAmmoHud();
    }

    public IEnumerator Reload(){
        currentAmmo = maxAmmo;
        gun.GetComponent<Animation>().Play("gun_reload");
        reload.Play();
        updateAmmoHud();
        yield break;
    }

    public IEnumerator Shoot()
    {
        if(!hasAmmo()){
            gun_empty.Play();
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

        yield return new WaitForSeconds(1f);
    }

    void Update()
    {
        Debug.DrawRay(spawnPoint.transform.position, spawnPoint.transform.forward, Color.green);
    }
}
