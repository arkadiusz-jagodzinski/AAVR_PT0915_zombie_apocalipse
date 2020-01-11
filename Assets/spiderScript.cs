using UnityEngine;
using System.Collections;

public class spiderScript : MonoBehaviour
{
    private float timer = 0;
    private float timerAtack = 0;
    private Transform goal;
    private UnityEngine.AI.NavMeshAgent agent;
    public float zombieDmg = 5f;
    public AudioClip deathSound;
    public AudioClip hitSound;
    public AudioClip respawnSound;
    private bool isAttacking = false;
    private bool zombieDead = false;

    private AudioSource zombieNoise;
    private AudioSource hit;

    void Start()
    {

        //create references
        goal = Camera.main.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = goal.position;
        GetComponent<Animation>()["run"].wrapMode = WrapMode.Loop;
        GetComponent<Animation>().Play("run");
        var asources = GetComponents<AudioSource>();
        zombieNoise = asources[1];
        zombieNoise.loop = true;
        hit = asources[0];

        zombieNoise.Play();
        zombieNoise.volume = 0.5f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (agent.enabled)
        {
            agent.destination = goal.position;
            agent.speed = GameState.enemySpeed;
            agent.destination = goal.position;

            if (agent.remainingDistance < 4.1 && agent.remainingDistance != 0 && Mathf.Infinity != agent.remainingDistance)
            {
                if (!isAttacking)
                {
                    this.attack();
                    isAttacking = true;
                }

                if (timerAtack + 1.1 < timer)
                {
                    isAttacking = false;
                }
            }
            else
            {
                if (!zombieDead)
                    GetComponent<Animation>().Play("run");
            }
        }
        if (zombieDead)
        {
            isAttacking = false;
        }
        if (PlayerScript.gameEnded)
        {
            Destroy(gameObject);
        }
    }
    public void attack()
    {
        Debug.Log("spider attack");
        timerAtack = timer;
        AudioSource.PlayClipAtPoint(hitSound, this.transform.position);
        GetComponent<Animation>().Play("attack1");
        hit.Play();
        HealthBarScript.setHealth(HealthBarScript.getHealth() - zombieDmg);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "bullet(Clone)")
        {
            Debug.Log("traafienie!");
            zombieDead = true;
            AudioSource.PlayClipAtPoint(deathSound, this.transform.position);
            GetComponent<CapsuleCollider>().enabled = false;
            Destroy(col.gameObject);
            agent.enabled = false;
            GetComponent<Animation>().Stop();

            zombieNoise.loop = false;
            zombieNoise.Stop();
            zombieNoise.mute = true;

            GetComponent<Animation>().Play("death1");
            Destroy(gameObject, 6);
            StartCoroutine(respawnZombie());
        }
    }


    IEnumerator respawnZombie()
    {
        yield return new WaitForSeconds(3f);
        zombieDead = false;
        GameObject zombie = GameState.getGameState().respSpider();
        AudioSource.PlayClipAtPoint(respawnSound, zombie.transform.position);
        zombie.GetComponent<spiderScript>().Start();
    }
}