using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    private static int zombiesCount = 2;
    private static float zombieSpeed = 3.0f;

    private static readonly List<Vector3> respPoints = new List<Vector3>
    {
        new Vector3(-15,0,2),
        new Vector3(-15,0,-10),
        new Vector3(15,0,-10),
        new Vector3(4,11,-33),
        new Vector3(10,11,-6),
        new Vector3(33,5,-17)
    };

    private static GameState gameState;

    public static GameState getGameState()
    {
        if (gameState == null) gameState = new GameState();
        return gameState;
    }

    private GameState() { }

    public int getZombiesCount()
    {
        return zombiesCount;
    }

    public float getZombieSpeed()
    {
        return zombieSpeed;
    }


    public GameObject respNewZombie()
    {
        zombiesCount++;
        return respNewZombie();
    }

    public GameObject respZombie()
    {
        GameObject newZombie = Object.Instantiate(GameObject.Find("zombieZero"));
        newZombie.GetComponent<Transform>().position = getRandomRespPosition();
        newZombie.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        newZombie.GetComponent<AudioSource>().enabled = true;
        newZombie.GetComponent<zombieScript>().enabled = true;
        newZombie.GetComponent<Animation>()["Z_Run_InPlace"].wrapMode = WrapMode.Loop;
        newZombie.GetComponent<Animation>().Play("Z_Run_InPlace");
        newZombie.GetComponent<CapsuleCollider>().enabled = true;

        return newZombie;
    }

    private Vector3 getRandomRespPosition()
    {
        return respPoints[Random.Range(0, respPoints.Count)];
    }
}