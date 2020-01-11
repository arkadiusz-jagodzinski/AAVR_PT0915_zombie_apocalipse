using UnityEngine;
using System.Collections;

public class zombieScript : MonoBehaviour
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

    // PlayerScript playerScript;

    void Start()
    {

        //create references
        goal = Camera.main.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = goal.position;
        GetComponent<Animation>()["Z_Run_InPlace"].wrapMode = WrapMode.Loop;
        GetComponent<Animation>().Play("Z_Run_InPlace");
        var asources = GetComponents<AudioSource>();
        zombieNoise = asources[0];
        zombieNoise.loop = true;
        zombieNoise.Play();
        zombieNoise.volume = 0.05f;

        // playerScript = FindObjectOfType<PlayerScript>();
        // playerScript.GameOverEvent += Instance_GameOverEvent;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (agent.enabled)
        {
            agent.destination = goal.position;
            agent.speed = GameState.zombieSpeed;
            agent.destination = goal.position;

            if (agent.remainingDistance < 3.1 && agent.remainingDistance > 0.5)
            {
                if (!isAttacking)
                {
                    this.attack();
                    isAttacking = true;
                }

                if (timerAtack + 0.5 < timer)
                {
                    isAttacking = false;
                }
            }
            else
            {
                if (zombieDead)
                {
                    zombieNoise.loop = false;
                    zombieNoise.Stop();
                    zombieNoise.mute = true;
                    zombieDead = true;
                }
                else
                {
                    GetComponent<Animation>().Play("Z_Run_InPlace");

                }
            }
        }
        if (zombieDead)
        {
            isAttacking = false;
        }

   /*     if (PlayerScript.gameEnded)
        {
            Destroy(gameObject);
        }
        */
    }
    public void attack()
    {
        timerAtack = timer;
        AudioSource.PlayClipAtPoint(hitSound, this.transform.position);
        GetComponent<Animation>().Play("Z_Attack");
        var asources = GetComponents<AudioSource>();
        var hit = asources[1];
        hit.Play();
        HealthBarScript.Health -= zombieDmg;

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "bullet(Clone)")
        {
            Debug.Log("traafienie!");
            zombieNoise.loop = false;
            zombieNoise.Stop();
            zombieNoise.mute = true;
            zombieDead = true;
            GameState.kiledZombie++;
            AudioSource.PlayClipAtPoint(deathSound, this.transform.position);
            GetComponent<CapsuleCollider>().enabled = false;
            Destroy(col.gameObject);
            agent.enabled = false;
            GetComponent<Animation>().Stop();



            GetComponent<Animation>().Play("Z_FallingBack");
            Destroy(gameObject, 6);
            StartCoroutine(respawnZombie());
        }
    }


    IEnumerator respawnZombie()
    {
        yield return new WaitForSeconds(3f);
        zombieDead = false;
        GameObject zombie = GameState.getGameState().respZombie();
        AudioSource.PlayClipAtPoint(respawnSound, zombie.transform.position);
        zombie.GetComponent<zombieScript>().Start();
    }

    private void Instance_GameOverEvent(object sender, System.EventArgs e)
    {
        zombieNoise.loop = false;
        zombieNoise.Stop();
        zombieNoise.mute = true;
        zombieDead = true;
    }
}
