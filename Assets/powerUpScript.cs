using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpScript : MonoBehaviour
{
    public static float SPEED_BOOST = 3;
    public static int SPEED_UP_LENGTH = 5;
    private MeshRenderer renderer;
    private bool isActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Player") && !isActivated)
        {
            isActivated = true;
            GameState.walkingSpeed *= SPEED_BOOST;
            Debug.Log("PowerUp zebrany!");
            renderer.enabled = false;
            Invoke("disablingSpeedUp", SPEED_UP_LENGTH);
            
        }
    }

    void disablingSpeedUp()
    {
        GameState.walkingSpeed /= SPEED_BOOST;
        GameObject.Destroy(this.gameObject);
    }
}
