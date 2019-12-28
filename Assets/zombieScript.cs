using UnityEngine;
using System.Collections;

public class zombieScript : MonoBehaviour
{
    private float timer = 0;
    private float timerAtack = 0;
    private Transform goal;
    private UnityEngine.AI.NavMeshAgent agent;
    public AudioClip deathSound;
    public AudioClip respawnSound;
    private bool isAttack = false;
    private bool isDeath = false;

    void Start()
    {

        //create references
        goal = Camera.main.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = goal.position;
        GetComponent<Animation>()["Z_Run_InPlace"].wrapMode = WrapMode.Loop;
        GetComponent<Animation>().Play("Z_Run_InPlace");

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (agent.enabled)
        {
            agent.destination = goal.position;
            agent.speed = GameState.zombieSpeed;
            agent.destination = goal.position;

            if (agent.remainingDistance != 0 && agent.remainingDistance < 3.1)
            {
                if (!isAttack)
                    timerAtack = timer;
                isAttack = true;
                this.attack();
                if(timerAtack + 1 < timer)
                {
                    HealthBarScript.Health -= 10f;
                    isAttack = false;
                }
            }
            else
            {
                isAttack = false;
                if (!isDeath)
                    GetComponent<Animation>().Play("Z_Run_InPlace");
            }
            //Debug.Log(agent.remainingDistance);

        }
    }
    public void attack()
    {
        if (isAttack)
            GetComponent<Animation>().Play("Z_Attack");
    }
    //for this to work both need colliders, one must have rigid body, and the zombie must have is trigger checked.
    void OnTriggerEnter(Collider col)
    {
        if (col.name == "Player")
        {
            HealthBarScript.Health -= 10f;
        }
        else
        {
            Debug.Log("traafienie!");
            isDeath = true;
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
    }


    IEnumerator respawnZombie()
    {
        yield return new WaitForSeconds(3f);
        isDeath = false;
        GameObject zombie = GameState.getGameState().respZombie();
        AudioSource.PlayClipAtPoint(respawnSound, zombie.transform.position);
        zombie.GetComponent<zombieScript>().Start();
    }
}