using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    PlayerScript playerScript;
    public RectTransform mPanelGameOver;
    public Text mTextGameOver;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<PlayerScript>();
        playerScript.GameOverEvent += Instance_GameOverEvent;
    }
    private void Instance_GameOverEvent(object sender, System.EventArgs e)
    {
        mPanelGameOver.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
