﻿using UnityEngine;
using System.Collections;

public class zombieScript : MonoBehaviour
{
    private float timer = 0;
    private float timerAtack = 0;
    private Transform goal;
    private UnityEngine.AI.NavMeshAgent agent;
    public AudioClip deathSound;
    public AudioClip hitSound;
    public AudioClip respawnSound;
    private bool isAttacking = false;
    private bool zombieDead = false;
    

    void Start()
    {

        //create references
        goal = Camera.main.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = goal.position;
        GetComponent<Animation>()["Z_Run_InPlace"].wrapMode = WrapMode.Loop;
        GetComponent<Animation>().Play("Z_Run_InPlace");
        var asources = GetComponents<AudioSource>();
        var zombieNoise = asources[0];
        zombieNoise.loop = true;
        
        zombieNoise.Play();
            

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (agent.enabled)
        {
            agent.destination = goal.position;
            agent.speed = GameState.zombieSpeed;
            agent.destination = goal.position;

            if (agent.remainingDistance < 3.1 && agent.remainingDistance != 0 && Mathf.Infinity != agent.remainingDistance)
            {                
                if (!isAttacking){
                    this.attack();
                    isAttacking = true;
                }
                    
                if(timerAtack + 0.5 < timer)
                {
                    isAttacking = false;
                }
            }
            else
            {
                if (!zombieDead)
                    GetComponent<Animation>().Play("Z_Run_InPlace");
            }
        }
        if(zombieDead){
            isAttacking = false;
        }
    }
    public void attack()
    {
        timerAtack = timer;
        AudioSource.PlayClipAtPoint(hitSound, this.transform.position);
        GetComponent<Animation>().Play("Z_Attack");
        var asources = GetComponents<AudioSource>();
        var hit = asources[1];
        hit.Play();
        HealthBarScript.Health -= 10f;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "Player")
        {
            HealthBarScript.Health -= 10f;
        }
        else
        {
            Debug.Log("traafienie!");
            zombieDead = true;
            AudioSource.PlayClipAtPoint(deathSound, this.transform.position);
            GetComponent<CapsuleCollider>().enabled = false;
            Destroy(col.gameObject);
            agent.enabled = false;
            GetComponent<Animation>().Stop();
            var asources = GetComponents<AudioSource>();
            var zombieNoise = asources[0];
            zombieNoise.loop = false;
            zombieNoise.Stop();
            zombieNoise.loop = false;
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
}