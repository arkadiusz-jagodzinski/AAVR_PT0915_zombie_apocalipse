using System.Collections;
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
    private bool changeDifficulityBlocked = false;

    public void PlayGame()
    {
        playSound();
        Debug.Log("PlayGame()");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        playSound();
        Application.Quit();
    }

    public void ChangeDifficultyText()
    {
        playSound();
        const int length = 5;
        string[] difficulty = new string[length] {"tutorial","easy","medium","hard","insane"};
        var oldText = DifficultyButtonText.text;
        var newText = "";
        

        

        for(var i = 0;i<length;i++){
            if(oldText == difficulty[i]){
                newText = difficulty[(i+1)%(length)];
                switch (i%length)
                {
                    case(4):
                        GLMScript.setTutorialDifficulty();
                        break;  
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
    if(Input.GetButton("joystick button 1"))
        {
            Debug.Log("fire1");
            PlayGame();
        }
    
    if(Input.GetButton("Jump") || Input.GetKey(KeyCode.Space) || Input.GetButton("joystick button 0") )
        {
            //TODO: actually change diffficulty in game scene
            if(!changeDifficulityBlocked)
            {
                ChangeDifficultyText();
                changeDifficulityBlocked = true;
                Invoke("unblockDifficulity", 0.4f);
            }
            Debug.Log("fire2");
            
        }
    
    if(Input.GetButton("Fire3") || Input.GetButton("joystick button 2"))
        {
            Debug.Log("fire3");
            QuitGame();
        }
    }

    void unblockDifficulity()
    {
        changeDifficulityBlocked = false;
    }

void playSound(){

    var asources = GetComponents<AudioSource>();
    var click = asources[0];
    click.Play();
}
}
