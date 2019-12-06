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
        GetComponent<Animation>()["Z_Walk"].wrapMode = WrapMode.Loop;
        GetComponent<Animation>().Play("Z_Walk");

    }

    private void Update()
    {
        agent.destination = goal.position;
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
        agent.destination = gameObject.transform.position;
        //stop the walking animation and play the falling back animation
        GetComponent<Animation>().Stop();
        GetComponent<Animation>().Play("Z_FallingBack");
        agent.enabled = false;
        //destroy this zombie in six seconds.
        //Destroy(gameObject, 6);
        //instantiate a new zombie


        //set the coordinates for a new vector 3
        float randomX = UnityEngine.Random.Range(-12f, 12f);
        float constantY = .01f;
        float randomZ = UnityEngine.Random.Range(-13f, 13f);
        //set the zombies position equal to these new coordinates

        //if the zombie gets positioned less than or equal to 3 scene units away from the camera we won't be able to shoot it
        //so keep repositioning the zombie until it is greater than 3 scene units away. 
        Vector3 position = new Vector3(randomX, constantY, randomZ);
        while (Vector3.Distance(position, Camera.main.transform.position) <= 3)
        {

            randomX = UnityEngine.Random.Range(-12f, 12f);
            randomZ = UnityEngine.Random.Range(-13f, 13f);

            position = new Vector3(randomX, constantY, randomZ);
        }
        StartCoroutine(respawnZombie(position));

    }


    IEnumerator respawnZombie(Vector3 position)
    {
        yield return new WaitForSeconds(3f);
        gameObject.transform.position = position;
        GetComponent<Animation>()["Z_Walk"].wrapMode = WrapMode.Loop;
        GetComponent<Animation>().Play("Z_Walk");
        GetComponent<CapsuleCollider>().enabled = true;
        AudioSource.PlayClipAtPoint(respawnSound, this.transform.position);
        agent.enabled = true;
    }

}
