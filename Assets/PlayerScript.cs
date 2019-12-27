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

    void Start()
    {
        gun = gameObject.transform.GetChild(0).gameObject;
        spawnPoint = gun.transform.GetChild(0).gameObject;
        AmmoText.text = "11";
    }

    public IEnumerator Shoot()
    {
        GameObject bullet = Instantiate(Resources.Load("bullet", typeof(GameObject))) as GameObject;

        //Get the bullet's rigid body component and set its position and rotation equal to that of the spawnPoint
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        bullet.transform.rotation = spawnPoint.transform.rotation;
        bullet.transform.position = spawnPoint.transform.position;

        //add force to the bullet in the direction of the spawnPoint's forward vector
        rb.AddForce(spawnPoint.transform.forward * 500f);

        GetComponent<AudioSource>().Play();
        gun.GetComponent<Animation>().Play();

        Destroy(bullet, 1);

        yield return new WaitForSeconds(1f);
    }

    void Update()
    {
        Debug.DrawRay(spawnPoint.transform.position, spawnPoint.transform.forward, Color.green);
    }
}
