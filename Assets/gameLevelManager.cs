using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameLevelManager : MonoBehaviour
{
    public static bool isIncreasingLevelEnabled = true;
    public static float zombieSpeedIncrease = 0.1f;
    public static int zombieSpeedIncreasePeriod = 8;

    public static bool isIncrementingZombieNumberEnabled = true;
    public static int zombieNumberIncrementPeriod = 20;


    
    private GameState gameState;
    void Start()
    {
        gameState = GameState.getGameState();
        StartCoroutine(increaseZombieSpeed());
        StartCoroutine(incrementZombiesNumber());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator increaseZombieSpeed()
    {
        while (isIncreasingLevelEnabled)
        {
            GameState.zombieSpeed += zombieSpeedIncrease;
            yield return new WaitForSeconds(zombieSpeedIncreasePeriod);
        }
    }

    IEnumerator incrementZombiesNumber()
    {
        while (isIncrementingZombieNumberEnabled)
        {
            gameState.respNewZombie();
            yield return new WaitForSeconds(zombieNumberIncrementPeriod);
        }
    }
}
