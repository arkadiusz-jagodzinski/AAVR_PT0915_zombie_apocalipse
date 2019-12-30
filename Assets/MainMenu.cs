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
        string[] difficulty = new string[3] {"easy","normal","hard"};
        var oldText = DifficultyButtonText.text;
        var newText = "";
        var length = 3;
        for(var i = 0;i<length;i++){
            if(oldText == difficulty[i]){
                newText = difficulty[(i+1)%(length)];
            }
        }
        DifficultyButtonText.text = newText;
    }

    public void Update(){
    if(Input.GetButtonDown("Fire1"))
        {
            Debug.Log("fire1");
            // PlayGame();
        }
    
    if(Input.GetButtonDown("Fire2"))
        {
            //TODO: actually change diffficulty in game scene
            Debug.Log("fire2");
            ChangeDifficultyText();
        }
    
    if(Input.GetButtonDown("Fire3"))
        {
            Debug.Log("fire3");
            QuitGame();
        }
    }
}
