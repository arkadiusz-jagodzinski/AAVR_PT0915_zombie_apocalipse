using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovingScript : MonoBehaviour {
    private Transform goal;
    private NavMeshAgent agent;

    void Start () {

        goal = Camera.main.transform;
        agent = GetComponent<NavMeshAgent> ();
        agent.destination = goal.position;
        
        //start animacji chodzenia 
        GetComponent<Animation> ().Play ("walk");
    }

    void OnTriggerEnter (Collider col) {
        //w razie problemow zakomentowac
        GetComponent<CapsuleCollider> ().enabled = false;
        //zniszczenie obiektu z kolizja (kuli)
        Destroy (col.gameObject);
        //zatrzymanie
        agent.destination = gameObject.transform.position;
        GetComponent<Animation> ().Stop ();
        GetComponent<Animation> ().Play ("back_fall");

        Destroy (gameObject, 6);
        //stworzenie nowego zombie
        GameObject zombie = Instantiate (Resources.Load ("zombie", typeof (GameObject))) as GameObject;

        //ustawienie nowych koordynatow zombie
        float randomX = UnityEngine.Random.Range (-12f, 12f);
        float constantY = .01f;//wysokosc zawsze ta sama
        float randomZ = UnityEngine.Random.Range (-13f, 13f);
    
        zombie.transform.position = new Vector3 (randomX, constantY, randomZ);

        //ustawianie zombiaka nie za blisko od gracza 
        while (Vector3.Distance (zombie.transform.position, Camera.main.transform.position) <= 3) {

            randomX = UnityEngine.Random.Range (-12f, 12f);
            randomZ = UnityEngine.Random.Range (-13f, 13f);

            zombie.transform.position = new Vector3 (randomX, constantY, randomZ);
        }

    }

}