using UnityEngine;
using System.Collections;

public class zombieScript : MonoBehaviour
{
    private Transform goal;
    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {

        //create references
        goal = Camera.main.transform;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = goal.position;
        GetComponent<Animation>()["Z_Walk"].wrapMode = WrapMode.Loop;
        GetComponent<Animation>().Play("Z_Walk");
    }
}
