using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    PlayerScript playerScript;
    private int lastTime = 0;
    private bool stopTimer = false;
    private int timer = 0;
    public RectTransform mPanelGameOver;
    public Text mTextGameOver;

    public RectTransform mPanelNotification;
    public Text mTextNotification;
    // Start is called before the first frame update
    void Start()
    {
        lastTime = (int)Time.time;
        GameState.kiledEnemies = 0;
        playerScript = FindObjectOfType<PlayerScript>();
        playerScript.GameOverEvent += Instance_GameOverEvent;
    }
    private void Instance_GameOverEvent(object sender, System.EventArgs e)
    {
        mPanelGameOver.gameObject.SetActive(true);
        if (!stopTimer)
        {
            timer = (int)(Time.time - lastTime);
            stopTimer = true;
            lastTime = (int)Time.time;
        }
        mTextGameOver.text = "Game Over \n" + "Killed Enemies: " + GameState.kiledEnemies.ToString() + "\n Game Time: " + timer + "s";
        
    }
    public  void setNotification(bool state)
    {
        mPanelNotification.gameObject.SetActive(state);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
