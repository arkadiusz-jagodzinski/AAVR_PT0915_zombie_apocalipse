using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameLevelManager : MonoBehaviour
{
    private static bool isIncreasingLevelEnabled = true;
    private static float zombieSpeedIncrease = 0.2f;
    private static int zombieSpeedIncreasePeriod = 8;

    private static bool isIncrementingZombieNumberEnabled = true;
    private static int zombieNumberIncrementPeriod = 20;

    public static void setTutorialDifficulty()
    {
        isIncreasingLevelEnabled = false;
        isIncrementingZombieNumberEnabled = false;
    }

    public static void setEasyDifficulty()
    {
        isIncreasingLevelEnabled = false;
        isIncrementingZombieNumberEnabled = true;
        zombieNumberIncrementPeriod = 60;
    }

    public static void setMediumDifficulty()
    {
        isIncreasingLevelEnabled = true;
        isIncrementingZombieNumberEnabled = true;
        zombieNumberIncrementPeriod = 45;
        zombieSpeedIncreasePeriod = 20;
    }

    public static void setHardDifficulty()
    {
        isIncreasingLevelEnabled = true;
        isIncrementingZombieNumberEnabled = true;
        zombieNumberIncrementPeriod = 30;
        zombieSpeedIncreasePeriod = 15;
    }

    public static void setInsaneDifficulty()
    {
        isIncreasingLevelEnabled = true;
        isIncrementingZombieNumberEnabled = true;
        zombieNumberIncrementPeriod = 25;
        zombieSpeedIncreasePeriod = 10;
    }



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
