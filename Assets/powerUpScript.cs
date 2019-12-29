using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpScript : MonoBehaviour
{
    public static float SPEED_BOOST = (float) 1.5;
    public static int SPEED_UP_LENGTH = 5;
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
            GameState.walkingSpeed *= SPEED_BOOST;
            Invoke("disablingSpeedUp", SPEED_UP_LENGTH);
            
        }
    }

    void disablingSpeedUp()
    {
        GameState.walkingSpeed /= SPEED_BOOST;
    }
}
