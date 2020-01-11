using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    private static int enemyCount = 2;
    public static float enemySpeed = 3.0f;
    public static float walkingSpeed = 6.0f;
    public static float kiledEnemies = 0.0f;

    private static readonly List<Vector3> enemyRespPoints = new List<Vector3>
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
        return enemyCount;
    }

    public GameObject respNewEnemy()
    {
        enemyCount++;
        return respEnemy();
    }

    public GameObject respEnemy()
    {
        int enemyType = Random.Range(0, 2);
        if (enemyType == 0)
        {
            return respZombie();
        }
        else
        {
            return respSpider();
        }
      
    }

    public GameObject respZombie()
    {
        GameObject newEnemy = Object.Instantiate(GameObject.Find("zombieZero"));
        newEnemy.GetComponent<Transform>().position = getRandomZombieRespPosition();
        newEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        newEnemy.GetComponent<AudioSource>().enabled = true;
        newEnemy.GetComponent<zombieScript>().enabled = true;
        newEnemy.GetComponent<Animation>()["Z_Run_InPlace"].wrapMode = WrapMode.Loop;
        newEnemy.GetComponent<Animation>().Play("Z_Run_InPlace");
        newEnemy.GetComponent<CapsuleCollider>().enabled = true;
        return newEnemy;
    }

    public GameObject respSpider()
    {
        GameObject newEnemy = Object.Instantiate(GameObject.Find("spiderZero"));
        newEnemy.GetComponent<Transform>().position = getRandomZombieRespPosition();
        newEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        newEnemy.GetComponent<AudioSource>().enabled = true;
        newEnemy.GetComponent<spiderScript>().enabled = true;
        newEnemy.GetComponent<Animation>()["run"].wrapMode = WrapMode.Loop;
        newEnemy.GetComponent<Animation>().Play("run");
        newEnemy.GetComponent<CapsuleCollider>().enabled = true;
        return newEnemy;
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
        return enemyRespPoints[Random.Range(0, enemyRespPoints.Count)];
    }

    private Vector3 getRandomPowerUpRespPosition()
    {
        return powerUpsRespPoints[Random.Range(0, powerUpsRespPoints.Count)];
    }
}