using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpScript : MonoBehaviour
{
    public static float SPEED_BOOST = 1.8f;
    public static int SPEED_UP_LENGTH = 25;
    public static int IMMORTALITY_LENGTH = 20;
    public static int FROZEN_ENEMIES_LENGTH = 15;
    public AudioClip activationSound;
    
    void Start()
    {
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            Debug.Log("PowerUp zebrany!");
            AudioSource.PlayClipAtPoint(activationSound, this.transform.position);
            this.gameObject.GetComponent<Transform>().position = new Vector3(0,0,-222);
            chooseRandomPowerUp();
            
        }
    }

    void chooseRandomPowerUp()
    {
        int powerUpNumber = Random.Range(0, 5);
        if(powerUpNumber == 0)
        {
            Debug.Log("PowerUp: Przyspieszenie");
            GameState.walkingSpeed *= SPEED_BOOST;
            Invoke("disablingSpeedUp", SPEED_UP_LENGTH);
            FindObjectOfType<HUDScript>().mTextNotification.text = "Speed up";
            FindObjectOfType<HUDScript>().mPanelNotification.gameObject.SetActive(true);
           
        }
        if(powerUpNumber == 1)
        {
            Debug.Log("PowerUp: nieśmiertelność");
            HealthBarScript.isMortal = false;
            Invoke("disablingImmortality", IMMORTALITY_LENGTH);
            FindObjectOfType<HUDScript>().mTextNotification.text = "Immortality";
            FindObjectOfType<HUDScript>().mPanelNotification.gameObject.SetActive(true);
            
        }
        if(powerUpNumber == 2)
        {
            Debug.Log("PowerUp: zamrożenie");
            GameObject[] enemies =  GameObject.FindGameObjectsWithTag("enemy");
            foreach(GameObject enemy in enemies)
            {
                enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
            }
            Invoke("disablingFrozenEnemies", FROZEN_ENEMIES_LENGTH);
            FindObjectOfType<HUDScript>().mTextNotification.text = "Frozen";
            FindObjectOfType<HUDScript>().mPanelNotification.gameObject.SetActive(true);
            
        }
        if(powerUpNumber == 3 || powerUpNumber == 4)
        {
            Debug.Log("PowerUp: uzupełnienie zdrowia");
            HealthBarScript.restoreHealth();
        }
    }


    void disablingSpeedUp()
    {
        GameState.walkingSpeed /= SPEED_BOOST;
        FindObjectOfType<HUDScript>().mPanelNotification.gameObject.SetActive(false);
    }

    void disablingImmortality()
    {
        HealthBarScript.isMortal = true;
        FindObjectOfType<HUDScript>().mPanelNotification.gameObject.SetActive(false);
    }

    void disablingFrozenEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        }
        FindObjectOfType<HUDScript>().mPanelNotification.gameObject.SetActive(false);
    }
}
