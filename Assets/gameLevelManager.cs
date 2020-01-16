using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameLevelManager : MonoBehaviour
{
    private static bool isIncreasingLevelEnabled = false;
    private static float zombieSpeedIncrease = 0.2f;
    private static int zombieSpeedIncreasePeriod = 20;

    private static bool isIncrementingZombieNumberEnabled = true;
    private static int zombieNumberIncrementPeriod = 1;

    public void setTutorialDifficulty()
    {
        isIncreasingLevelEnabled = false;
        isIncrementingZombieNumberEnabled = false;
    }

    public void setEasyDifficulty()
    {
        isIncreasingLevelEnabled = false;
        isIncrementingZombieNumberEnabled = true;
        zombieNumberIncrementPeriod = 60;
    }

    public void setMediumDifficulty()
    {
        isIncreasingLevelEnabled = true;
        isIncrementingZombieNumberEnabled = true;
        zombieNumberIncrementPeriod = 45;
        zombieSpeedIncreasePeriod = 20;
    }

    public void setHardDifficulty()
    {
        isIncreasingLevelEnabled = true;
        isIncrementingZombieNumberEnabled = true;
        zombieNumberIncrementPeriod = 30;
        zombieSpeedIncreasePeriod = 15;
    }

    public void setInsaneDifficulty()
    {
        isIncreasingLevelEnabled = true;
        isIncrementingZombieNumberEnabled = true;
        zombieNumberIncrementPeriod = 20;
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
            GameState.enemySpeed += zombieSpeedIncrease;
            yield return new WaitForSeconds(zombieSpeedIncreasePeriod);
        }
    }

    IEnumerator incrementZombiesNumber()
    {
        while (isIncrementingZombieNumberEnabled)
        {
            gameState.respNewEnemy();
            yield return new WaitForSeconds(zombieNumberIncrementPeriod);
        }
    }
}
