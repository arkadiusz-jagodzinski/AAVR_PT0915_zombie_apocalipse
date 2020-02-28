using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUpsManager : MonoBehaviour
{

    public static int POWER_UPS_PERIOD = 25;
    public static bool isEnabled = true;
    private GameState gameState;
    void Start()
    {
        gameState = GameState.getGameState();
        StartCoroutine("startRespingPowerUps");
    }

    void Update()
    {
        
    }

    IEnumerator startRespingPowerUps()
    {
        while (isEnabled)
        {
            gameState.respNewPowerUp();
            yield return new WaitForSeconds(POWER_UPS_PERIOD);
        }
    }
}
