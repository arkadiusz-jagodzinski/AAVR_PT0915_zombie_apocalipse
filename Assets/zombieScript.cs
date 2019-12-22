using UnityEngine;
using System.Collections;

public class zombieScript : MonoBehaviour
{
    private Transform goal;
    private UnityEngine.AI.NavMeshAgent agent;
    public AudioClip deathSound;
    public AudioClip respawnSound;

    void Start()
    {

        //create references
        goal = Camera.main.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = goal.position;
        GetComponent<Animation>()["Z_Walk_InPlace"].wrapMode = WrapMode.Loop;
        GetComponent<Animation>().Play("Z_Walk_InPlace");

    }

    private void Update()
    {
        if(agent.enabled)agent.destination = goal.position;
    }
    //for this to work both need colliders, one must have rigid body, and the zombie must have is trigger checked.
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("traafienie!");
        AudioSource.PlayClipAtPoint(deathSound, this.transform.position);
        //first disable the zombie's collider so multiple collisions cannot occur
        GetComponent<CapsuleCollider>().enabled = false;
        //destroy the bullet
        Destroy(col.gameObject);
        //stop the zombie from moving forward by setting its destination to it's current position
        agent.enabled = false;
        //stop the walking animation and play the falling back animation
        GetComponent<Animation>().Stop();
        GetComponent<Animation>().Play("Z_FallingBack");
        
        //destroy this zombie in six seconds.
        Destroy(gameObject, 6);
        //instantiate a new zombie

        StartCoroutine(respawnZombie());

    }


    IEnumerator respawnZombie()
    {
        yield return new WaitForSeconds(3f);
        GameObject zombie = GameState.getGameState().respZombie();
        AudioSource.PlayClipAtPoint(respawnSound, zombie.transform.position);
        zombie.GetComponent<zombieScript>().Start();
    }

}
