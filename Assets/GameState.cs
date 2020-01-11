using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    private static int zombiesCount = 2;
    public static float zombieSpeed = 3.0f;
    public static float walkingSpeed = 6.0f;
    public static float kiledZombie = 0.0f;

    private static readonly List<Vector3> zombieRespPoints = new List<Vector3>
    {
        new Vector3(-15,0,2),
        new Vector3(-15,0,-10),
        new Vector3(15,0,-10),
        new Vector3(4,11,-33),
        new Vector3(10,11,-6),
        new Vector3(33,5,-17)
    };

    private static readonly List<Vector3> powerUpsRespPoints = new List<Vector3>
    {
        new Vector3(1,0,0),
        new Vector3(1,0,-20),
        new Vector3(4,11,-28),
        new Vector3(42,5,-22),
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

    public GameObject respNewZombie()
    {
        zombiesCount++;
        return respZombie();
    }

    public GameObject respZombie()
    {
        GameObject newZombie = Object.Instantiate(GameObject.Find("zombieZero"));
        newZombie.GetComponent<Transform>().position = getRandomZombieRespPosition();
        newZombie.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        newZombie.GetComponent<AudioSource>().enabled = true;
        newZombie.GetComponent<zombieScript>().enabled = true;
        newZombie.GetComponent<Animation>()["Z_Run_InPlace"].wrapMode = WrapMode.Loop;
        newZombie.GetComponent<Animation>().Play("Z_Run_InPlace");
        newZombie.GetComponent<CapsuleCollider>().enabled = true;

        return newZombie;
    }

    public GameObject respNewPowerUp()
    {
        GameObject newPowerUp = Object.Instantiate(GameObject.Find("powerupZero"));
        newPowerUp.GetComponent<Transform>().position = getRandomPowerUpRespPosition();
        Debug.Log("Nowy powerUp!");
        return newPowerUp;
    }

    private Vector3 getRandomZombieRespPosition()
    {
        return zombieRespPoints[Random.Range(0, zombieRespPoints.Count)];
    }

    private Vector3 getRandomPowerUpRespPosition()
    {
        return powerUpsRespPoints[Random.Range(0, powerUpsRespPoints.Count)];
    }
}