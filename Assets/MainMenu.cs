﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI DifficultyButtonText;
    public gameLevelManager GLMScript;

    public void PlayGame()
    {
        Debug.Log("PlayGame()");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void ChangeDifficultyText()
    {
        const int length = 4;
        string[] difficulty = new string[length] {"easy","medium","hard","insane"};
        var oldText = DifficultyButtonText.text;
        var newText = "";
        

        

        for(var i = 0;i<length;i++){
            if(oldText == difficulty[i]){
                newText = difficulty[(i+1)%(length)];
                switch (i+1%length)
                {
                    case(0):
                        GLMScript.setEasyDifficulty();
                        break;  
                    case(1):
                        GLMScript.setMediumDifficulty();
                        break;  
                    case(2):
                        GLMScript.setHardDifficulty();
                        break;  
                    case(3):
                        GLMScript.setInsaneDifficulty();
                        break;  
                    default:
                        GLMScript.setEasyDifficulty();
                        break;
                }
            }
            
        }
        DifficultyButtonText.text = newText;
    }

    public void Start(){
    GLMScript = GameObject.FindObjectOfType(typeof(gameLevelManager)) as gameLevelManager;
//   GLMScript.Test();
}

    public void Update(){
    if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("joystick button 1"))
        {
            Debug.Log("fire1");
            // PlayGame();
        }
    
    if(Input.GetButtonDown("Fire2") || Input.GetKey(KeyCode.Space) || Input.GetButtonDown("joystick button 0") )
        {
            //TODO: actually change diffficulty in game scene
            Debug.Log("fire2");
            ChangeDifficultyText();
        }
    
    if(Input.GetButtonDown("Fire3") || Input.GetButtonDown("joystick button 2"))
        {
            Debug.Log("fire3");
            QuitGame();
        }
    }
}
